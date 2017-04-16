using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToShop : Photon.MonoBehaviour
{

    public PhotonView m_photonView;
    public GameObject teleportPosition;
    public GameObject teleportIndicator;
    private GameObject myIndicator;
    private Vector3 teleportVectorPoint;
    private PlayerHealth_NET myPlayerHealth;
    public float recallCastDuration = 5;

    void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
        myPlayerHealth = this.GetComponent<PlayerHealth_NET>();
    }

    void Start()
    {
        teleportVectorPoint = teleportPosition.GetComponent<Transform>().position;
    }

    void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            RecallToShop();
        }
    }

    public void RecallToShop()
    {
        m_photonView.RPC("_recallToShop", PhotonTargets.All);
    }

    [PunRPC]
    void _recallToShop()
    {
        Debug.Log("Teleporting to Shop");
        myPlayerHealth.Freeze(recallCastDuration);
        myIndicator = Instantiate(teleportIndicator, transform.position, Quaternion.identity);
        Destroy(myIndicator, recallCastDuration);
        transform.position = new Vector3(teleportVectorPoint.x, teleportVectorPoint.y, teleportVectorPoint.z + 2.5f);
    }

    public void StopPlayerRecall()
    {
        m_photonView.RPC("_stopPlayerRecall", PhotonTargets.All);
    }

    [PunRPC]
    void _stopPlayerRecall()
    {
        if (myIndicator != null)
            Destroy(myIndicator);
    }


}
