using UnityEngine;
using System.Collections;

public class PlayerHUDController : MonoBehaviour {

    GameObject manager;

    public GameObject timerText;

    public GameObject endText;
    public GameObject endBG;

    private double timer;
    private float roundLength;

    string[] winNames;

    // Use this for initialization
    void Start () {
        manager = GameObject.Find("LobbyManager");   // hopefully this works...

        winNames = new string[4] { "RED", "BLUE", "YELLOW", "GREEN" };

        timer = Network.time;

        roundLength = 120;
    }
	
	// Update is called once per frame
	void Update () {
        timerText.GetComponent<UnityEngine.UI.Text>().text = string.Format("{0}:{1:D2}", (int)((roundLength - (Network.time - timer)) / 60), (int)((roundLength - (Network.time - timer)) % 60));   // lol let's not even pretend to sync it
        //print(manager.GetComponent<TimeLobby>().isEnd);
        if (manager.GetComponent<TimeLobby>().isEnd)
        {
            endText.SetActive(true);

            if (manager.GetComponent<TimeLobby>().winner == 5)
                endText.GetComponent<UnityEngine.UI.Text>().text = "TIE GAME";
            else
                endText.GetComponent<UnityEngine.UI.Text>().text = winNames[manager.GetComponent<TimeLobby>().winner] + " WINS";

            endBG.SetActive(true);
        }
    }
}
