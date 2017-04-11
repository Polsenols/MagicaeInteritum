using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullTowerBehaviour : MonoBehaviour
{
    public int layerIndex = 10;
    public float gravityFieldForce = 10f;
    public float pullRadius = 10f;
    private float timeStamp;
    public bool destroyDiamondAfterTime = true;
    public float delayToDestoryEffect = 10f;

    private SphereCollider myTriggerRadius;

    void Start()
    {
        timeStamp = Time.time;
        if (destroyDiamondAfterTime)
            Destroy(gameObject, delayToDestoryEffect);
        myTriggerRadius = GetComponent<SphereCollider>();
        myTriggerRadius.radius = pullRadius;
    }

    void Update()
    {
        if (Time.time > timeStamp + (delayToDestoryEffect - 0.1f))
        {
            pullRadius = 0.01f;
            myTriggerRadius.radius = pullRadius;
        }
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

/*
// Target Selection: Target first person to enter the collider
if (playersTransform[0] != null)
{
    var lookPos = playersTransform[0].position - transform.position;
    lookPos.y = 0;
    var rotation = Quaternion.LookRotation(lookPos);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * TurnDamper);
 
    lineRenderer.SetPosition(1, new Vector3(playersTransform[0].position.x, playersTransform[0].position.y + 0.5f, playersTransform[0].position.z));
 
    if (distance[0] > radius)
    {
        lineRenderer.enabled = false;
    }
    else
    {
        lineRenderer.enabled = true;
    }
}
*/
