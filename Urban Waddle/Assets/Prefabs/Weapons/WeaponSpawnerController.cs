using UnityEngine;
using System.Collections;

public class WeaponSpawnerController : MonoBehaviour {

    public GameObject weapon;

	private GameObject weaponModel;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<MeshFilter>().mesh = weapon.GetComponent<Mesh>();
		weaponModel = (GameObject)Instantiate(weapon.GetComponent<WeaponBase>().GetMesh(), transform.position, transform.rotation);
		weaponModel.transform.parent = transform;
        
	}
	
	// Update is called once per frame
    void Update() {
        
    }

	void OnTriggerEnter(Collider other) {
        PlayerController p = other.GetComponent<PlayerController>();

        if (p != null) {
            p.SetWeapon(weapon);    // if collidee is a player, and said player has no weapon
            Destroy(gameObject);
        }
    }
}
