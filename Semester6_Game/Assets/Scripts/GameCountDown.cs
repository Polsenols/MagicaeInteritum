using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MovementEffects;

public class GameCountDown : MonoBehaviour
{

    public float countDownSeconds = 10;
    public GameObject winState;
    public Text winnerNameText;
    private PhotonView m_photonView;
    private Text countDownText;
    private List<PlayerHealth_NET> Player = new List<PlayerHealth_NET>();

    void Awake()
    {
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

    IEnumerator<float> _CountDown()
    {
        while (countDownSeconds > 0f)
        {
            yield return 0f;
            countDownSeconds -= Time.unscaledDeltaTime;
            countDownText.text = "Time left: " + ((int)countDownSeconds).ToString();
        }

        Debug.Log("Finding winner..");
        int highestScorePlayerID = -1;
        for (int i = 0; i < SpawnManager.Instance.Players.Count; i++)
        {
            if (highestScorePlayerID < SpawnManager.Instance.Players[i].GetComponent<CharacterManager_NET>().score)
            {
                highestScorePlayerID = i;
                if(i > 0)
                {
                    Player.Clear();
                }
                Player.Add(SpawnManager.Instance.Players[i]);
            }
            else if (highestScorePlayerID == SpawnManager.Instance.Players[i].GetComponent<CharacterManager_NET>().score)
            {
                Player.Add(SpawnManager.Instance.Players[i]);
            }
        }

        string winText = "";
        for (int i = 0; i < Player.Count; i++)
        {
            if (!SpawnManager.Instance.Players[i].gameObject.activeSelf)
            {
                SpawnManager.Instance.Players[i].gameObject.SetActive(true);
            }

            SpawnManager.Instance.Players[i].transform.localScale *= 5;
            SpawnManager.Instance.Players[i].setHealth(1000000);
            SpawnManager.Instance.Players[i].GetComponent<Animator>().SetBool("Won", true);
            string playerName = SpawnManager.Instance.Players[i].GetComponent<CharacterManager_NET>().playerName;
            winState.SetActive(true);
            winText += playerName + " ";
        }

        winnerNameText.text = winText + "won!";
        
        //Play fireworks!
        //Text on screen
    }

}
