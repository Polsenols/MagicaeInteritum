using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserOscillation : MonoBehaviour {
    public float multiplier = 1;
    public float speed = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = new Vector3(Mathf.Abs(Mathf.Sin(Time.time*speed)*multiplier)+0.5f, Mathf.Abs(Mathf.Sin(Time.time * speed) * multiplier) + 0.5f, transform.localScale.z);
	}
}
