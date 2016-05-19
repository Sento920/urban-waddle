using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TimeLobby : NetworkLobbyManager {

    private float timer = 0;
    public bool isEnd = false;

    public int winner;

    bool isHost = false;

    public override void OnLobbyStartHost() {
        isHost = true;
    }

    public override void OnLobbyServerPlayersReady()
    {
        timer = Time.time + 15; // wait 15 seconds to start
    }

    void Update()
    {
        if (timer == 0)
            return;

        if (Time.time > timer && isHost)
        {
            timer = 0;
            ServerChangeScene(playScene);   // TODO: map vote
            GetComponent<GameManager>().enabled = true;
        }
        else
        {
            for (int i = 0; i < numPlayers; i++)
            {
                lobbySlots[i].GetComponent<PlayerLobbyController>().timeLeft = (int)(timer - Time.time);
            }
        }
    }

    public float GetTime()
    {
        return Time.time;
    }

    public void ReturnToLobby()
    {
        for (int i = 0; i < numPlayers; i++)
        {
            lobbySlots[i].GetComponent<PlayerLobbyController>().isEnd = false;
        }
        GetComponent<GameManager>().enabled = false;
        isEnd = false;
        ServerReturnToLobby();
    }

    public void EndGame(int winner)
    {
        this.winner = winner;
        for (int i = 0; i < numPlayers; i++)
        {
            lobbySlots[i].GetComponent<PlayerLobbyController>().winner = winner;
            lobbySlots[i].GetComponent<PlayerLobbyController>().isEnd = true;
        }
        isEnd = true;
    }
}
