using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldReflect : MonoBehaviour
{

    SpellData spellData;
    Transform originTransform;
    // Use this for initialization
    void Start()
    {
        spellData = GetComponent<SpellData>();
        originTransform = spellData.owner.transform;
    }

    void Update()
    {
        transform.position = originTransform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ability"))
        {
            SpellData enemySpellData = other.GetComponent<SpellData>();
            if (enemySpellData.ownerID() != spellData.ownerID())
            {
                if (spellData.owner.m_photonView.isMine)
                {
                    Vector3 collisionTarget = other.transform.position;
                    Vector3 normal = collisionTarget - transform.position;
                    Vector3 reflectDir = Vector3.Reflect(other.GetComponent<SpellMovement>().GetSpellDir(), normal);
                    reflectDir.Normalize();
                    Vector3 pointOnDirection = transform.position + reflectDir * 100f;
                    spellData.owner.ShoutSpell(enemySpellData.spellID(), collisionTarget, pointOnDirection);
                    enemySpellData.owner.SendAbilityHit(enemySpellData.InstantiateID(), false);
                }
                Destroy(enemySpellData.gameObject);
            }
            
            /*Debug.Log("Ability reflected!");
            SpellData enemySpellData = other.GetComponent<SpellData>();
            Vector3 targetPos = enemySpellData.owner.transform.position;
            enemySpellData.setOwnerID(spellData.ownerID());
            enemySpellData.setOwner(spellData.owner);
            other.GetComponent<SpellMovement>().m_startPosition = transform.position;
            other.GetComponent<SpellMovement>().SetSpellDirection(transform.position, targetPos);*/
        }
    }
}
