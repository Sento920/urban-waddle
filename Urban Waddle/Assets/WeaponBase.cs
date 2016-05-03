using UnityEngine;
using System.Collections;

public interface WeaponBase {
	weaponType getType ();
	void fire (Vector3 dir);
}

public enum weaponType {
	thrown, tossed, swung
};