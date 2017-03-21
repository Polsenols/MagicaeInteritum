using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth_NET : Photon.MonoBehaviour {

    PhotonView m_PhotonView;
    public int maxHealth;
    private int health;

    void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        //If this script is not on the local player, destroy it.
    }

    public int getHealth()
    {
       return health;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            AttackShout();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
        Debug.Log("I now have health: " + health);
    }

    public void Die()
    {
        Debug.Log("I am dead xD");
    }

    void AttackShout()
    {
        if(m_PhotonView.isMine)
        m_PhotonView.RPC("IWasAttacked", PhotonTargets.Others, PhotonNetwork.player.ID);
    }

    [PunRPC]
    private void IWasAttacked(int playerID)
    {
        string log = "I was Attacked by " + playerID;
        Debug.Log(log);
        TakeDamage(10);
    }
}
