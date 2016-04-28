using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float gravity = 10.0f;
    public float maxSpeed = 10.0f;
    public float acceleration = 0.5f;

    CharacterController c;

    // Use this for initialization
    void Start () {
        c = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 moveDirection = Vector3.zero;

        if (c.isGrounded)
        {
            moveDirection = new Vector3(acceleration * Input.GetAxis("Horizontal"), 0.0f, acceleration * Input.GetAxis("Vertical"));

        }
        moveDirection.y -= gravity;

        c.Move(moveDirection);
	}
}
