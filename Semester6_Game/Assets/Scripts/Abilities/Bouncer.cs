using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bouncer : MonoBehaviour
{

    public int amountOfBounces = 5;
    private int currentBounceCount = 0;
    public DamageType damageType = DamageType.Instant;
    public bool canPush = false;
    public bool canFreeze = false;
    public bool canSlow = false;
    public bool canCurse = false;
    public bool canLifeSteal = false;
    public SpellData spellData;
    public List<PlayerHealth_NET> Players = new List<PlayerHealth_NET>();
    public PlayerHealth_NET lastPlayerTarget;

    [Header("Damage over time")]
    public int amountOfTicks = 0;
    public float timeBetweenTicks = 0;

    public enum DamageType
    {
        Instant,
        DOT,
        None
    }

    void Awake()
    {
        spellData = GetComponent<SpellData>();
    }

    void Start()
    {
        for (int i = 0; i < SpawnManager.Instance.Players.Count; i++)
        {
            Players.Add(SpawnManager.Instance.Players[i]);
        }
        spellData.lastPlayerTarget = spellData.owner.playerHealth;
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
            CharacterManager_NET player = other.GetComponent<CharacterManager_NET>();
            if (player.m_PhotonView.isMine)
            {   
                if (spellData.lastPlayerTarget != player.playerHealth())
                {
                    spellData.owner.SetLastPlayerHit(spellData.InstantiateID(), player.playerHealth().playerID);
                    currentBounceCount++;

                    if (player.playerID != spellData.ownerID())
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
                            other.GetComponent<PlayerHealth_NET>().Freeze(spellData.freezeDuration());
                        if (canSlow)
                            other.GetComponent<PlayerMovement>().slowPlayerMovementSpeed(spellData.slowMovementSpeed(), spellData.slowDuration());
                        if (canLifeSteal)
                            spellData.owner.GetComponent<PlayerHealth_NET>().AddLife(spellData.damage() * spellData.lifeStealAmount());
                        if (canCurse)
                            player.playerHealth().Curse(spellData.curseDuration(), spellData.curseDmgAdjuster());
                    }

                    if (currentBounceCount >= amountOfBounces)
                    {
                        spellData.owner.SendAbilityHit(spellData.InstantiateID(), true, true);
                        Destroy(this.gameObject);
                        spellData.AbilityImpactEffect();
                    }
                    AssignNewDirection();
                }
            }
        }
    }

    void AssignNewDirection()
    {
        spellData.owner.SendAbilityHit(spellData.InstantiateID(), true, false);
        spellData.AbilityImpactEffect();
        spellData.owner.SetSpellDirection(spellData.InstantiateID(), transform.position, GetClosestEnemy(), spellData.ownerID());
    }

    Vector3 GetClosestEnemy()
    {
        Vector3 closestPos = Vector3.one;
        float closestDistance = 9999;
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].gameObject.activeSelf && Players[i] != spellData.lastPlayerTarget)
            {
                float distance = Vector3.Distance(Players[i].transform.position, transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPos = Players[i].transform.position;
                }
            }
        }
        return closestPos;
    }


    public void Push(Rigidbody rb, float force, bool isDistanceBased)
    {
        Vector3 pushDir = rb.transform.position - transform.position;
        pushDir.y = 0;
        if (isDistanceBased)
            force -= pushDir.sqrMagnitude;
        rb.AddForce(pushDir.normalized * Mathf.Abs(force), ForceMode.Impulse);
    }
}
