using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollControl : MonoBehaviour {

    public Rigidbody hip;
    public float force;
    void Start()
    {
        PushRagdoll(transform.position);
    }

    public void PushRagdoll(Vector3 pos)
    {
        pos.y = -0.5f;
        hip.AddExplosionForce(force, pos, 10.0f);
    }
}
