using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportToShop : Photon.MonoBehaviour
{

    public PhotonView m_photonView;
    public GameObject magicShopPosition;
    public GameObject teleportIndicator;
    private GameObject myIndicator;
    private Vector3[] teleportVectorPoints = new Vector3[5];

    public float bigRecallOffset = 2.5f;
    public float smallRecallOffset = 1.5f;

    private PlayerMovement _playerMovement;

    public float recallCastDuration = 5;
    public float currentRecallAmount = 0;
    public bool teleportingToShop = false;
    private float t = 0;

    public Image recallUI1;
    public Image recallUI2;
    public Image recallUI3;

    void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        for (int i = 0; i < teleportVectorPoints.Length; i++)
            teleportVectorPoints[i] = magicShopPosition.GetComponent<Transform>().position;

        recallUI3.GetComponent<Image>();
        recallUI3.enabled = false;
        recallUI1.GetComponent<Image>();
        recallUI1.enabled = false;
        recallUI2.GetComponent<Image>();
        recallUI2.enabled = false;

        teleportVectorPoints[0] = new Vector3(teleportVectorPoints[0].x, teleportVectorPoints[0].y, teleportVectorPoints[0].z - bigRecallOffset);
        teleportVectorPoints[1] = new Vector3(teleportVectorPoints[1].x + bigRecallOffset, teleportVectorPoints[1].y, teleportVectorPoints[1].z);
        teleportVectorPoints[2] = new Vector3(teleportVectorPoints[2].x - bigRecallOffset, teleportVectorPoints[2].y, teleportVectorPoints[2].z);
        teleportVectorPoints[3] = new Vector3(teleportVectorPoints[3].x - smallRecallOffset, teleportVectorPoints[3].y, teleportVectorPoints[3].z - smallRecallOffset);
        teleportVectorPoints[4] = new Vector3(teleportVectorPoints[4].x + smallRecallOffset, teleportVectorPoints[4].y, teleportVectorPoints[4].z - smallRecallOffset);
    }

    void Update()
    {
        if (m_photonView.isMine)
        {
            if (myIndicator != null)
                myIndicator.transform.position = transform.position;

            if (Input.GetKeyDown("x") && teleportingToShop == false)
            {
                RecallToShop();
            }
            else if (((Input.GetKeyDown("x")) || Input.GetKeyDown(KeyCode.Escape)) && teleportingToShop == true)
            {
                StopPlayerRecall();
            }


            if (teleportingToShop)
                currentRecallAmount = Mathf.Lerp(0, recallCastDuration, t);
            else
                currentRecallAmount = 0;

            if (t < 1)
            {
                t += Time.deltaTime / recallCastDuration;
            }

        }
    }

    public float currentRecallDuration()
    {
        return currentRecallAmount;
    }

    public void RecallToShop()
    {
        m_photonView.RPC("_recallToShop", PhotonTargets.All);
    }

    [PunRPC]
    void _recallToShop()
    {
        StartCoroutine("_recall");
    }

    public void StopPlayerRecall()
    {
        m_photonView.RPC("_stopPlayerRecall", PhotonTargets.All);
    }

    [PunRPC]
    void _stopPlayerRecall()
    {

            StopCoroutine("_recall");

            if (myIndicator != null)
                Destroy(myIndicator);

            recallUI3.enabled = false;
            recallUI1.enabled = false;
            recallUI2.enabled = false;

            teleportingToShop = false;
    }

    IEnumerator _recall()
    {
        t = 0;
        teleportingToShop = true;
        if (m_photonView.isMine)
        {
            _playerMovement.moving = false;
        }
        myIndicator = (GameObject)Instantiate(teleportIndicator, transform.position, Quaternion.identity);
        Destroy(myIndicator, recallCastDuration);

        recallUI3.enabled = true;
        recallUI1.enabled = true;
        recallUI2.enabled = true;

        yield return new WaitForSeconds(recallCastDuration);

        recallUI3.enabled = false;
        recallUI1.enabled = false;
        recallUI2.enabled = false;
        transform.position = teleportVectorPoints[m_photonView.ownerId - 1];
        teleportingToShop = false;
        yield break;
    }
}