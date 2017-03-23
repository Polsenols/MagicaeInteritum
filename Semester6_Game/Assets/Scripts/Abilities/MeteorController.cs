using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class MeteorController : AOE
{
    public float heightOffset = 10;
    public float explosionY_Offset = -5;
    public float speed = 10;
    public float force;
    public GameObject explosion, cracks;
    public ParticleSystem debrisParticleSys, smokeParticleSys;
    public Transform meteorMesh;

    public int ownerID = 0;

    private bool meteorExploded = false;

    // Use this for initialization
    void Start()
    {
        meteorMesh.position = new Vector3(transform.position.x, heightOffset, transform.position.z);
        smokeParticleSys.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (meteorMesh.position.y <= explosionY_Offset && !meteorExploded)
        {
            damageNearbyEnemies(ownerID,transform.position, damage, force, true, true);
            meteorExploded = true;
            explosion.SetActive(true);
            cracks.SetActive(true);
            debrisParticleSys.Play();
            Timing.RunCoroutine(_CleanUp(3.0f));
        }
        meteorMesh.Translate(Vector3.down * Time.fixedDeltaTime * speed, Space.World);
    }

}
