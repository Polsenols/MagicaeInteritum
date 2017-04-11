using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class AOEImpact : MonoBehaviour
{

    public DamageType damageType = DamageType.Instant;
    public LayerMask mask;
    public bool canPush = false;
    public bool canFreeze = false;
    public bool canDamage = false;
    public bool canCurse = false;
    public bool canSlow = false;
    public bool canLifeSteal = false;
    public bool screenshake = true;

    [Header("Damage over time")]
    public int amountOfTicks = 0;
    public float timeBetweenTicks = 0;

    public enum DamageType
    {
        Instant,
        DOT,
        None
    }


    public void impactNearbyEnemies(Vector3 origin, bool isDistanceBased, float radius, SpellData spellData)
    {
        Collider[] hitCols = Physics.OverlapSphere(origin, radius, mask);
        if (hitCols.Length > 0)
        {
            foreach (Collider col in hitCols)
            {
                CharacterManager_NET player = col.GetComponent<CharacterManager_NET>();

                if (player.playerID != spellData.ownerID())
                {
                    if (player.m_PhotonView.isMine)
                    {
                        if (canPush)
                            Push(player.GetComponent<Rigidbody>(), isDistanceBased, spellData);

                        if (canFreeze)
                            player.playerHealth().Freeze(spellData.freezeDuration());

                        if (canCurse)
                            player.playerHealth().Curse(spellData.curseDuration(), spellData.curseDmgAdjuster());

                        if (canSlow)
                            player.GetComponent<PlayerMovement>().slowPlayerMovementSpeed(spellData.slowMovementSpeed(), spellData.slowDuration());

                        if (canLifeSteal)
                            spellData.owner.GetComponent<PlayerHealth_NET>().AddLife(spellData.damage() * spellData.lifeStealAmount());

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
                    }
                }
            }
        }

        if (screenshake)
            ScreenEffects.Instance.screenShake();
    }

    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public void Push(Rigidbody rb, bool isDistanceBased, SpellData spellData)
    {        
        Vector3 pushDir = rb.transform.position - transform.position;
        pushDir.y = 0;
        float force = 0;
        if (isDistanceBased) {
            float distance = pushDir.magnitude;
            force = Remap(distance, 0, spellData.knockbackForce(), spellData.radius(), spellData.knockbackForce()*0.2f);
        }
        rb.AddForce(pushDir.normalized * Mathf.Abs(force), ForceMode.Impulse);
    }

    public IEnumerator<float> _CleanUp(float duration)
    {
        yield return Timing.WaitForSeconds(duration);
        Destroy(this.gameObject);
    }
}
