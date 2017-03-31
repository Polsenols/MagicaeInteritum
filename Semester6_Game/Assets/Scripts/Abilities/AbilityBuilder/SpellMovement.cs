using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMovement : MonoBehaviour {

    private bool isFired = false;
    private Vector3 spellDir = Vector3.zero;
    private SpellData spellData;
    private Rigidbody rb;
    Vector3 lastPos;
    float distanceTravelled = 0;

    private double m_creationTime = 0;
    private Vector3 m_startPosition = Vector3.zero;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        spellData = GetComponent<SpellData>();
        lastPos = transform.position;
        transform.position += spellDir * 1.25f;
	}
	
	// Update is called once per frame
	void Update () {
        if (isFired)
        {
            /*
            distanceTravelled += Vector3.SqrMagnitude(transform.position - lastPos);
            //rb.AddForce(spellDir * spellData.speed());
            transform.position += spellDir * spellData.speed() * Time.deltaTime;
            lastPos = transform.position;
            if (distanceTravelled >= spellData.travelDistance()*spellData.travelDistance())
            {
                Destroy(this.gameObject);
            }*/

            float timePassed = (float)(PhotonNetwork.time - m_creationTime);
            transform.position = m_startPosition + spellDir * spellData.speed() * timePassed;
        }
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
