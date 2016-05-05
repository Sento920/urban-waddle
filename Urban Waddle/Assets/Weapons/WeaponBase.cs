using UnityEngine;
using System.Collections;

public interface WeaponBase {
	weaponType GetWeaponType ();
	void Fire (Vector3 origin, Vector3 dir);
    Mesh GetMesh();
    string GetWeaponName();
}

public enum weaponType {
	thrown, fired, swung
};