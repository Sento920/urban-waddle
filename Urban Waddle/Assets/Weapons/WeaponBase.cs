using UnityEngine;
using System.Collections;

public interface WeaponBase {
	weaponType GetWeaponType ();
	void Fire (Vector3 origin, Vector3 dir);
    GameObject GetMesh();
    string GetWeaponName();
	bool isEmpty ();
}

public enum weaponType {
	thrown, fired, swung
};