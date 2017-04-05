using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePull : SingleImpact {

    public GameObject ForcePullDrag;
    private Transform target;
    private SpellData targetSpellData;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterManager_NET>().playerID != spellData.ownerID())
        {
            target = other.transform;
        }
        Impact(other);
    }

    void OnDestroy()
    {
        if (target != null)
        {
            GameObject go = (GameObject)Instantiate(ForcePullDrag, transform.position, Quaternion.identity);
            ForcePullDrag dragObj = go.GetComponent<ForcePullDrag>();
            dragObj.SetSpellData(spellData);
            dragObj.SetTarget(target);
        }
    }

}
