using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollControl : MonoBehaviour {

    public Rigidbody hip;
    void Start()
    {
        //PushRagdoll(transform.position);
    }

    public void PushRagdoll(Vector3 pos, float force)
    {
        Debug.Log("Pushing ragdoll boi force: " + force);
        pos.y = -0.5f;
        hip.AddExplosionForce(force, pos, 10);
    }
}
