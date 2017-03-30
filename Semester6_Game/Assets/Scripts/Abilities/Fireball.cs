using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    private AOEImpact impact;
    private LayerMask layermask;
	// Use this for initialization
	void Start () {
        impact = GetComponent<AOEImpact>();
	}
	
	void OnTriggerEnter(Collider other)
    {
        impact.damageNearbyEnemies(transform.position, true);
        Destroy(this.gameObject);
    }
}
