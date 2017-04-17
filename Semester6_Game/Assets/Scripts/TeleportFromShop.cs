using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class TeleportFromShop : MonoBehaviour
{
    public float teleportOpenDelay;
    public GameObject[] teleportPointGameobjects = new GameObject[3];
    private Vector3[] teleportTranformPoints = new Vector3[3];
    private int indexer = 0;

    void Start()
    {
        for (int i = 0; i < teleportPointGameobjects.Length; i++)
            teleportTranformPoints[i] = teleportPointGameobjects[i].GetComponent<Transform>().position;

        Timing.RunCoroutine(_EnablePortal());
    }

    IEnumerator<float> _EnablePortal()
    {
        transform.gameObject.SetActive(false);
        yield return Timing.WaitForSeconds(teleportOpenDelay);
        transform.gameObject.SetActive(true);

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
