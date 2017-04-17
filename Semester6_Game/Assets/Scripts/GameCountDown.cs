using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MovementEffects;

public class GameCountDown : MonoBehaviour
{

    public float countDownSeconds = 10;
    public GameObject winState;
    private Text winnerNameText;
    private PhotonView m_photonView;
    private Text countDownText;
    private List<PlayerHealth_NET> Player = new List<PlayerHealth_NET>();
    private GameObject Canvas;

    void Awake()
    {
        Canvas = GameObject.Find("Canvas");
        winState = GameObject.Find("WinState");
        winState.SetActive(false);
        m_photonView = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (m_photonView.isMine)
        {
            countDownText = GameObject.Find("CountDown").GetComponent<Text>();
        }
        if (PhotonNetwork.isMasterClient)
        {
            m_photonView.RPC("startCountDown", PhotonTargets.AllBuffered, PhotonNetwork.time);
        }
    }

    [PunRPC]
    void startCountDown(double countDownTimeStart)
    {
        countDownSeconds -= (float)(PhotonNetwork.time - countDownTimeStart);
        Timing.RunCoroutine(_CountDown());
    }

    void FindWinner()
    {
        m_photonView.RPC("DisplayWinner", PhotonTargets.All, GetWinner());
    }
    
    private int GetWinner()
    {
        int highestScorePlayerID = -1;
        for (int i = 0; i < SpawnManager.Instance.Players.Count; i++)
        {
            Debug.Log(SpawnManager.Instance.Players[i].GetComponent<CharacterManager_NET>().score);
            if (highestScorePlayerID < SpawnManager.Instance.Players[i].GetComponent<CharacterManager_NET>().score)
            {
                highestScorePlayerID = SpawnManager.Instance.Players[i].GetComponent<CharacterManager_NET>().playerID;
            }
        }
        return highestScorePlayerID;
    }

    [PunRPC]
    IEnumerator<float> _CountDown()
    {
        while (countDownSeconds > 0f)
        {
            yield return 0f;
            countDownSeconds -= Time.deltaTime;
            countDownText.text = "Time left: " + ((int)countDownSeconds).ToString();
        }
        if (PhotonNetwork.isMasterClient)
        {
            FindWinner();
        }
    }

    [PunRPC]
    void DisplayWinner(int playerID)
    {
        for (int i = 0; i < SpawnManager.Instance.Players.Count; i++)
        {
            if(SpawnManager.Instance.Players[i].playerID == playerID)
            {
                SpawnManager.Instance.Players[i].transform.localScale *= 5;
                SpawnManager.Instance.Players[i].setHealth(1000000);
                SpawnManager.Instance.Players[i].GetComponent<Animator>().SetBool("Won", true);
                string playerName = SpawnManager.Instance.Players[i].GetComponent<CharacterManager_NET>().playerName;
                //go.GetComponent<Image>().rectTransform.position = UIPos;
                winState.SetActive(true);
                winnerNameText = winState.GetComponentInChildren<Text>();
                winnerNameText.text = playerName;
            }
        }

    }

}
