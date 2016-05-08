using UnityEngine;
using System.Collections;

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
