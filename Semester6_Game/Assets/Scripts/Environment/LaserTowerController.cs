using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class LaserTowerController : MonoBehaviour {
    public float rotationSpeed;
    public bool isActive = false;
    public GameObject laser_rays;
    private PhotonView m_photonView;
    private SyncRotation syncRotate;
    public
	// Use this for initialization
	void Start () {
        m_photonView = transform.parent.GetComponent<PhotonView>();
        syncRotate = transform.parent.GetComponent<SyncRotation>();
        if(PhotonNetwork.isMasterClient)
        Timing.RunCoroutine(_ActivateLaser(5f));
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
                Debug.Log("Hit player");
            }
    }

    IEnumerator<float> _ActivateLaser(float waitTime)
    {
        yield return Timing.WaitForSeconds(waitTime);
        laser_rays.SetActive(true);
        syncRotate.StartLasers();
        isActive = true;
    }
}
