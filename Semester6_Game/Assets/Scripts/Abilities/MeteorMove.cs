using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMove : MonoBehaviour
{

    public float heightOffset = 10;
    public float explosionY_Offset = -5;
    public float speed = 10;
    public GameObject explosion, cracks;
    public ParticleSystem debrisParticleSys, smokeParticleSys;

    private bool meteorExploded = false;

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(transform.position.x, heightOffset, transform.position.z);
        smokeParticleSys.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= explosionY_Offset && !meteorExploded)
        {
            meteorExploded = true;
            explosion.SetActive(true);
            cracks.SetActive(true);
            debrisParticleSys.Play();
        }
        transform.Translate(Vector3.down * Time.fixedDeltaTime * speed, Space.World);
        //transform.Rotate(Vector3.right * Time.deltaTime * 1000.0f);

    }
}
