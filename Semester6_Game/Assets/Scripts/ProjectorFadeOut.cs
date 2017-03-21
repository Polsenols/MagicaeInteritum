using UnityEngine;
using System.Collections;
using MovementEffects;
using System.Collections.Generic;

public class ProjectorFadeOut : MonoBehaviour {

    private float startHeight;
    [SerializeField]
    private float targetDistance;
    [SerializeField]
    private float fadeOutDelay;

    void Awake()
    {
        startHeight = transform.position.y;
        Timing.RunCoroutine(_fadeOut());
    }
	
    private IEnumerator<float> _fadeOut(){
        yield return Timing.WaitForSeconds(fadeOutDelay);
        while(transform.position.y < startHeight + targetDistance)
        {
            transform.position += Vector3.up;
            yield return 0f;
        }
        Destroy(gameObject);
    }

}
