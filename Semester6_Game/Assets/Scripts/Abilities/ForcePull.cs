using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePull : SingleImpact {

    public GameObject ForcePullDrag;
    private Transform target;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
