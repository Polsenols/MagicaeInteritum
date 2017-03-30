using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : SingleTon<PlayerScore> {

    public Text[] playerScoreText;
    public int[] playerScore;
    public int scoreIncrement = 5;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < playerScoreText.Length; i++)
        {
            playerScore[i] = 0;
        }
	}

    public void SetScores()
    {
        
    }
	
    [PunRPC]
    public void SetPlayerScore(int playerID)
    {
        playerScore[playerID] += 5;
        playerScoreText[playerID].text = playerScore[playerID].ToString();
    }
	
}
