using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Dash : MonoBehaviour {

    public float dashForce = 50f;
    public ParticleSystem dashParticleSys;
    private Rigidbody rb;
    private PlayerMovement playerMove;
    private PhotonView m_PhotonView;

    void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }
	// Use this for initialization
	void Start () {
        if (!m_PhotonView.isMine)
        {
            Destroy(this);
            return;
        }
        playerMove = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
            dashParticleSys.startRotation3D = new Vector3(0, transform.localEulerAngles.y * Mathf.Deg2Rad, 0);
            StartCoroutine(Dash());
            playerMove.moving = false;
        }
	}

    IEnumerator Dash()
    {
        dashParticleSys.Play();
        yield return new WaitForSeconds(0.85f);
        dashParticleSys.Stop();
    }
}
