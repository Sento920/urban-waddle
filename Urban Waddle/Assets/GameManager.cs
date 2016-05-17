using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour {

	int numPlayers = 0;
	PlayerController[] p;

	// Use this for initialization
	void Start () {
		p = new PlayerController[8];
	}
	
	// Update is called once per frame
	void Update () {
		// see who's alive
		int playersLeft = 0;

		for (int i = 0; i < numPlayers; i++) {
			if (p[i].GetHealth() > 0)
				playersLeft++;
		}

		if (playersLeft < 2) {
			//print (playersLeft);
		}
	}

	public void AddPlayer(PlayerController player) {
		p [numPlayers] = player;
		numPlayers++;
	}
}
