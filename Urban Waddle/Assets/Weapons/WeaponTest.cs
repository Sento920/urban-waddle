using UnityEngine;
using System.Collections;

public class WeaponTest : MonoBehaviour, WeaponBase {

    public GameObject projectile;
    [SerializeField] private Mesh mesh;

    public float speed = 5.0f;

	weaponType type = weaponType.thrown;

	public weaponType GetWeaponType() {
		return type;
	}

    public Mesh GetMesh() {
        return mesh;
    }

	public void Fire(Vector3 origin, Vector3 dir) {
        GameObject bullet = (GameObject)Instantiate(projectile, origin, Quaternion.LookRotation(dir));
        bullet.GetComponent<ProjectileController>().Fire(dir, speed);
        //bullet.GetComponent<Rigidbody>().AddForce(dir * 50.0f);
	}

    public string GetWeaponName() {
        return "Test";
    }
}
