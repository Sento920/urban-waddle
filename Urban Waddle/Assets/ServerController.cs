using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ServerController : MonoBehaviour {

    public int port = 7777;
    string ip = "localhost";

    public GameObject startUI;

    // Use this for initialization
    void Start () {
        startUI.SetActive(true);
        gameObject.GetComponent<NetworkLobbyManager>().networkPort = port;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void HostServer ()
    {
        // because buttons are broken...
        gameObject.GetComponent<NetworkLobbyManager>().StartHost();
        //gameObject.GetComponent<NetworkLobbyManager>().ServerChangeScene("ArtTestZone");
        if (gameObject.GetComponent<NetworkLobbyManager>().isNetworkActive)
        {
            startUI.SetActive(false);
        }
    }

    public void SetIP (string ip)
    {
        this.ip = ip;
    }

    public void JoinServer ()
    {
        gameObject.GetComponent<NetworkLobbyManager>().networkAddress = ip;
        gameObject.GetComponent<NetworkLobbyManager>().StartClient();
        if (gameObject.GetComponent<NetworkLobbyManager>().isNetworkActive)
        {
            startUI.SetActive(false);
        }
    }
}
