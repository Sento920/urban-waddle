using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public interface WeaponBase {
	weaponType GetWeaponType ();
    /* 
      + All weapons must also call NetworkServer.Spawn(projectilePrefab); to properly get the transform.
      + Command Block Methods MUST start with "Cmd"
      + Monobehaviour MUST be changed to "NetworkingBehaviour" as well. 
            (It should offer all usual functionality, with the addition of networking.)
      + To conserve networking capacity, there is NO sent data from the transform beyond the initial rigidbody with velocity. 
            (If we want homing bullets, we will need to change projectiles.)     
      + * Command Block means the code is run on the server, and thus is put out to clients asap.
      + Talk to Tyler if you have questions.
    */
    
	void CmdFire (Vector3 origin, Vector3 dir);
    GameObject GetMesh();
    string GetWeaponName();
	bool isEmpty ();
}

public enum weaponType {
	thrown, fired, swung
};