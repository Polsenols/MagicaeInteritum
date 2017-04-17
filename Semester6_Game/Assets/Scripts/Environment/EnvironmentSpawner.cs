using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class EnvironmentSpawner : Photon.MonoBehaviour
{

    public GameObject[] GameEvent;
    public int[] EventTimer;
    public Transform EventSpawnPos;

    void Start()
    {
        if (PhotonNetwork.isMasterClient)
        {
            Timing.RunCoroutine(_SpawnEvent(EventSpawnPos.position));
        }
    }


    IEnumerator<float> _SpawnEvent(Vector3 pos)
    {
        for (int i = 0; i < GameEvent.Length; i++)
        {
            yield return Timing.WaitForSeconds(EventTimer[i]);
            PhotonNetwork.Instantiate(GameEvent[i].name, pos, Quaternion.identity, 0);
            yield return 0f;
        }
    }
}