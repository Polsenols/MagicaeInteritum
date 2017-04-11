using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class EnvironmentSpawner : Photon.MonoBehaviour {

    public GameObject LaserTower;
    public Transform laserSpawnPos;
	// Use this for initialization
	void Start () {
        if (PhotonNetwork.isMasterClient)
        {
            Timing.RunCoroutine(_SpawnElement(LaserTower, 8.0f, laserSpawnPos.position));
        }
	}
	
    IEnumerator<float> _SpawnElement(GameObject element, float waitTime, Vector3 pos)
    {
        yield return Timing.WaitForSeconds(waitTime);
        PhotonNetwork.Instantiate(element.name, pos, Quaternion.identity, 0);
        //LaserTower.SetActive(true);
        yield return 0f;
    }
}
