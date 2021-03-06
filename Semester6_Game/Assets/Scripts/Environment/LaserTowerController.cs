﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class LaserTowerController : MonoBehaviour {
    public float rotationSpeed;
    public float duration;
    public bool isActive = false;
    public GameObject laser_rays;
    private SyncRotation syncRotate;
    public
	// Use this for initialization
	void Start () {
        syncRotate = transform.parent.GetComponent<SyncRotation>();
        if(PhotonNetwork.isMasterClient)
        Timing.RunCoroutine(_ActivateLaser(5f,duration));
	}
	
	// Update is called once per frame
	void Update () {
        if(isActive && PhotonNetwork.isMasterClient)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
            PhotonView m_photonView = other.GetComponent<PhotonView>();
            if (m_photonView.isMine)
            {
                PlayerHealth_NET playerHealth = other.GetComponent<PlayerHealth_NET>();
                playerHealth.TakeDamage(playerHealth.maxHealth, -1, null);
            }
    }

    IEnumerator<float> _ActivateLaser(float waitTime, float duration)
    {
        yield return Timing.WaitForSeconds(waitTime);
        laser_rays.SetActive(true);
        syncRotate.StartLasers();
        isActive = true;
        yield return Timing.WaitForSeconds(duration);
        PhotonNetwork.Destroy(transform.parent.gameObject);
    }
}
