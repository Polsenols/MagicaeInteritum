using System.Collections;
using System.Collections.Generic;
using MovementEffects;
using UnityEngine;

public class AOE : MonoBehaviour
{

    public float radius;
    public int damage;
    public LayerMask mask;


    public void Push(Rigidbody rb, float force, bool isDistanceBased)
    {
        Vector3 pushDir = rb.transform.position - transform.position;
        if (!isDistanceBased)
            pushDir.Normalize();
        rb.AddForce(pushDir * force, ForceMode.Impulse);
    }

    public void damageNearbyEnemies(int ownerID, Vector3 origin, int damage, float force, bool canPush, bool isDistanceBased)
    {
        Collider[] hitCols = Physics.OverlapSphere(origin, radius, mask);
        foreach (Collider col in hitCols)
        {
            CharacterManager_NET player = col.GetComponent<CharacterManager_NET>();

            if (player.playerID != ownerID)
            {
                if (canPush)
                    Push(col.GetComponent<Rigidbody>(), force, isDistanceBased);

                col.GetComponent<PlayerHealth_NET>().TakeDamage(damage, ownerID, player);                
            }
        }
    }

    public IEnumerator<float> _CleanUp(float duration)
    {
        yield return Timing.WaitForSeconds(duration);
        Destroy(this.gameObject);
    }


}
