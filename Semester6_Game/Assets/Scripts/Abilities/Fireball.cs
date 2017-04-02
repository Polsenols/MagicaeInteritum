using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour {

    private AOEImpact impact;
    private LayerMask layermask;
    private SpellData spellData;
    public GameObject explosion;
	// Use this for initialization
	void Start () {
        impact = GetComponent<AOEImpact>();
        spellData = GetComponent<SpellData>();
	}
	
	void OnTriggerEnter(Collider other)
    {
        CharacterManager_NET player = other.GetComponent<CharacterManager_NET>();
        if (player.playerID != spellData.ownerID())
        {
            impact.damageNearbyEnemies(transform.position, false);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
