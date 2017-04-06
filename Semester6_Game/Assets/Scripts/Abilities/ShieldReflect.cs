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
            if (spellData.owner.m_photonView.isMine && enemySpellData.ownerID() != spellData.ownerID())
            {
                Vector3 collisionPoint = other.transform.position;
                Vector3 normal = collisionPoint - transform.position;
                Vector3 reflectDir = Vector3.Reflect(other.GetComponent<SpellMovement>().GetSpellDir(), normal);
                reflectDir.Normalize();
                Vector3 pointOnDir = transform.position + reflectDir * 150f;
                enemySpellData.owner.SetSpellDirection(enemySpellData.InstantiateID(), collisionPoint, pointOnDir, spellData.ownerID());
            }
        }
    }
}
