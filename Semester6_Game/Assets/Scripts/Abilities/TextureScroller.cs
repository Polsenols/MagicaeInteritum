using UnityEngine;
using System.Collections;

public class TextureScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5F;
    private Material mat;
    void Start()
    {
        if (GetComponent<Renderer>() != null)
        {
            mat = GetComponent<Renderer>().material;
        }
        else
        {
            mat = GetComponent<LineRenderer>().material; 
        }
    }
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}