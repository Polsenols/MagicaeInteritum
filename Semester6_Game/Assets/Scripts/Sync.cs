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
    public bool teleportIfDistanceGreaterThan;
    public float teleportDistance;
    Vector3 m_NetworkPosition;
    

    void Awake () {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
        if (!pv.isMine)
        {
            Destroy(rb);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (!pv.isMine)
        {
            UpdateTransform();
        }
	}

    void UpdateTransform()
    {
        transform.position = Vector3.Lerp(transform.position, trueLoc, Time.deltaTime * lerpSpeed);
        UpdateNetworkedRotation();
        if (teleportIfDistanceGreaterThan)
        {
            if (Vector3.Distance(transform.position, trueLoc) > teleportDistance)
            {
                transform.position = GetNetworkPosition();
            }
        }
    }

    Vector3 GetNetworkPosition()
    {
        return m_NetworkPosition;
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
                m_NetworkPosition = trueLoc;
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
