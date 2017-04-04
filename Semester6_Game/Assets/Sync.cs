    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sync : Photon.MonoBehaviour{

    Vector3 trueLoc;
    Quaternion trueRot;
    PhotonView pv;
    public float lerpSpeed;
    Quaternion m_NetworkRotation;
    Rigidbody rb;

    void Awake () {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
        if (!pv.isMine)
        {
            Destroy(rb);
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!pv.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, trueLoc, Time.deltaTime * lerpSpeed);
            UpdateNetworkedRotation();
        }
	}

    void UpdateNetworkedRotation()
    {
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            m_NetworkRotation, 180f * Time.deltaTime
        );
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //we are reicieving data
        if (stream.isReading)
        {
            //receive the next data from the stream and set it to the truLoc varible
            if (!pv.isMine)
            {//do we own this photonView?????
                trueLoc = (Vector3)stream.ReceiveNext();
                m_NetworkRotation = (Quaternion)stream.ReceiveNext();
            }
        }
        //we need to send our data
        else
        {
            //send our posistion in the data stream
            if (pv.isMine)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
            }
        }
    }
}
