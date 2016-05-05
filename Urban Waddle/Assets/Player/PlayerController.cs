using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

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

	public GameObject weapon = null;    // why does this need to be a gameobject why does this need to be a gameobject why does this need to be a gameobject
	public GameObject holder;	// holds the weapon model
	public GameObject reticle;	// used for projectile origin

    // Use this for initialization
    void Start () {
        c = GetComponent<CharacterController>();
        //cam = GetComponent<Camera>();
        dashTimer = dashTime;

        //weapon = gameObject.AddComponent<WeaponTest>();
    }
	
	// Update is called once per frame
    void Update () {
        Vector3 moveDirection = Vector3.zero;

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        // prevent the "Doom diagonal speed boost"
        if (moveDirection.magnitude > Vector3.Normalize(moveDirection).magnitude)
            moveDirection.Normalize();

        moveDirection *= speed;

        if (isDashing == true)
            moveDirection += c.transform.forward * dashPower;

        moveDirection.y -= gravity;

        moveDirection *= Time.deltaTime;

        c.Move(moveDirection);

        // aiming things!

        // get vector of pointer at character's y pos
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float dist = (ray.origin.y - c.transform.position.y);
        Vector3 lookDir = ray.origin + ((-dist / ray.direction.y) * ray.direction);

        Quaternion rotation = Quaternion.LookRotation(lookDir - gameObject.transform.position);
        gameObject.transform.rotation = rotation;

        cam.transform.position = gameObject.transform.position + new Vector3(0.0f, camHeight, -camDistance);
        cam.transform.rotation = Quaternion.LookRotation(c.transform.position - cam.transform.position);
        cam.transform.position += (1 / camDriftFraction) * (lookDir - gameObject.transform.position);

		// weapon stuff
		if (Input.GetButton("Fire1") && weapon != null) {
			weapon.GetComponent<WeaponBase>().Fire (reticle.transform.position, gameObject.transform.forward);
		}
		if (weapon != null && weapon.GetComponent<WeaponBase>().isEmpty()) {
			Destroy (weapon);
			weapon = null;	// maybe drop the weapon?
		}
    }

    void FixedUpdate () {
        if (c.isGrounded) {
            if (dashTimer >= dashChargeTime && Input.GetButton("Jump") && canDash && !dashHeld)
            {
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
        }
        else if (dashTimer <= dashTime + dashChargeTime)
            dashTimer++;

        if (dashTimer >= dashTime + dashChargeTime && !Input.GetButton("Jump"))
            dashHeld = false;
    }

	public void SetWeapon(GameObject weapon) {
		this.weapon = Instantiate(weapon);
	}

    public GameObject GetWeapon() {
        return weapon;
    }
}
