using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPull : MonoBehaviour
{
    public int layerIndex = 8;
    public float gravityFieldForce = 10f;
    public float effectTime = 5f;
    public float pullRadius = 10f;

    public GameObject _meshDisplay;
    private GameObject meshDisplay;

    void Start()
    {
        meshDisplay = Instantiate(_meshDisplay, transform.position, Quaternion.identity);
        meshDisplay.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Destroy(meshDisplay, effectTime);
        Destroy(gameObject, effectTime);
    }

    void Update()
    {
        transform.Rotate(150 * Time.deltaTime, 150 * Time.deltaTime, 150 * Time.deltaTime); //rotate field

        if (meshDisplay != null)
            meshDisplay.transform.position = transform.position;


        if (meshDisplay != null && meshDisplay.transform.localScale.y < 2f)
            meshDisplay.transform.localScale += new Vector3(meshDisplay.transform.localScale.x, meshDisplay.transform.localScale.y, meshDisplay.transform.localScale.z) * 10f * Time.deltaTime;

        pullGravityField(transform.position, pullRadius, gravityFieldForce); //Pull in
    }


    void pullGravityField(Vector3 center, float radius, float force)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, 1 << layerIndex); //playerLayer is No 8
        int i = 0;
        while (i < hitColliders.Length)
        {
            Transform[] playersTransform = new Transform[hitColliders.Length];
            playersTransform[i] = hitColliders[i].GetComponent<Transform>();
            float[] distance = new float[hitColliders.Length];
            distance[i] = Vector3.Distance(transform.position, playersTransform[i].transform.position);
            hitColliders[i].GetComponent<Rigidbody>().AddForce((transform.position - playersTransform[i].transform.position) * force * Time.smoothDeltaTime);
            i++;
        }
    }
}