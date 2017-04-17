using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShopPeaceZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().isMine)
            {
                other.GetComponent<PlayerHealth_NET>().invulnurable = true;
                other.GetComponent<SpellManager>().magicPeaceZone = true;
                other.GetComponent<ShopScript>().resourcePerTick = 0;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        other.GetComponent<PlayerHealth_NET>().invulnurable = false;
        other.GetComponent<SpellManager>().magicPeaceZone = false;
        other.GetComponent<ShopScript>().resourcePerTick = other.GetComponent<ShopScript>().originalResourcePerTick;
    }

}
