using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    //Give this script to the camera to follow the player

    public GameObject player;
    public float yOffset;
    public float zOffset;

    void LateUpdate()
    {
        if(player != null)
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, player.transform.position.z + zOffset);
    }
}