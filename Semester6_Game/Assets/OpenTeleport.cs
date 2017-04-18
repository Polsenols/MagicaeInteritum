using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTeleport : MonoBehaviour
{

    public float openTeleportDelay = 20;
    public GameObject teleporter;

    private TextMesh text;
    private float startTime = 0;
    private float currentTime;

    void Start()
    {
        text = GetComponent<TextMesh>();
        currentTime = 0;
        startTime = openTeleportDelay;
    }


    void Update()
    {
        currentTime += Time.deltaTime;
        text.text = "Teleport opens in: \n" + (Mathf.FloorToInt(openTeleportDelay - currentTime));
        if (currentTime >= openTeleportDelay)
        {
            teleporter.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
