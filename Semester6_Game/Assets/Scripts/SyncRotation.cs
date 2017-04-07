using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncRotation : Photon.PunBehaviour {

    PhotonView pv;
    public Transform rotateObj;
    public float lerpSpeed;
    Quaternion m_NetworkRotation;
    public GameObject laserVisual;
    // Use this for initialization
    void Start () {
        pv = GetComponent<PhotonView>();
    }
	
	// Update is called once per frame
	void Update () {
        if(!pv.isMine)
        UpdateNetworkedRotation();
	}

    void UpdateNetworkedRotation()
    {
        rotateObj.rotation = Quaternion.Lerp(
            rotateObj.rotation,
            m_NetworkRotation, lerpSpeed * Time.deltaTime
        );
    }

    public void StartLasers()
    {
        pv.RPC("ActivateLaser",PhotonTargets.AllBuffered);
    }

    [PunRPC]
    void ActivateLaser()
    {
        laserVisual.SetActive(true);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //we are reicieving data
        if (stream.isReading)
        {
            //receive the next data from the stream and set it to the truLoc varible
            if (!pv.isMine)
            {//do we own this photonView?????
                m_NetworkRotation = (Quaternion)stream.ReceiveNext();
            }
        }
        //we need to send our data
        else
        {
            //send our posistion in the data stream
            if (pv.isMine)
            {
                stream.SendNext(rotateObj.rotation);
            }
        }
    }
}
