using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager_NET : Photon.MonoBehaviour {

    private PhotonView m_PhotonView;
    private Animator anim;
    private PlayerMovement playerMove;

	// Use this for initialization
	void Start () {
        m_PhotonView = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMovement>();
	}


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(anim.GetFloat("m_MoveSpeed"));
        }
        else
        {
            anim.SetFloat("m_MoveSpeed", (float)stream.ReceiveNext());
        }
    }
}
