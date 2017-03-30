﻿using UnityEngine;
using System.Collections;

public class TextureScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5F;
    private Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}