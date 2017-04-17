using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportFromShop : MonoBehaviour
{

    public GameObject[] teleportPointGameobjects = new GameObject[3];
    private Vector3[] teleportTranformPoints = new Vector3[3];
    private int indexer = 0;

    void Start()
    {
        for (int i = 0; i < teleportPointGameobjects.Length; i++)
            teleportTranformPoints[i] = teleportPointGameobjects[i].GetComponent<Transform>().position;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.GetComponent<PhotonView>().isMine)
        {
            other.transform.position = teleportTranformPoints[indexer];
            indexer++;
            if(indexer == 3)
            {
                indexer = 0;
            }
        }
    }

}
