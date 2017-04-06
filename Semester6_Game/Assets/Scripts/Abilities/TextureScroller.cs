using UnityEngine;
using System.Collections;

public class TextureScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5F;
    private Material mat;
    public ScrollAxis scrollAxis = ScrollAxis.X;

    public enum ScrollAxis
    {
        X,
        Y,
        Both
    }

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

        switch (scrollAxis)
        {
            case ScrollAxis.X:
                mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
                break;
            case ScrollAxis.Y:
                mat.SetTextureOffset("_MainTex", new Vector2(0, offset));
                break;
            case ScrollAxis.Both:
                mat.SetTextureOffset("_MainTex", new Vector2(offset, offset));
                break;

        }
    }
}