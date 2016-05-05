using UnityEngine;
using System.Collections;

public class WeaponSpawnerController : MonoBehaviour {

    public GameObject weapon;

	// Use this for initialization
	void Start () {
        //gameObject.GetComponent<Mesh>() = weapon.GetComponent<Mesh>();
	}
	
	// Update is called once per frame
    void Update() {
        
    }

	void OnTriggerEnter(Collider other) {
        PlayerController p = other.GetComponent<PlayerController>();

        if (p != null && p.GetWeapon() == null) {
            p.SetWeapon(weapon);    // if collidee is a player, and said player has no weapon
            Destroy(gameObject);
        }
    }
}
