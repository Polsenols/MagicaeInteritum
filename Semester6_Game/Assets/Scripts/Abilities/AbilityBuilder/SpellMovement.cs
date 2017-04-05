using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMovement : MonoBehaviour {

    public bool isFired = false;
    private Vector3 spellDir = Vector3.zero;
    private SpellData spellData;
    private Rigidbody rb;
    Vector3 lastPos;
    private float distanceTravelled = 0;
    private double m_creationTime = 0;
    private Vector3 m_startPosition = Vector3.zero;

    public bool rotateTowardsDirection = false; 

	void Start () {
        rb = GetComponent<Rigidbody>();
        spellData = GetComponent<SpellData>();
        lastPos = transform.position;
        transform.position += spellDir * 1.25f;
	}
	
	void Update () {
        if (isFired)
        {
            distanceTravelled += Vector3.SqrMagnitude(transform.position - lastPos);
            lastPos = transform.position;
            float timePassed = (float)(PhotonNetwork.time - m_creationTime);
            transform.position = m_startPosition + spellDir * spellData.speed() * timePassed;           

            if (distanceTravelled >= spellData.travelDistance())
            {
                Destroy(this.gameObject);
            }

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
        castOrigin.y = 0;
        targetPos.y = 0;
        spellDir = Vector3.Normalize(targetPos - castOrigin);
        isFired = true;
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
