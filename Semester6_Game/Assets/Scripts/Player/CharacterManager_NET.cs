using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager_NET : Photon.PunBehaviour {

    private PhotonView m_PhotonView;
    private Animator anim;
    private PlayerHealth_NET playerHealth;
    public int playerID;
    public string playerName;
    public int score;

    public int health;
	// Use this for initialization
    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth_NET>();
        anim = GetComponent<Animator>();
        m_PhotonView = GetComponent<PhotonView>();
    }
	void Start () {    
        SetScore(0);
	}

    void Update()
    {
        if (!m_PhotonView.isMine)
        {
            playerHealth.setHealth(health);
        }
        else
        {
            health = playerHealth.getHealth();
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(health);
            stream.SendNext(anim.GetFloat("m_MoveSpeed"));
        }
        else
        {
            health = (int)stream.ReceiveNext();
            anim.SetFloat("m_MoveSpeed", (float)stream.ReceiveNext());
        }
    }

    [PunRPC]
    public void SetID(int id)
    {
        playerID = id;
    }

    [PunRPC]
    public void SetName(string name)
    {
        playerName = name;
    }

    public void ShoutScore()
    {
        m_PhotonView.RPC("SetScore", PhotonTargets.All, 10);
    }

    [PunRPC]
    public void SetScore(int score)
    {
        this.score += score;
        string scoreText = playerName + " : " + this.score.ToString();
        GameObject.Find("Player" + playerID).GetComponent<Text>().text = scoreText;
        Debug.Log(playerID);
    }
}
