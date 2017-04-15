using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTeleportationTo : MonoBehaviour
{

    public GameObject[] teleportPointGameobjects = new GameObject[3];
    private Vector3[] teleportTranformPoints = new Vector3[3];
    private int teleportIndexer = 0;

    void Start()
    {
        for (int i = 0; i < teleportPointGameobjects.Length; i++)
        {
            teleportTranformPoints[i] = teleportPointGameobjects[i].GetComponent<Transform>().position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Should be some code here that makes the player unable to cast and lose hp etc.
            Debug.Log("Teleporting player to magic shop");
            other.transform.position = teleportTranformPoints[teleportIndexer];
            teleportIndexer++;
            if (teleportIndexer == 3)
            {
                teleportIndexer = 0;
            }
        }
    }
}
