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
            distanceTravelled += Vector3.SqrMagnitude(transform.position - lastPos);
            //rb.AddForce(spellDir * spellData.speed());
            transform.position += spellDir * spellData.speed() * Time.deltaTime;
            lastPos = transform.position;
            if (distanceTravelled >= spellData.travelDistance()*spellData.travelDistance())
            {
                Destroy(this.gameObject);
            }
        }
	}

    public void SetSpellDirection(Vector3 castOrigin, Vector3 targetPos)
    {
        castOrigin.y = 0;
        targetPos.y = 0;
        spellDir = Vector3.Normalize(targetPos - castOrigin);
        isFired = true;
    }
}
