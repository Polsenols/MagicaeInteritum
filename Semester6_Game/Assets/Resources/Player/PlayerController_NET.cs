using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_NET : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 4.0f;
    [SerializeField]
    private float turnSpeed = 35.0f;

    private Rigidbody localRigidBody;
    private PhotonView m_PhotonView;


    void Awake()
    {
        localRigidBody = GetComponent<Rigidbody>();
        m_PhotonView = GetComponent<PhotonView>();
    }

	void Start () {

        //If this script is not on the local player, destroy it.
        if (!m_PhotonView.isMine)
        {
            Destroy(this);
            return;
        }
	}
	

	void FixedUpdate () {
        float turnAmount = Input.GetAxis("Horizontal");
        float moveAmount = Input.GetAxis("Vertical");

        //Translation
        Vector3 deltaTranslation = transform.position + transform.forward * moveAmount * moveSpeed * Time.deltaTime;
        localRigidBody.MovePosition(deltaTranslation);

        //Rotation
        Quaternion deltaRotation = Quaternion.Euler(turnSpeed * new Vector3(0, turnAmount, 0) * Time.deltaTime);
        localRigidBody.MoveRotation(localRigidBody.rotation * deltaRotation);   
	}

}
