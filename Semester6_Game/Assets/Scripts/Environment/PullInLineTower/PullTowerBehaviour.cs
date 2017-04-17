using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullTowerBehaviour : MonoBehaviour
{
    public int layerIndex = 10;
    public float gravityFieldForce = 10f;
    public float pullRadius;
    //private float timeStamp;
    public bool destroyDiamondAfterTime = true;
    public float delayToDestoryEffect = 10f;

    //private SphereCollider myTriggerRadius;

    void Start()
    {
        //timeStamp = Time.time;
        if (destroyDiamondAfterTime)
            Destroy(gameObject, delayToDestoryEffect);
        //myTriggerRadius = GetComponent<SphereCollider>();
        //myTriggerRadius.radius = pullRadius;
    }

    void Update()
    {
        /*
        if (Time.time > timeStamp + (delayToDestoryEffect - 0.1f))
        {
            pullRadius = 0.01f;
            myTriggerRadius.radius = pullRadius;
        }
        */
        pullGravityField(transform.position, pullRadius, gravityFieldForce);
    }

    void pullGravityField(Vector3 center, float radius, float force)
    {

        Collider[] hitColliders = Physics.OverlapSphere(center, radius, 1 << layerIndex);

        int i = 0;
        while (i < hitColliders.Length)
        {

            Transform[] playersTransform = new Transform[hitColliders.Length];
            playersTransform[i] = hitColliders[i].GetComponent<Transform>();
            float[] distance = new float[hitColliders.Length];
            distance[i] = Vector3.Distance(transform.position, playersTransform[i].transform.position);

            hitColliders[i].GetComponent<Rigidbody>().AddForce((transform.position - playersTransform[i].transform.position) * force * Time.smoothDeltaTime);

            i++;
        }
    }
}