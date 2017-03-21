using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositionScript : MonoBehaviour {

    public LayerMask mask;
    private Ray ray;
    private PhotonView m_PhotonView;

    void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        //If this script is not on the local player, destroy it.
        if (!m_PhotonView.isMine)
        {
            Destroy(this);
            return;
        }
    }

    public Vector3 getMouseWorldPoint()
    {
        RaycastHit hit;
        ray =  Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        if (Physics.Raycast(ray, out hit, 1000, mask))
        {
            Vector3 worldPos = hit.point;
            worldPos.y = 0;
            return worldPos;
        }
        return transform.position;
    }
}
