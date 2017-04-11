using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossLineRender : MonoBehaviour {


    private LineRenderer lineRenderer;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        lineRenderer.SetPosition(0, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z));
	}

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Environmental")
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(1, new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z));
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Environmental")
        {
            lineRenderer.enabled = false;
        }
    }
    
}
