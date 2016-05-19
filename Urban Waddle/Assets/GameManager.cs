using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {
    
    [SerializeField] private float timer;
    public float roundLength = 120;
    public float postLength = 10;

    bool isRunning = false;
    public bool isPost = false; // postgame

	int numPlayers = 0;
	PlayerController[] p;

    public int winner = 0;

	// Use this for initialization
	void Start () {
        if (GetComponent<TimeLobby>())
        {
            p = new PlayerController[0];
            //p = GameObject.FindObjectsOfType<PlayerController>();

            /*for (int i = 0; i < GetComponent<TimeLobby>().numPlayers; i++)
            {
                //p[i] = Network.connections[i].gameObject.GetComponent<PlayerController>();
                PlayerController.
            }*/
        }
        //GetComponent<NetworkLobbyManager>().lobbySlots;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<TimeLobby>() && p.Length != GetComponent<TimeLobby>().numPlayers)
        {
            p = GameObject.FindObjectsOfType<PlayerController>();	// my finest hack

			for (int i = 0; i < p.Length; i++) {
				p[i].color = i;
			}

            return;
        }
        if (isPost)
        {
            if (Network.time > timer + postLength)
            {
                isPost = false;
                p = new PlayerController[0];
                GetComponent<TimeLobby>().ReturnToLobby();
            }

        }
        else if (isRunning) {
            //print(p.Length);
            int playersLeft = 0;
            if (GetComponent<TimeLobby>())
            {
                for (int i = 0; i < p.Length; i++)
                {
                    if (p[i].GetHealth() > 0)
                    {
                        winner = i;
                        playersLeft++;
                    }
                }

                //print("PlayersLeft: " + playersLeft + " Winner: " + winner);
            }

		    if (GetComponent<TimeLobby>() && (playersLeft < 2 || Network.time > timer + roundLength)) {
                isPost = true;
                isRunning = false;
                timer = (float)Network.time;
                
                if (playersLeft < 2)
                    GetComponent<TimeLobby>().EndGame(winner);
                else
                    GetComponent<TimeLobby>().EndGame(5);
            }
        }
        else
        {
            timer = (float)Network.time;
            isRunning = true;
        }
    }

    public string GetTime()
    {
        return string.Format("{0}:{1:D2}", (int)((roundLength - (Network.time - timer)) / 60), (int)((roundLength - (Network.time - timer)) % 60));
    }

    public void AddPlayer (PlayerController player)
    {
        //p[player.GetComponent<NetworkIdentity>().playerControllerId] = player;
        //p = GameObject.FindObjectsOfType<PlayerController>();
    }
}
