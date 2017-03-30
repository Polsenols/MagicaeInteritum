using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MovementEffects;

public class GameCountDown : MonoBehaviour {

    public float countDownSeconds = 10;
    private PhotonView m_photonView;
    public bool gameStarted = false;
    private Text countDownText;

    void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
    }
	void Start () {
        if (m_photonView.isMine)
        {
            countDownText = GameObject.Find("CountDown").GetComponent<Text>();
        }
        Time.timeScale = 0;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && PhotonNetwork.isMasterClient)
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
        while(countDownSeconds > 0f)
        {
            yield return 0f;
            countDownSeconds -= Time.unscaledDeltaTime;
            countDownText.text = ((int)countDownSeconds).ToString();
        }
        gameStarted = true;
        Destroy(countDownText.gameObject);
        Time.timeScale = 1;
    }

}
