using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpellData))]
public class ForceWallController : MonoBehaviour
{

    public DamageType damageType = DamageType.Instant;
    public bool canPush = false;
    public bool canFreeze = false;
    public bool canSlow = false;
    public bool canLifeSteal = false;
    public bool canCurse = false;
    public bool destroyOnImpact = true;
    public bool canSwapPos = false;
    public SpellData spellData;
    public Transform pushOrigin;

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

    void OnTriggerStay(Collider other)
    {
        Impact(other);
    }



    public void Impact(Collider other)
    {
        if (other.CompareTag("Ability"))
        {
            SpellData spell = other.GetComponent<SpellData>();
            spell.owner.SendAbilityHit(spell.InstantiateID(), true, true);
            spell.AbilityImpactEffect();
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Environmental"))
        {
            spellData.owner.SendAbilityHit(spellData.InstantiateID(), true, true);
            spellData.AbilityImpactEffect();
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            CharacterManager_NET player = other.GetComponent<CharacterManager_NET>();
            if (player.m_PhotonView.isMine)
            {
                if (player.playerID != spellData.ownerID())
                {
                    switch (damageType)
                    {
                        case DamageType.DOT:
                            player.playerHealth().TakeDamageOverTime(amountOfTicks, spellData.damage(), timeBetweenTicks, player, spellData.ownerID());
                            break;
                        case DamageType.Instant:
                            player.playerHealth().TakeDamage(spellData.damage(), spellData.ownerID(), player, transform, spellData.knockbackForce());
                            break;
                        case DamageType.None:
                            break;
                    }

                    if (canPush)
                        Push(other.GetComponent<Rigidbody>(), spellData.knockbackForce(), false);

                    if (canFreeze)
                        other.GetComponent<PlayerHealth_NET>().Freeze(spellData.freezeDuration());

                    if (canSlow)
                        other.GetComponent<PlayerMovement>().slowPlayerMovementSpeed(spellData.slowMovementSpeed(), spellData.slowDuration());

                    if (canLifeSteal)
                        spellData.owner.GetComponent<PlayerHealth_NET>().AddLife(spellData.damage() * spellData.lifeStealAmount());

                    if (canCurse)
                        player.playerHealth().Curse(spellData.curseDuration(), spellData.curseDmgAdjuster());
                    if (canSwapPos)
                    {
                        Vector3 hitPlayerPos = other.transform.position;
                        other.transform.position = spellData.owner.transform.position;
                        spellData.owner.transform.position = hitPlayerPos;
                    }

                    spellData.AbilityImpactEffect();

                }
                else
                {
                    return;
                }
            }
        }
    }

    public void Push(Rigidbody rb, float force, bool isDistanceBased)
    {
        Vector3 pushDir = rb.transform.position - pushOrigin.position;
        pushDir.y = 0;
        if (isDistanceBased)
            force -= pushDir.sqrMagnitude;
        rb.AddForce(pushDir.normalized * Mathf.Abs(force), ForceMode.Impulse);
    }
}
