using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIndicatorController : MonoBehaviour {

    public GameObject moveIndicator;
    private AnimateTiledTexture animTexture;
    private PhotonView m_PhotonView;

    // Use this for initialization
    void Start()
    {
        animTexture = moveIndicator.GetComponent<AnimateTiledTexture>();
        m_PhotonView = GetComponent<PhotonView>();

        if (!m_PhotonView.isMine)
        {
            Destroy(moveIndicator);
            Destroy(this);
            return;
        }
    }

    // Update is called once per frame
    public void UpdateMoveIndicator(Vector3 newPos)
    {
            moveIndicator.SetActive(true);
            animTexture.UpdatePosition(newPos);
    }
}
