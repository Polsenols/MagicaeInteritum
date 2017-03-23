using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    private PhotonView m_PhotonView;

    void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (!m_PhotonView.isMine)
        {
            Destroy(this);
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            shoot();
        }
    }

    void shoot()
    {
        GameObject go = PhotonNetwork.Instantiate(projectile.name, transform.position, Quaternion.identity, 0) as GameObject;
            
    }
}
