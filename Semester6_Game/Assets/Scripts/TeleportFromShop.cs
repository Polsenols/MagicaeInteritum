using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class TeleportFromShop : MonoBehaviour
{
    public GameObject[] teleportPointGameobjects;
    private Vector3[] teleportTranformPoints;
    private int indexer = 0;

    void Start()
    {
        teleportTranformPoints = new Vector3[teleportPointGameobjects.Length];

        for (int i = 0; i < teleportPointGameobjects.Length; i++)
            teleportTranformPoints[i] = teleportPointGameobjects[i].transform.position;

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.GetComponent<PhotonView>().isMine)
        {
            other.GetComponent<SpellManager>().canCastSpells = true;
            other.GetComponent<SpellManager>().magicPeaceZone = false;
            other.transform.position = teleportTranformPoints[Random.Range(0,3)];
        }
    }

}
