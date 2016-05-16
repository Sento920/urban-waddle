using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ProjectileController : NetworkBehaviour {

    private Vector3 dir = Vector3.forward;
	private float speed = 50.0f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
		transform.position += gameObject.transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other) {
        PlayerController p = other.GetComponent<PlayerController>();
        if (p != null && p.GetIFrames() < 1) {
            p.CmdDamage();
            Destroy(gameObject);
        }
        else {
            Destroy(gameObject);  // we don't want it exploding on players with iframes!
        }
    }

	/*
    [Command] public void CmdFire(Vector3 dir, float speed) {
        this.dir = dir;
        this.speed = speed;
    }
	*/
}
