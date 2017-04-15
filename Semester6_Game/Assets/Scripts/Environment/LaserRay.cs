using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRay : MonoBehaviour {

    public float maxDistance = 10;
    public ParticleSystem collisionObj;
    public Transform target, origin;
    public LayerMask mask;
    public LineRenderer[] lineRender;

    RaycastHit hit;

	void FixedUpdate () {

        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, mask)){
            collisionObj.transform.position = hit.point;
            target.position = hit.point;
            if(!collisionObj.isPlaying)
                collisionObj.Play();
        }
        else
        {
            collisionObj.Stop();
            target.position = transform.position + transform.forward * maxDistance;
        }

        UpdateLinerenderPos();
	}

    void UpdateLinerenderPos()
    {
        for (int i = 0; i < lineRender.Length; i++)
        {
            lineRender[i].SetPosition(0, origin.position);
            lineRender[i].SetPosition(1, target.position);
        }
    }
}
