using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateLifeStealParticles : MonoBehaviour {

    public Transform relativeTo;
    public float ossilationFactor = 0.1f;
    public float turnFactor = 20.0f;

    // Update is called once per frame
    void Update () {
        transform.position = relativeTo.transform.position + new Vector3(0.0f, Mathf.Sin(Time.time) * ossilationFactor, 0.0f);
    }
}