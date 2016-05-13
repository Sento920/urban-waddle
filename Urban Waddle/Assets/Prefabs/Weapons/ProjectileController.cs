using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ProjectileController : NetworkBehaviour {

    private Vector3 dir;
    private float speed;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.position += dir * speed * Time.deltaTime;
    }

    [Command] public void CmdFire(Vector3 dir, float speed) {
        this.dir = dir;
        this.speed = speed;
    }
}
