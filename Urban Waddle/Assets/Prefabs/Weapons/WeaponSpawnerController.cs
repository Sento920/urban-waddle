using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WeaponSpawnerController : NetworkBehaviour {

    public int respawnTime = 10;
    [SyncVar] private float respawnTimer = 0;
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
        if (respawnTimer > 0 && (weaponModel.GetComponent<MeshRenderer>().enabled || gameObject.GetComponent<Collider>().enabled))
        {
            weaponModel.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        } else if (respawnTimer == 0 && (!weaponModel.GetComponent<MeshRenderer>().enabled || !gameObject.GetComponent<Collider>().enabled))
        {
            weaponModel.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<Collider>().enabled = true;
        }
        if (respawnTimer != 0 && Time.time > respawnTimer)
        {
            weaponModel.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<Collider>().enabled = true;
            respawnTimer = 0;
        }
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
                weaponModel.GetComponent<MeshRenderer>().enabled = false;    //Destroy(weaponModel);
            gameObject.GetComponent<Collider>().enabled = false;
            respawnTimer = Time.time + respawnTime; //Destroy(gameObject);
        }
    }

}
