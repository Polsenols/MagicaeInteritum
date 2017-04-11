using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMovement : MonoBehaviour
{

    public bool isFired = false;
    private Vector3 spellDir = Vector3.zero;
    private SpellData spellData;
    private double m_creationTime = 0;
    public Vector3 m_startPosition = Vector3.zero;
    private float timeSinceStart;

    public bool rotateTowardsDirection = false;

    void Start()
    {
        spellData = GetComponent<SpellData>();
        transform.position = m_startPosition;
        //transform.position += spellDir * 1.25f;
    }

    void FixedUpdate()
    {
        if (isFired)
        {
            timeSinceStart += Time.deltaTime;
            if (timeSinceStart > spellData.travelDuration())
            {
                spellData.owner.SendAbilityHit(spellData.InstantiateID(), true, true);
                spellData.AbilityImpactEffect();
                Destroy(this.gameObject);
            }

            float timePassed = (float)(PhotonNetwork.time - m_creationTime);
            transform.position = m_startPosition + spellDir * spellData.speed() * timePassed;

            if (rotateTowardsDirection)
                RotateTowardsDir();
        }
    }

    private void RotateTowardsDir()
    {
        transform.rotation = Quaternion.LookRotation(spellDir);
    }

    public void SetSpellDirection(Vector3 castOrigin, Vector3 targetPos)
    {
        timeSinceStart = 0;
        castOrigin.y = 0;
        targetPos.y = 0;
        spellDir = Vector3.Normalize(targetPos - castOrigin);
        isFired = true;
    }

    public Vector3 GetSpellDir()
    {
        return spellDir;
    }

    public void SetCreationTime(double createTime)
    {
        m_creationTime = createTime;
    }

    public void SetStartPosition(Vector3 startPos)
    {
        m_startPosition = startPos;
    }
}
    