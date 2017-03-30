﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColorv2 : MonoBehaviour {


	// Update is called once per frame
	void Update () {

        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;

        float emission = Mathf.PingPong(Time.time * 0.2f, 0.5f - 0.1f) + 0.1f;
        Color baseColor = new Color(0.5f, 0f,0f, 1.0f);  //Replace this with whatever you want for your base color at emission level '1'

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
        mat.SetColor("_EmissionColor", finalColor);

    }
}
