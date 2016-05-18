using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SetUpPlayer : NetworkBehaviour {

    [SerializeField]
    private Camera Main;
	[SerializeField]
	private GameObject Man;

	// Use this for initialization
	void Start () {
		Man = GameObject.Find ("GameManager");
		Man.GetComponent<GameManager>().AddPlayer(GetComponent<PlayerController>());
        if (isLocalPlayer)
        {
            Camera Cam;
            Cam = Instantiate<Camera>(Main);
            GetComponent<PlayerController>().enabled = true;
            GetComponent<PlayerController>().setCamera(Cam);
            
            //print("Enabled Player Controllers.");
        }
	}
}
