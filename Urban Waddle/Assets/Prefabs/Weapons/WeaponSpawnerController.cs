using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WeaponSpawnerController : NetworkBehaviour {

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
        GameObject p = other.gameObject;
        if (p != null)
            CmdgrabWeapon(p);
    }

    [Command] void CmdgrabWeapon(GameObject o) { 
        PlayerController p = o.GetComponent<PlayerController>();
        if (p != null){
            p.CmdSetWeapon(weapon);    // if collidee is a player, and said player has no weapon
            if (weaponModel != null)
                Destroy(weaponModel);
            Destroy(gameObject);
        }
    }

}
