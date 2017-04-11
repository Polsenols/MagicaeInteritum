using UnityEngine;
using System.Collections;
using MovementEffects;
using System.Linq;

public class LightAnimation : MonoBehaviour
{
    private Light lightSystem;
    public AnimationCurve lightScale;
    public float timeScale = 1;
    public float intensity = 5;
    private float startTime;

    void Start()
    {
        lightSystem = GetComponent<Light>();
        startTime = Time.time;
    }

    void Update()
    {
        float timeFromBegin = Time.time - startTime;
        float lightScaleFactor = lightScale.Evaluate(timeFromBegin / timeScale) * intensity;
        lightSystem.intensity = lightScaleFactor;
    }
}
