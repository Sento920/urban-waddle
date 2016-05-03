using UnityEngine;
using System.Collections;

public class WeaponTest : MonoBehaviour, WeaponBase {

	weaponType type = weaponType.thrown;

	public weaponType getType() {
		return type;
	}

	public void fire(Vector3 dir) {
		Debug.Log ("Test");
	}
}
