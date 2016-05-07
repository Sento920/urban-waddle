using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DestroyOnContact : MonoBehaviour {

	void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Geometry")
            Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Geometry")
            Destroy(this.gameObject);
    }
}
