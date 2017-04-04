using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class LaserTowerController : MonoBehaviour {
    public float rotationSpeed;
    public bool isActive = false;
    public GameObject[] laser_rays;
	// Use this for initialization
	void Start () {
        Timing.RunCoroutine(_ActivateLaser(10f));
	}
	
	// Update is called once per frame
	void Update () {
        if(isActive)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            PhotonView test = other.GetComponent<PhotonView>();
            if (test.isMine)
            {
                PlayerHealth_NET playerHealth = other.GetComponent<PlayerHealth_NET>();
                playerHealth.TakeDamage(playerHealth.maxHealth, -1, null);
                Debug.Log("Hit player");
            }

        }
    }

    IEnumerator<float> _ActivateLaser(float waitTime)
    {
        yield return Timing.WaitForSeconds(waitTime);
        for (int i = 0; i < laser_rays.Length; i++)
        {
            laser_rays[i].SetActive(true);
        }
        isActive = true;
    }
}
