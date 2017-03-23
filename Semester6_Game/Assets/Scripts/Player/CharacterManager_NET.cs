using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager_NET : Photon.MonoBehaviour {

    public PhotonView m_PhotonView;
    private Animator anim;
    private PlayerMovement playerMove;
    private PlayerHealth_NET playerHealth;
    public int playerID;

    public int health;
	// Use this for initialization
    void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }
	void Start () {
        playerHealth = GetComponent<PlayerHealth_NET>();
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMovement>();
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
}
