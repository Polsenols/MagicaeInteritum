using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignCamera : MonoBehaviour {

    //Assign this to the player Prefab to give the camera a target.
	// Use this for initialization
	void Start () {
        PhotonView m_PhotonView = GetComponent<PhotonView>();

        if (m_PhotonView.isMine)
        {
            if (Camera.main.GetComponent<CameraFollow>() != null)
            {
                Camera.main.GetComponent<CameraFollow>().player = gameObject;
            }
        }

    }
}
