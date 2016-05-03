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

	public WeaponBase weapon;

    // Use this for initialization
    void Start () {
        c = GetComponent<CharacterController>();
        //cam = GetComponent<Camera>();
        dashTimer = dashTime;
    }
	
	// Update is called once per frame
    void Update () {
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
			weapon.fire (gameObject.transform.forward);
		}
    }

    void FixedUpdate () {
        Vector3 moveDirection = Vector3.zero;

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection.Normalize();  // prevent the "Doom diagonal speed boost"
        moveDirection *= speed;

        if (c.isGrounded)
        {
            if (dashTimer >= dashChargeTime && Input.GetButton("Jump") && canDash && !dashHeld) {
                // experimental dash
                isDashing = true;
                dashTimer = 0.0f;
                dashHeld = true;
            }
        }
        if (isDashing == true) {
           moveDirection += c.transform.forward * dashPower;
            dashTimer++;
            if (dashTimer >= dashTime)
                isDashing = false;
        }
        else if (dashTimer <= dashTime + dashChargeTime)
            dashTimer++;

        if (dashTimer >= dashTime + dashChargeTime && !Input.GetButton("Jump"))
            dashHeld = false;

        moveDirection.y -= gravity;

        moveDirection *= Time.fixedDeltaTime;
        //moveDirection *= Time.deltaTime;

        c.Move(moveDirection);
	}

	void SetWeapon(WeaponBase weapon) {
		this.weapon = weapon;
	}
}
