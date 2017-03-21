using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting_NET : Photon.MonoBehaviour {


    private PhotonView m_PhotonView;
    public GameObject testSpawn;
	// Use this for initialization
	void Awake () {
        m_PhotonView = GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnShout();
        }
	}

    void SpawnShout()
    {
            m_PhotonView.RPC("spawnThing", PhotonTargets.All, PhotonNetwork.player.ID);       
    }

    [PunRPC]
    private void spawnThing(int playerID)
    {
        Instantiate(testSpawn, transform.position, Quaternion.identity);
    }
}
