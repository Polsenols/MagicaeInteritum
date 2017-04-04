using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpellData))]
public class SingleImpact : MonoBehaviour {

    public bool doesDamage = false;
    public bool canPush = false;
    public GameObject impactEffect;
    public SpellData spellData;

    void Start()
    {
        spellData = GetComponent<SpellData>();
    }

    void OnTriggerEnter(Collider other)
    {
        Impact(other);
    }

    public void Impact(Collider other)
    {
        CharacterManager_NET player = other.GetComponent<CharacterManager_NET>();
        if (player.playerID != spellData.ownerID() && player.m_PhotonView.isMine)
        {
            other.GetComponent<PlayerHealth_NET>().TakeDamage(spellData.damage(), spellData.ownerID(), player);
            if (canPush)
            {
                Push(other.GetComponent<Rigidbody>(), spellData.knockbackForce(), false);
            }
            spellData.owner.SendAbilityHit(spellData.InstantiateID());
            Destroy(this.gameObject);
        }
    }

    public void Push(Rigidbody rb, float force, bool isDistanceBased)
    {
        Vector3 pushDir = rb.transform.position - transform.position;
        pushDir.y = 0;
        if (isDistanceBased)
            force -= pushDir.sqrMagnitude;
        rb.AddForce(pushDir.normalized * Mathf.Abs(force), ForceMode.Impulse);
    }

    void OnDestroy()
    {
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
    }
}
