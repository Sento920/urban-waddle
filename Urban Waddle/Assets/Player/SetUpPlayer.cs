using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetUpPlayer : NetworkBehaviour {

    [SerializeField]
    private Camera Main;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            Camera Cam;
            Cam = Instantiate<Camera>(Main);
            GetComponent<PlayerController>().enabled = true;
            GetComponent<PlayerController>().setCamera(Cam);
            GetComponent<CharacterController>().enabled = true;
            
            print("Enabled Player Controllers.");
        }
	}
}
