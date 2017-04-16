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

    private PlayerHealth_NET _playerHealth;
    //private SpellManager _spellManager;
    //private PlayerMovement _playerMovement;

    public float recallCastDuration = 5;
    public bool teleportingToShop = false;

    void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
        _playerHealth = GetComponent<PlayerHealth_NET>();
        //_spellManager = GetComponent<SpellManager>();
        //_playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        teleportVectorPoint = teleportPosition.GetComponent<Transform>().position;
    }

    void Update()
    {
        if (myIndicator != null)
            myIndicator.transform.position = transform.position;

        if (Input.GetKeyDown("x") && teleportingToShop == false)
        {
            RecallToShop();
        }

        if (Input.GetKeyDown("z"))
        {
            StopPlayerRecall();
        }
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
        if (myIndicator != null) //hence the photon I guess
            Destroy(myIndicator);

        //unfreeeze....eeeezeee....
        /*
        if (this.GetComponent<PlayerMovement>() != null)
            this.GetComponent<PlayerMovement>().UnfreezePlayerMovemenet();
        if (this.GetComponent<SpellManager>() != null)
            this.GetComponent<SpellManager>().UnfreezePlayerSpellCasting();
        */
        teleportingToShop = false;
    }

    IEnumerator _recall()
    {
        Debug.Log("Teleporting to Shop");
        teleportingToShop = true;

        /*
        if (this.GetComponent<PlayerMovement>() != null)
            this.GetComponent<PlayerMovement>().FreezePlayerMovement();
        if (this.GetComponent<SpellManager>() != null)
            this.GetComponent<SpellManager>().FreezePlayerSpellCasting();
        */

        _playerHealth.Freeze(recallCastDuration);

        myIndicator = Instantiate(teleportIndicator, transform.position, Quaternion.identity);
        Destroy(myIndicator, recallCastDuration);
        yield return new WaitForSeconds(recallCastDuration);
        transform.position = new Vector3(teleportVectorPoint.x, teleportVectorPoint.y, teleportVectorPoint.z + 2.5f);
        teleportingToShop = false;
        yield break;
    }
}
