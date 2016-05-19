using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerLobbyController : NetworkBehaviour {

    public GameObject lobbyUI;
    [SyncVar] public int timeLeft;
    [SerializeField] private GameObject readyUI;
    public GameObject readyText;

    [SyncVar] public bool isEnd = false;
    [SyncVar] public int winner = 0;

    string[] winNames;

    // Use this for initialization
    void Start () {
        if (isLocalPlayer)
        {
            winNames = new string[4] { "RED", "BLUE", "YELLOW", "GREEN" };
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
        if (isLocalPlayer && gameObject.GetComponent<NetworkLobbyPlayer>().readyToBegin && timeLeft > 0)
        {
            readyText.GetComponent<UnityEngine.UI.Text>().text = "GET READY... " + timeLeft;
        }
        else if (isLocalPlayer && !gameObject.GetComponent<NetworkLobbyPlayer>().readyToBegin)
        {
            readyText.GetComponent<UnityEngine.UI.Text>().text = "WAITING FOR PLAYERS...";
        }
        
        if (isLocalPlayer && !gameObject.GetComponent<NetworkLobbyPlayer>().readyToBegin && (!lobbyUI.activeSelf || !readyUI.activeSelf))
        {
            lobbyUI.SetActive(true);
            readyUI.SetActive(true);
        }
        if (!isLocalPlayer && (lobbyUI.activeSelf || readyUI.activeSelf))
        {
            lobbyUI.SetActive(false);
            readyUI.SetActive(false);
        }
    }
}
