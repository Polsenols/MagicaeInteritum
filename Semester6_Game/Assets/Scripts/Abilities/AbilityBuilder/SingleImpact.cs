using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpellData))]
public class SingleImpact : MonoBehaviour {

    public DamageType damageType = DamageType.Instant;
    public bool canPush = false;
    public bool canFreeze = false;
    public bool canSlow = false;
    public GameObject impactEffect;
    public SpellData spellData;

    [Header("Damage over time")]
    public int amountOfTicks = 0;
    public float timeBetweenTicks = 0;

    public enum DamageType
    {
        Instant,
        DOT,
        None
    }

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
        if (other.CompareTag("Ability"))
        {
            if (spellData.ownerID() == other.GetComponent<SpellData>().ownerID())
            {
                return;
            }
            spellData.owner.SendAbilityHit(spellData.InstantiateID());
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Environmental"))
        {
            spellData.owner.SendAbilityHit(spellData.InstantiateID());
            Destroy(this.gameObject);
        }
        else
        {
            CharacterManager_NET player = other.GetComponent<CharacterManager_NET>();
            if (player.playerID != spellData.ownerID() && player.m_PhotonView.isMine)
            {
                switch (damageType)
                {
                    case DamageType.DOT:
                        player.playerHealth().TakeDamageOverTime(amountOfTicks, spellData.damage(), timeBetweenTicks, player, spellData.ownerID());
                        break;
                    case DamageType.Instant:
                        player.playerHealth().TakeDamage(spellData.damage(), spellData.ownerID(), player);
                        break;
                    case DamageType.None:
                        break;
                }

                if (canPush)
                    Push(other.GetComponent<Rigidbody>(), spellData.knockbackForce(), false);
                if (canFreeze)
                    other.GetComponent<PlayerHealth_NET>().Freeze();
                if (canSlow)
                    other.GetComponent<PlayerMovement>().slowPlayerMovementSpeed(spellData.slowMovementSpeed(), spellData.slowDuration());

                spellData.owner.SendAbilityHit(spellData.InstantiateID());
                Destroy(this.gameObject);
            }
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
