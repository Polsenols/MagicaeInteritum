using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPing : MonoBehaviour {

    private Text pingText;
	// Use this for initialization
	void Start () {
        pingText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        pingText.text = "Ping: " + PhotonNetwork.networkingPeer.RoundTripTime.ToString() + " ms";
    }
}
