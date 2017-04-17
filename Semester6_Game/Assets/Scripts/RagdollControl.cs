using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class RagdollControl : MonoBehaviour {

    public Rigidbody hip;
    public SkinnedMeshRenderer render;
    public float dissolveTimer = 3;
    public float dissolveDampener = 1;

    private float currentDissolve = 0;
    private bool startDissolving = false;

    public void PushRagdoll(Vector3 pos, float force)
    {
        pos.y = -0.5f;
        hip.AddExplosionForce(force, pos, 10);
    }

    void Start()
    {
        Timing.RunCoroutine(_StartDissolve());
    }

    void Update()
    {
        if(startDissolving)
            Dissolve();
    }


    void Dissolve()
    {
        if(currentDissolve <= 1)
        {
            currentDissolve += Time.deltaTime / dissolveDampener;
            for (int i = 0; i < render.materials.Length; i++)
            {
                render.materials[i].SetFloat("_SliceAmount", currentDissolve);
            }           
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator<float> _StartDissolve()
    {
        yield return Timing.WaitForSeconds(dissolveTimer);
        startDissolving = true;
    }

}
