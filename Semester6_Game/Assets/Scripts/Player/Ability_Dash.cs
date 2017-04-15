using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class Ability_Dash : MonoBehaviour {

    public float dashForce = 50f;
    public int coolDownTime = 5;
    public ParticleSystem dashParticleSys;
    private Rigidbody rb;
    private PlayerMovement playerMove;
    private PhotonView m_PhotonView;

    void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }

	void Start () {
        if (m_PhotonView.isMine)
        {
            playerMove = GetComponent<PlayerMovement>();
            rb = GetComponent<Rigidbody>();
        }
	}
	

	public void Dash () {
        if (m_PhotonView.isMine)
        {
                rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
                Vector3 particleRotation = new Vector3(0, transform.localEulerAngles.y * Mathf.Deg2Rad, 0);
                DisplayDashVisual(particleRotation);
                playerMove.moving = false;
        }
	}

    public void DisplayDashVisual(Vector3 rotation)
    {
        m_PhotonView.RPC("StartDashEnum", PhotonTargets.All, rotation);
    }

    [PunRPC]
    void StartDashEnum(Vector3 rotation)
    {
        Timing.RunCoroutine(Dash(rotation));
    }

    IEnumerator<float> Dash(Vector3 rotation)
    {
        Debug.Log("startDash");
        dashParticleSys.startRotation3D = rotation;
        dashParticleSys.Play();
        yield return Timing.WaitForSeconds(0.85f);
        dashParticleSys.Stop();
        Debug.Log("stopDash");
    }
}
