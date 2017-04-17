using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class releaseSpikes : MonoBehaviour
{
    public Transform spike;
    public float waitTime = 0.5f;
    private AudioSource _audio;
    // animate the game object from -1 to +1 and back
    float minimum;
    float maximum;
    float offset = 0.6f;

    bool activateTrap = false;

    bool goingUP = true;

    int keepCount = 0;

    // starting value for the Lerp    
    static float t = 0.0f;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
        minimum = spike.transform.position.y;
        maximum = minimum + offset;
    }

    void Update()
    {
        // animate the position of the game object...
        if (activateTrap)
        {
            if (keepCount == 2)
            {
                activateTrap = false;
                keepCount = 0;
            }

            spike.transform.position = new Vector3(spike.transform.position.x, Mathf.Lerp(minimum, maximum, t), spike.transform.position.z);

            if (goingUP)
            {
                t += 10 * Time.deltaTime;
            }
            else
            {
                t += 0.5f * Time.deltaTime;
            }

            // now check if the interpolator has reached 1.0
            // and swap maximum and minimum so game object moves
            // in the opposite direction.
            if (t > 1.0f)
            {
                if (goingUP == true)
                {
                    goingUP = false;
                }
                else
                {
                    goingUP = true;
                }
                keepCount++;

                float temp = maximum;
                maximum = minimum;
                minimum = temp;
                t = 0.0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Timing.RunCoroutine(_ActivateTrap());
    }

    void OnTriggerStay(Collider other)
    {
        if(activateTrap == true)
        {
            other.GetComponent<PlayerHealth_NET>().TakeDamage(100, -1, null, transform, 5.0f);
        }
    }

    IEnumerator<float> _ActivateTrap()
    {
        yield return Timing.WaitForSeconds(waitTime);
        _audio.Play();  
        activateTrap = true;
    }
}
