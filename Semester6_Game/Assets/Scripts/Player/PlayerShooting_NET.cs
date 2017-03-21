using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting_NET : Photon.MonoBehaviour
{


    private PhotonView m_PhotonView;
    public GameObject testSpawn;
    // Use this for initialization
    void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SpawnShout();
        }
    }

    void SpawnShout()
    {
        Vector3 pos = transform.position;
        m_PhotonView.RPC("spawnThing", PhotonTargets.All, PhotonNetwork.player.ID, pos);
    }

    [PunRPC]
    private void spawnThing(int playerID, Vector3 spawnPos)
    {
        if (m_PhotonView.isMine)
        {
            Instantiate(testSpawn, spawnPos, Quaternion.identity);
        }
    }
}
