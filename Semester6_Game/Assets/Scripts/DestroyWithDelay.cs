using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithDelay : MonoBehaviour {


    public float destroyDelay = 3.0f;
	void Start () {
        Destroy(gameObject, destroyDelay);
	}

}
