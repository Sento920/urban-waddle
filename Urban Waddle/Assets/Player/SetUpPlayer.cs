using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetUpPlayer : NetworkBehaviour {

    [SerializeField]
    private Camera Main;
    [SerializeField]
    private GameObject HUD;

	// Use this for initialization
	void Start () {
        if (isLocalPlayer)
        {
            Camera Cam;
            Cam = Instantiate<Camera>(Main);
            GetComponent<PlayerController>().enabled = true;
            GetComponent<PlayerController>().setCamera(Cam);

            // setup ui
            Instantiate(HUD);
            
            //print("Enabled Player Controllers.");
        }
	}
}
