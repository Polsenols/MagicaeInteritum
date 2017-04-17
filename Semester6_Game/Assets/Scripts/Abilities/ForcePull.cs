using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePull : SingleImpact
{

    public GameObject ForcePullDrag;
    private Transform target;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ability"))
        {
            if (spellData.ownerID() == other.GetComponent<SpellData>().ownerID())
            {
                return;
            }
            spellData.owner.SendAbilityHit(spellData.InstantiateID(), true, true);
            spellData.AbilityImpactEffect();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Environmental"))
        {
            spellData.owner.SendAbilityHit(spellData.InstantiateID(), true, true);
            spellData.AbilityImpactEffect();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            if (other.GetComponent<CharacterManager_NET>().playerID != spellData.ownerID())
            {
                target = other.transform;
                GameObject go = (GameObject)Instantiate(ForcePullDrag, transform.position, Quaternion.identity);
                ForcePullDrag dragObj = go.GetComponent<ForcePullDrag>();
                dragObj.SetSpellData(spellData);
                dragObj.SetTarget(target);
                Impact(other);
            }
        }
    }

}