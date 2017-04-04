using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : SingleImpact {

    
    void OnTriggerEnter(Collider other)
    {
        Impact(other);
        Vector3 hitPlayerPos = other.transform.position;
        other.transform.position = spellData.owner.transform.position;
        spellData.owner.transform.position = hitPlayerPos;
    }
}
