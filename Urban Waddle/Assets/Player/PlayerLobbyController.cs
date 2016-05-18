using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerLobbyController : NetworkBehaviour {

    public GameObject lobbyUI;
    [SyncVar] public int timeLeft;
    public GameObject readyUI;
    public GameObject readyText;

    // Use this for initialization
    void Start () {
        if (isLocalPlayer)
        {
            lobbyUI.SetActive(true);
            readyUI.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (isLocalPlayer && !gameObject.GetComponent<NetworkLobbyPlayer>().readyToBegin && Input.GetButton("Jump"))
        {
            //gameObject.GetComponent<NetworkLobbyPlayer>().readyToBegin = true;
            gameObject.GetComponent<NetworkLobbyPlayer>().SendReadyToBeginMessage();
            lobbyUI.SetActive(false);
        }
        if (isLocalPlayer && timeLeft > 0)
        {
            readyText.GetComponent<UnityEngine.UI.Text>().text = "GET READY... " + timeLeft;
        }
        if (!isLocalPlayer && (lobbyUI.activeSelf || readyUI.activeSelf))
        {
            lobbyUI.SetActive(false);
            readyUI.SetActive(false);
        }
    }
}
