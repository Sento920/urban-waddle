using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetUpPlayer : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            GetComponentInChildren<PlayerController>().enabled = true;
            GetComponentInChildren<CharacterController>().enabled = true;
        }
	}
	
}
