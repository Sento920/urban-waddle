using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    public float gravity = 10.0f;
    public float speed = 10.0f;

    CharacterController c;
    public Camera cam;

    public float camHeight = 10.0f;     // height of camera above player
    public float camDistance = 3.0f;    // distance of camera behind player
    public float camDriftFraction = 4.0f;   // camera will move 1/x * aim dist 

    public bool canDash = true;
    public float dashPower = 10.0f; // scale movement by this when dashing
    public float dashTime = 3.0f;  // duration of dash in fixed updates
    public float dashChargeTime = 100.0f;
    private float dashTimer;
    private bool isDashing = false; // me irl :(
    private bool dashHeld = false;  // to prevent holding dash repeatedly dashing

	[SyncVar] public GameObject weapon = null;    // why does this need to be a gameobject why does this need to be a gameobject why does this need to be a gameobject
	public GameObject holder;	// holds the weapon model
	public GameObject reticle;	// used for projectile origin

	private GameObject weaponModel = null;	// our held weapon

    private Vector3 dashDir;    // the last move direction, for dashes and looking

    // Use this for initialization
    void Start () {
		if (isLocalPlayer) {
       		c = GetComponent<CharacterController>();
        	//cam = GetComponent<Camera>();
        	dashTimer = dashTime;
        	reticle.SetActive(false);

        	//weapon = gameObject.AddComponent<WeaponTest>();
		}
    }
	
	// Update is called once per frame
    void Update () {
		if (isLocalPlayer) {
			Vector3 moveDirection = Vector3.zero;

			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0.0f, Input.GetAxis ("Vertical"));

			// prevent the "Doom diagonal speed boost"
			if (moveDirection.magnitude > Vector3.Normalize (moveDirection).magnitude)
				moveDirection.Normalize ();

			moveDirection *= speed;

			if (!isDashing && moveDirection.magnitude != 0.0f)
				dashDir = Vector3.Normalize (moveDirection);
			else if (!isDashing)
				dashDir = transform.forward;

			if (isDashing)
				moveDirection = dashDir * dashPower;
			else
				moveDirection.y -= gravity;


			moveDirection *= Time.deltaTime;

			c.Move (moveDirection);

			// aiming things!

			// get vector of pointer at character's y pos
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			float dist = (ray.origin.y - c.transform.position.y);
			Vector3 lookDir = ray.origin + ((-dist / ray.direction.y) * ray.direction);

			Quaternion rotation;

			if (!isDashing)
				rotation = Quaternion.LookRotation (lookDir - gameObject.transform.position);
			else
				rotation = Quaternion.LookRotation (dashDir);

			gameObject.transform.rotation = rotation;   // if holding a weapon, take aim

			cam.transform.position = gameObject.transform.position + new Vector3 (0.0f, camHeight, -camDistance);
			cam.transform.rotation = Quaternion.LookRotation (c.transform.position - cam.transform.position);
			cam.transform.position += (1 / camDriftFraction) * (lookDir - gameObject.transform.position);

			// weapon stuff
			if (Input.GetButtonDown ("Fire1") && weapon != null) {
				weapon.GetComponent<WeaponBase> ().CmdFire (reticle.transform.position, gameObject.transform.forward);
			}
			if (weapon != null && weapon.GetComponent<WeaponBase> ().isEmpty ()) {
				Destroy (weapon);
				Destroy (weaponModel);
				weapon = null;	// maybe drop the weapon?
				reticle.SetActive (false);
			}
		}
    }

    void FixedUpdate () {
		if (isLocalPlayer) {
			if (c.isGrounded) {
				if (dashTimer >= dashChargeTime && Input.GetButton ("Jump") && canDash && !dashHeld) {
					// experimental dash
					isDashing = true;
					dashTimer = 0.0f;
					dashHeld = true;
				}
			}
			if (isDashing == true) {
				dashTimer++;
				if (dashTimer >= dashTime)
					isDashing = false;
			} else if (dashTimer <= dashTime + dashChargeTime)
				dashTimer++;

			if (dashTimer >= dashTime + dashChargeTime && !Input.GetButton ("Jump"))
				dashHeld = false;
		}
    }

	[Command] public void CmdSetWeapon(GameObject weapon) {
		if (this.weapon != null) {
			Destroy (this.weapon);
			this.weapon = null;
			Destroy (weaponModel);
			weaponModel = null;
		}

		GameObject tmp = Instantiate(weapon);
        NetworkServer.Spawn(tmp); 
		this.weapon = tmp;
	}

	public void SetWeaponModel() {
		weaponModel = (GameObject)Instantiate(weapon.GetComponent<WeaponBase>().GetMesh(), holder.transform.position, holder.transform.rotation);
		weaponModel.transform.parent = holder.transform;
		reticle.SetActive(true);
	}

    public GameObject GetWeapon() {
        return weapon;
    }

    public void setCamera(Camera cam)
    {
        this.cam = cam;
    }

}
