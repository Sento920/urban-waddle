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

    [Command] public void CmdFire(Vector3 dir, float speed) {
        this.dir = dir;
        this.speed = speed;
    }

	public void Fire(Vector3 dir, float speed) {
		this.dir = dir;
		this.speed = speed;
	}
}
