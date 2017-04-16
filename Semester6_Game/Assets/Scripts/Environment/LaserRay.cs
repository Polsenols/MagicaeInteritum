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
            if (hit.collider.CompareTag("Player"))
            {
                PhotonView m_photonView = hit.collider.GetComponent<PhotonView>();
                if (m_photonView.isMine)
                {
                    collisionObj.Emit(30);
                    PlayerHealth_NET playerHealth = hit.collider.GetComponent<PlayerHealth_NET>();
                    playerHealth.TakeDamage(playerHealth.maxHealth, -1, null, origin, 10.0f);
                }
            }
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
