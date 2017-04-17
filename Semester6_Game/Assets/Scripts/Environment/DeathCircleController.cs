using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class DeathCircleController : MonoBehaviour
{

    #region Public variables
    public Projector lightProjector;
    public Renderer CircleEdge;
    public float fadeInDampenSpeed = 1;
    public float fadeOutDampenSpeed = 1;
    public float scaleDampen = 1;
    public float scaleTarget = 10;
    public float lightIntensityTarget = 0.3f;
    public float moveSpeed = 5;
    public float moveDuration = 10;
    [Header("Damage")]
    public float damagePerTick = 1;
    public float tickInterval;
    public Transform circleObj;
    public float width;
    public float height;
    #endregion

    #region Private variables
    private Light mainLight;
    private PhotonView m_photonView;
    private float fadeAmount = 0;
    private bool fadeInComplete = false;
    private bool canStart = false;
    private bool canStop = false;
    private bool scaleInComplete = false;
    private bool moving = false;
    private bool startMoveTrigger = false;
    private float startMoveTimestamp = 0;
    private float timeSinceMoveStart = 0;
    private float timeSinceDamage = 0;
    private Material projectorMat, edgeMat;
    private Vector3 originalScale;
    private float originalLightIntensity;
    private float xDir = 1.1f;
    private float zDir = 0.95f;
    public List<PlayerHealth_NET> playersOutsideCircle = new List<PlayerHealth_NET>();
    #endregion

    void Start()
    {
        mainLight = SpawnManager.Instance.mainLight;
        m_photonView = GetComponent<PhotonView>();
        originalLightIntensity = mainLight.intensity;
        originalScale = circleObj.localScale;
        originalScale.y = 1;
        projectorMat = new Material(lightProjector.material);
        edgeMat = new Material(CircleEdge.material);
        lightProjector.material = projectorMat;
        CircleEdge.material = edgeMat;
        foreach (PlayerHealth_NET player in SpawnManager.Instance.Players)
        {
            if(player.m_PhotonView.isMine)
                playersOutsideCircle.Add(player);
        }
        if (PhotonNetwork.isMasterClient)
        {
            StartEvent();
        }
    }

    void Update()
    {
        if (canStart)
        {
            if (fadeInComplete == false)
                FadeIn();
            if (scaleInComplete == false && fadeInComplete)
                ScaleIn();
            if (scaleInComplete && startMoveTrigger)
            {
                startMoveTimestamp = Time.time + moveDuration;
                startMoveTrigger = false;
            }
            if (scaleInComplete)
            {
                DamagePlayer();
            }
            if (PhotonNetwork.isMasterClient)
            {
                if (scaleInComplete && timeSinceMoveStart < startMoveTimestamp)
                {
                    if (!moving)
                    {
                        Moving();
                        moving = true;
                    }
                    MoveCircle();
                }
                if(scaleInComplete && timeSinceMoveStart > startMoveTimestamp)
                {
                    StartFadeOut();
                }
            }
            if (canStop)
                FadeOut();
            if (moving)
            {
                SpawnManager.Instance.SetSpawnOrigin(transform.position);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth_NET player = other.GetComponent<PlayerHealth_NET>();
        if (playersOutsideCircle.Contains(player) && player.m_PhotonView.isMine)
        {
            playersOutsideCircle.Remove(player);
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerHealth_NET player = other.GetComponent<PlayerHealth_NET>();
        if (!playersOutsideCircle.Contains(player) && player.m_PhotonView.isMine)
        {
            playersOutsideCircle.Add(player);
        }
    }

    void DamagePlayer()
    {
        if (timeSinceDamage <= Time.time && playersOutsideCircle.Count > 0)
        {
            timeSinceDamage = Time.time + tickInterval;
            for (int i = 0; i < playersOutsideCircle.Count; i++)
            {
                playersOutsideCircle[i].TakeDamage(damagePerTick, -1, null);
            }
        }
    }

    #region Photon functions
    private void StartScaleIn()
    {
        m_photonView.RPC("fadeComplete", PhotonTargets.AllBuffered);
    }


    private void StartEvent()
    {
        m_photonView.RPC("startEvent", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    private void startEvent()
    {
        canStart = true;
    }

    private void StartFadeIn()
    {
        m_photonView.RPC("FadeIn", PhotonTargets.AllBuffered);
    }

    private void StartFadeOut()
    {
        m_photonView.RPC("FadeOutTrigger", PhotonTargets.AllBuffered);
    }

    private void Moving()
    {
        m_photonView.RPC("SetMoving", PhotonTargets.OthersBuffered);
    }

    #endregion
    [PunRPC]
    private void FadeOutTrigger()
    {
        canStop = true;
        moving = false;
    }

    [PunRPC]
    private void SetMoving()
    {
        moving = true;
    }

    private void ScaleIn()
    {
        circleObj.localScale = Vector3.MoveTowards(circleObj.localScale, new Vector3(scaleTarget, scaleTarget, scaleTarget), Time.deltaTime);
        SpawnManager.Instance.SetSpawnRadius(circleObj.localScale.x);    
        if (circleObj.localScale.x < scaleTarget + 0.1f && scaleInComplete == false)
        {
            timeSinceMoveStart = Time.time;
            scaleInComplete = true;
            startMoveTrigger = true;
        }
    }

    public void Reset()
    {
        SpawnManager.Instance.InitializeOriginalState();
        Destroy(this.gameObject);
    }

    private void MoveCircle()
    {
        float circleRadius = circleObj.localScale.x;
        transform.position += new Vector3(xDir, 0, zDir) * Time.deltaTime * moveSpeed;
        if (transform.position.x >= width - circleRadius)
        {
            xDir = -Mathf.Abs(xDir);
        }
        else if (transform.position.x <= -width + circleRadius)
        {
            xDir = Mathf.Abs(xDir);
        }
        if (transform.position.z >= height - circleRadius)
        {
            zDir = -Mathf.Abs(zDir);
        }
        else if (transform.position.z <= -height + circleRadius)
        {
            zDir = Mathf.Abs(zDir);
        }
        timeSinceMoveStart += Time.deltaTime;
    }

    private void FadeOut()
    {

        if (fadeAmount >= 0)
        {
            fadeAmount -= Time.deltaTime / fadeOutDampenSpeed;
            projectorMat.SetFloat("_Fade", fadeAmount);
            edgeMat.SetFloat("_Fade", fadeAmount);
        }
        else if (mainLight.intensity <= originalLightIntensity)
        {
            mainLight.intensity += Time.deltaTime / fadeOutDampenSpeed;
        }
        else
        {
            Reset();
        }
    }

    private void FadeIn()
    {
        if (fadeAmount <= 1)
        {
            fadeAmount += Time.deltaTime / fadeInDampenSpeed;
            projectorMat.SetFloat("_Fade", fadeAmount);
            edgeMat.SetFloat("_Fade", fadeAmount);
        }
        else if (mainLight.intensity >= lightIntensityTarget)
        {
            mainLight.intensity -= Time.deltaTime / fadeInDampenSpeed;
        }
        else
        {
            fadeInComplete = true;
        }
    }
}
