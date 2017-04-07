using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePullDrag : MonoBehaviour
{

    private SpellData spellData;
    private Transform targetTransform;
    private PlayerHealth_NET targetHealth;
    private CharacterManager_NET charMan;
    private Rigidbody targetRb;
    private Vector3 origin, target;
    private bool canPullTarget = false;
    private Vector3 pullDir;
    private float timestamp = 0;
    private float time = 0;

    #region public vars
    public LineRenderer lr_one, lr_two;
    public float pullForce = 0;
    public int damage = 1;
    public float interval = 1;
    public float distanceToBreak = 0;
    public float duration = 0;
    public float heightOffset = 0.63f;
    #endregion

    void Start()
    {
        canPullTarget = targetTransform.GetComponent<SpellManager>().m_photonView.isMine;
        if (canPullTarget)
        {
            targetRb = targetTransform.GetComponent<Rigidbody>();
            charMan = targetTransform.GetComponent<CharacterManager_NET>();
            targetHealth = targetTransform.GetComponent<PlayerHealth_NET>();
        }
        Destroy(this.gameObject, duration);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= duration)
        {
            Destroy(this.gameObject);
        }
        UpdateLineRenderPos();
        if (canPullTarget)
        {
            PullTarget();
            if (timestamp <= Time.time)
            {
                DamageOverTime();
            }
        }
    }

    void DamageOverTime()
    {
        targetHealth.TakeDamage(damage, spellData.ownerID(), charMan);
        timestamp = Time.time + interval;
    }

    void UpdateLineRenderPos()
    {
        if (!targetTransform.gameObject.activeSelf)
        {
            Debug.Log("Target not active - destroyed");
            Destroy(this.gameObject);
        }
        if (pullDir != null)
        {
            if (pullDir.sqrMagnitude > distanceToBreak * distanceToBreak)
            {
                Debug.Log("Too far, broke" + pullDir.sqrMagnitude);
                Destroy(this.gameObject);
            }
        }
        origin = spellData.owner.transform.position;
        target = targetTransform.position;
        origin.y = heightOffset;
        target.y = heightOffset;
        lr_one.SetPosition(0, origin);
        lr_one.SetPosition(1, target);
        lr_two.SetPosition(0, origin);
        lr_two.SetPosition(1, target);

    }

    void PullTarget()
    {
        pullDir = origin - target;
        targetRb.AddForce(pullDir.normalized * pullForce);
    }

    public void SetSpellData(SpellData spellData)
    {
        this.spellData = spellData;
    }

    public void SetTarget(Transform target)
    {
        this.targetTransform = target;
    }
}
