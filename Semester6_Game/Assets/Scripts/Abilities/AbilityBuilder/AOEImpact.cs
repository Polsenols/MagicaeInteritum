using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class AOEImpact : MonoBehaviour {

    private SpellData spellData;
    public LayerMask mask;
    public bool canPush = false;
    public bool screenshake = true;

    void Start()
    {
        spellData = GetComponent<SpellData>();
    }

    public void damageNearbyEnemies(Vector3 origin, bool isDistanceBased)
    {
        Collider[] hitCols = Physics.OverlapSphere(origin, spellData.radius(), mask);
        foreach (Collider col in hitCols)
        {
            CharacterManager_NET player = col.GetComponent<CharacterManager_NET>();

            if (player.playerID != spellData.ownerID())
            {
                if (canPush)
                    Push(player.GetComponent<Rigidbody>(), spellData.knockbackForce(), isDistanceBased);

                player.GetComponent<PlayerHealth_NET>().TakeDamage(spellData.damage(), spellData.ownerID(), player);
            }
        }
        if (screenshake)
            ScreenEffects.Instance.screenShake();
    }

    public void freezeNearbyEnemies(Vector3 origin, bool isDistanceBased)
    {
        Collider[] hitCols = Physics.OverlapSphere(origin, spellData.radius(), mask);
        foreach (Collider col in hitCols)
        {
            CharacterManager_NET player = col.GetComponent<CharacterManager_NET>();

            if (player.playerID != spellData.ownerID())
            {
                if (canPush)
                    Push(col.GetComponent<Rigidbody>(), spellData.knockbackForce(), isDistanceBased);

                col.GetComponent<PlayerHealth_NET>().TakeDamage(spellData.damage(), spellData.ownerID(), player);

                if (col.GetComponent<PlayerHealth_NET>() != null)
                    col.GetComponent<PlayerHealth_NET>().FreezePlayer();

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

    public IEnumerator<float> _CleanUp(float duration)
    {
        yield return Timing.WaitForSeconds(duration);
        Destroy(this.gameObject);
    }
}
