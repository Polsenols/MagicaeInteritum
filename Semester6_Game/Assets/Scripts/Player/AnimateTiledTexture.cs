using UnityEngine;
using System.Collections;

class AnimateTiledTexture : MonoBehaviour
{
    public int columns = 2;
    public int rows = 2;
    public float framesPerSecond = 10f;

    public bool loop = false;
    //the current frame to display
    private int index = -1;
    private bool animationDone;

    void Start()
    {
        //set the tile size of the texture (in UV units), based on the rows and columns
        Vector2 size = new Vector2(1f / columns, 1f / rows);
        GetComponent<Renderer>().sharedMaterial.SetTextureScale("_MainTex", size);
        animationDone = false;

        transform.SetParent(null);

        gameObject.SetActive(false);
    }

    public void UpdatePosition(Vector3 newPos)
    {
        newPos.y += 0.1f;
        index = -1;
        gameObject.transform.position = newPos;
        StartCoroutine(updateTiling());
    }

    private IEnumerator updateTiling()
    {
        animationDone = false;
        while (!animationDone)
        {
            //move to the next index
            index++;
            if (index >= rows * columns)
            {
                if (loop == true)
                {
                    index = 0;
                }
                else
                {
                    animationDone = true;
                    index = (rows * columns) - 1;
                }
            }
                

            //split into x and y indexes
            Vector2 offset = new Vector2((float)index / columns - (index / columns), //x index
                                          (index / columns) / (float)rows);          //y index

            GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);

            yield return new WaitForSeconds(1f / framesPerSecond);
        }
        gameObject.SetActive(false);
    }
}