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
            GetComponent<AOEImpact>().damageNearbyEnemies(transform.position, true);
            meteorExploded = true;
            explosion.SetActive(true);
            cracks.SetActive(true);
            debrisParticleSys.Play();
            Timing.RunCoroutine(_CleanUp(2));
        }
        meteorMesh.Translate(Vector3.down * Time.fixedDeltaTime * speed, Space.World);
    }

    public IEnumerator<float> _CleanUp(float duration)
    {
        yield return Timing.WaitForSeconds(duration);
        Destroy(this.gameObject);
    }

}
