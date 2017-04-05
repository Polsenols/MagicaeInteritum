using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonDartbehaviour : MonoBehaviour {

    void Update()
    {
        transform.Rotate(Vector3.right, Time.deltaTime * 500f);
    }
}
