using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeEmissionOfMaterial : MonoBehaviour
{

    public Color baseColor = Color.white;

    void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;

        float floor = 0.3f;
        float ceiling = 1.0f;
        float emission = floor + Mathf.PingPong(Time.time, ceiling - floor);

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

        mat.SetColor("_EmissionColor", finalColor);
    }
}