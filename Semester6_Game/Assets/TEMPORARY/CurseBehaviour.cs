using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurseBehaviour : AOEImpact
{

    SpellData spellData;
    // Use this for initialization
    void Start()
    {
        spellData = GetComponent<SpellData>();
        impactNearbyEnemies(transform.position, false, spellData.radius(), spellData);
        Destroy(gameObject, 3.0f);
    }

}
