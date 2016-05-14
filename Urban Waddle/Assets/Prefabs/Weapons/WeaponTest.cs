using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WeaponTest : NetworkBehaviour, WeaponBase {

    public GameObject projectile;
    [SerializeField] private GameObject mesh;

	private int ammo = 5;

    public float speed = 5.0f;

	weaponType type = weaponType.thrown;

	public weaponType GetWeaponType() {
		return type;
	}

    public GameObject GetMesh() {
        return mesh;
    }

	public void Fire(Vector3 origin, Vector3 dir) {
        GameObject bullet = (GameObject)Instantiate(projectile, origin, Quaternion.LookRotation(dir));
        
        //bullet.GetComponent<ProjectileController>().Fire(dir, speed);
        //bullet.GetComponent<Rigidbody>().AddForce(dir * 50.0f);
		ammo--;

		NetworkServer.Spawn(bullet);
	}

    public string GetWeaponName() {
        return "Test";
    }

	public bool isEmpty() {
		return (ammo < 1);
	}
}
