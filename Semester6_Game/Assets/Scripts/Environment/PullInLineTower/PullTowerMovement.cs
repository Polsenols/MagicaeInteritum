using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullTowerMovement : MonoBehaviour
{

    private Vector3 _startPosition;
    public float ossilationFactor = 0.1f;
    public float turnFactor = 20.0f;
    public bool rotateAlso = false;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        if (rotateAlso)
            transform.Rotate(Vector3.up * Time.deltaTime * turnFactor);
        transform.position = _startPosition + new Vector3(0.0f, Mathf.Sin(Time.time) * ossilationFactor, 0.0f);
    }
}
