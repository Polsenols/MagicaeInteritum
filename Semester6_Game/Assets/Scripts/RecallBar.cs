using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecallBar : MonoBehaviour
{

    private float maxRecallDuration;
    private float currentRecallDuration;
    private Image recallFill;
    public Color lerpedColor = Color.white;
    public Color learpTo;

    public TeleportToShop teleportInfo;

    // Use this for initialization
    void Start()
    {
        recallFill = GetComponent<Image>();
        recallFill.fillAmount = 0;
        recallFill.color = Color.white;
        maxRecallDuration = teleportInfo.recallCastDuration;
        currentRecallDuration = teleportInfo.currentRecallDuration();
    }
    
    void Update()
    {
        lerpedColor = Color.Lerp(Color.white, learpTo, Mathf.PingPong(Time.time, 1));

        recallFill.color = lerpedColor;
        
        currentRecallDuration = teleportInfo.currentRecallDuration();
        recallFill.fillAmount = currentRecallDuration / maxRecallDuration;
    }
}