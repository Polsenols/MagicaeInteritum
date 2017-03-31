    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync : Photon.MonoBehaviour{

    Vector3 trueLoc;
    Quaternion trueRot;
    PhotonView pv;
    public float lerpSpeed;

    void Awake () {
        pv = GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!pv.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, trueLoc, Time.deltaTime * lerpSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, trueRot, Time.deltaTime * lerpSpeed);
        }
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //we are reicieving data
        if (stream.isReading)
        {
            //receive the next data from the stream and set it to the truLoc varible
            if (!pv.isMine)
            {//do we own this photonView?????
                this.trueLoc = (Vector3)stream.ReceiveNext(); //the stream send data types of "object" we must typecast the data into a Vector3 format
            }
        }
        //we need to send our data
        else
        {
            //send our posistion in the data stream
            if (pv.isMine)
            {
                stream.SendNext(transform.position);
            }
        }
    }
}
