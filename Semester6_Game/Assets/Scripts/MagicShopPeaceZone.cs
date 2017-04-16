using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShopPeaceZone : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth_NET>().invulnurable = true;
            other.GetComponent<SpellManager>().canCastSpells = false;
            other.GetComponent<ShopScript>().resourcePerTick = 0;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Peace Zone left");
        other.GetComponent<PlayerHealth_NET>().invulnurable = false;
        other.GetComponent<SpellManager>().canCastSpells = true;
        other.GetComponent<ShopScript>().resourcePerTick = other.GetComponent<ShopScript>().originalResourcePerTick;
    }

}
