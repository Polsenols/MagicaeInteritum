using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class MeteorController : MonoBehaviour
{
    public float heightOffset = 10;
    public float speed;
    public float explosionY_Offset = -5;
    public GameObject explosion, cracks;
    public ParticleSystem debrisParticleSys, smokeParticleSys;
    public Transform meteorMesh;
    public AudioSource meteorAudio;
    private SpellData spellData;
    private AOEImpact aoeImpact;
    

    private bool meteorExploded = false;

    // Use this for initialization
    void Start()
    {
        aoeImpact = GetComponent<AOEImpact>();
        spellData = GetComponent<SpellData>();
        meteorMesh.position = new Vector3(transform.position.x, heightOffset, transform.position.z);
        smokeParticleSys.Play();
    }
    
    void Update()
    {
        if (meteorMesh.position.y <= explosionY_Offset && !meteorExploded)
        {
            aoeImpact.impactNearbyEnemies(transform.position, true, spellData.radius(),spellData);
            meteorExploded = true;
            explosion.SetActive(true);
            //cracks.SetActive(true); Disabled until we find better looking texture
            debrisParticleSys.Play();
            meteorAudio.Stop();
            Timing.RunCoroutine(_CleanUp(2));
            Timing.RunCoroutine(ScreenEffects.Instance.screenShake());
        }
        meteorMesh.Translate(Vector3.down * Time.deltaTime * speed, Space.World);
    }

    public IEnumerator<float> _CleanUp(float duration)
    {
        yield return Timing.WaitForSeconds(duration);
        Destroy(this.gameObject);
    }

}
