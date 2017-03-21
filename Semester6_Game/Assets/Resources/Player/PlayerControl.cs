using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private Rigidbody rigidBody;
    private Vector3 targetPos;

    void Awake()
    {
        targetPos = transform.position;
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                targetPos = hit.point;
                Debug.Log(hit.point);
            }

        }

        Vector3 moveDir = Vector3.Normalize(targetPos - transform.position);
        rigidBody.velocity = moveDir * 10.0f;
    }
}
