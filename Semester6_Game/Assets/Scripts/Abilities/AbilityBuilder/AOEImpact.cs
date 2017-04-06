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
                            Push(player.GetComponent<Rigidbody>(), spellData.knockbackForce(), isDistanceBased);

                        if (canFreeze)
                            player.playerHealth().Freeze();

                        if (canCurse)
                            player.playerHealth().Curse();

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
                    }
                }
            }
        }

        if (screenshake)
            ScreenEffects.Instance.screenShake();
    }

    public void Push(Rigidbody rb, float force, bool isDistanceBased)
    {
        Vector3 pushDir = rb.transform.position - transform.position;
        pushDir.y = 0;
        if (isDistanceBased)
            force -= pushDir.sqrMagnitude;
        rb.AddForce(pushDir.normalized * Mathf.Abs(force), ForceMode.Impulse);
    }

    public IEnumerator<float> _CleanUp(float duration)
    {
        yield return Timing.WaitForSeconds(duration);
        Destroy(this.gameObject);
    }
}
