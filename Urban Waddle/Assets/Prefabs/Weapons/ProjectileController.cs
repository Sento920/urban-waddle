using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

    private Vector3 dir;
    private float speed;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += dir * speed * Time.deltaTime;
	}

    public void Fire(Vector3 dir, float speed) {
        this.dir = dir;
        this.speed = speed;
    }
}
