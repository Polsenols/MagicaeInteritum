using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class Icespikes : AOEImpact {

    SpellData spellData;
    public GameObject icespikes;
    public float startDelay;
    public AudioClip impactSound;
    
	void Start () {
        spellData = GetComponent<SpellData>();
        Timing.RunCoroutine(_StartEvent());
	}

    IEnumerator<float> _StartEvent()
    {
        spellData.audio.PlayOneShot(impactSound);
        yield return Timing.WaitForSeconds(startDelay);
        icespikes.SetActive(true);
        impactNearbyEnemies(transform.position, false, spellData.radius(),spellData);
    }
	
    
}
