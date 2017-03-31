using UnityEngine;
using System.Collections;
using MovementEffects;
using System.Collections.Generic;

[System.Serializable]

public class ScreenEffects : SingleTon<ScreenEffects>
{
    protected ScreenEffects() { }


    public void Awake()
    {
        OriginalPos = transform.position;
    }

    public float shakeIntensity = 0.5f;
    public float shakeDecay = 0.02f;
    private float currentShakeIntensity;

    private Vector3 OriginalPos;
    private bool isShakeRunning = false;


    public IEnumerator<float> screenShake()
    {
        if (isShakeRunning == false)
        {
            //OriginalPos = transform.position;
            isShakeRunning = true;
            currentShakeIntensity = shakeIntensity;
            while (currentShakeIntensity > 0)
            {
                transform.position = OriginalPos + Random.insideUnitSphere * currentShakeIntensity;
                currentShakeIntensity -= shakeDecay;
                yield return 0f;
            }

            isShakeRunning = false;
        }
        else
        {
            currentShakeIntensity += shakeIntensity;
        }

    }

}
