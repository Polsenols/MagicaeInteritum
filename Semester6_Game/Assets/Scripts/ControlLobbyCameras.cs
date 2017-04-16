using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLobbyCameras : MonoBehaviour
{
    public Camera[] myCams = new Camera[4];
    public float switchingDelay = 5.0f;

    private bool[] moveCams = new bool[3];

    private Vector3[] startPositions = new Vector3[4];
    private Quaternion[] startRotations = new Quaternion[4];

    void Start()
    {
        for (int i = 0; i < myCams.Length; i++)
        {
            myCams[i].enabled = false;
            startPositions[i] = myCams[i].transform.position;
            startRotations[i] = myCams[i].transform.rotation;
        }

        for (int i = 0; i < moveCams.Length; i++)
            moveCams[i] = false;

        StartCoroutine("cameraSwitching", switchingDelay);
    }

    void Update()
    {
        //Additional cam 1
        if (moveCams[0])
        {
            myCams[1].transform.position = Vector3.Lerp(myCams[1].transform.position, new Vector3(startPositions[1].x - 10.0f, startPositions[1].y - 11f, startPositions[1].z - 3.0f), Time.deltaTime * 0.1f);
            myCams[1].transform.Rotate(Vector3.up, Time.deltaTime * 2.5f, Space.World);
            myCams[1].transform.Rotate(-Vector3.right, Time.deltaTime * 2.0f, Space.Self);
        }
        else
        {
            myCams[1].transform.position = startPositions[1];
            myCams[1].transform.rotation = startRotations[1];
        }

        //Additional cam 2
        if (moveCams[1])
        {
            myCams[2].transform.position = Vector3.Lerp(myCams[2].transform.position, new Vector3(startPositions[2].x - 5.0f, startPositions[2].y, startPositions[2].z), Time.deltaTime * 0.1f);
        }
        else
        {
            myCams[2].transform.position = startPositions[2];
        }

        //Additional cam 3
        if (moveCams[2])
        {
            myCams[3].transform.position = Vector3.Lerp(myCams[3].transform.position, new Vector3(startPositions[3].x + 5.0f, startPositions[3].y, startPositions[3].z + 4.0f), Time.deltaTime * 0.1f);
            myCams[3].transform.Rotate(-Vector3.right, Time.deltaTime * 1.0f, Space.World);
        }
        else
        {
            myCams[3].transform.position = startPositions[3];
            myCams[3].transform.rotation = startRotations[3];
        }

    }


    IEnumerator cameraSwitching(float delayBetweenSwitches)
    {
        while (true)
        {
            myCams[0].enabled = true;
            yield return new WaitForSeconds(delayBetweenSwitches);
            moveCams[0] = true;
            myCams[1].enabled = true;
            myCams[0].enabled = false;
            yield return new WaitForSeconds(delayBetweenSwitches);
            moveCams[0] = false;
            moveCams[1] = true;
            myCams[2].enabled = true;
            myCams[1].enabled = false;
            yield return new WaitForSeconds(delayBetweenSwitches);
            moveCams[1] = false;
            moveCams[2] = true;
            myCams[3].enabled = true;
            myCams[2].enabled = false;
            yield return new WaitForSeconds(delayBetweenSwitches);
            moveCams[2] = false;
            myCams[3].enabled = false;
        }
    }

}
