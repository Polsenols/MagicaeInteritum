using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform barrelPos;

    [SerializeField]
    private float power = 40.0f;


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

        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump"))
        {
            shoot();
        }
    }

    void shoot()
    {

            GameObject go = PhotonNetwork.Instantiate(projectile.name,barrelPos.position,barrelPos.rotation, 0) as GameObject;
            go.GetComponent<Rigidbody>().AddForce(barrelPos.forward * power);

    }
}
