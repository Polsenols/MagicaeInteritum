using System.Diagnostics;
using System.Security;
using UnityEngine;
using System.Collections;
using MovementEffects;
using System.Collections.Generic;

public class FadeInOutShaderColor : MonoBehaviour
{
    public string ShaderColorName = "_Color";
    public float StartDelay = 0;
    public float FadeInSpeed = 0;
    public float FadeOutDelay = 0;
    public float FadeOutSpeed = 0;

    private Projector projector;
    private Material mat;
    private Color currentColor;
    private float alpha;

    private bool fadeInComplete = false;
    private bool canStart = false;
    private bool canFadeOut = false;
    #region Non-public methods

    void Start()
    {
        projector = GetComponent<Projector>();
        mat = new Material(projector.material);
        projector.material = mat;
        currentColor = mat.GetColor(ShaderColorName);
        currentColor.a = 0;
        
        alpha = currentColor.a;
        StartCoroutine(_StartDelay());
    }

    void Update()
    {
        if (canStart)
        {
            FadeIn();
        }
        if (fadeInComplete && canFadeOut)
        {
            FadeOut();
        }
    }

    private IEnumerator _StartDelay()
    {
        yield return new WaitForSeconds(StartDelay);
        canStart = true;
    }

    private IEnumerator _FadeOutDelay()
    {
        yield return new WaitForSeconds(FadeOutDelay);
        canFadeOut = true;
    }

    private void FadeIn()
    {
        if (alpha <= 1 && !fadeInComplete)
        {
            alpha += Time.deltaTime / FadeInSpeed;
            currentColor.a = alpha;
            mat.SetColor(ShaderColorName, currentColor);
        }
        else
        {
            StartCoroutine(_FadeOutDelay());
            fadeInComplete = true;
        }
    }

    private void FadeOut()
    {
        if (alpha >= 0)
        {
            alpha -= Time.deltaTime / FadeOutSpeed;
            currentColor.a = alpha;
            mat.SetColor(ShaderColorName, currentColor);
        }
    }

    #endregion
}