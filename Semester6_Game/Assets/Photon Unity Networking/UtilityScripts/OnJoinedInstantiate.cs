using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnJoinedInstantiate : MonoBehaviour
{
    public Transform SpawnPosition;
    public float PositionOffset = 2.0f;
    public GameObject[] PrefabsToInstantiate;   // set in inspector
    public List<string> nameList;

    public void OnJoinedRoom()
    {
        if (this.PrefabsToInstantiate != null)
        {
            foreach (GameObject o in this.PrefabsToInstantiate)
            {
                Debug.Log("Instantiating: " + o.name);

                Vector3 spawnPos = Vector3.up;
                if (this.SpawnPosition != null)
                {
                    spawnPos = this.SpawnPosition.position;
                }

                Vector3 random = Random.insideUnitSphere;
                random.y = 0;
                random = random.normalized;
                Vector3 itempos = spawnPos + this.PositionOffset * random;

                GameObject playerObj = PhotonNetwork.Instantiate(o.name, itempos, Quaternion.identity, 0) as GameObject;
                CharacterManager_NET charMan = playerObj.GetComponent<CharacterManager_NET>();
                charMan.photonView.RPC("SetID", PhotonTargets.AllBuffered, PhotonNetwork.player.ID);
            }
        }
    }
}
