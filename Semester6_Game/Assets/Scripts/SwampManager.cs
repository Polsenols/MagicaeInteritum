using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;

public class SwampManager : MonoBehaviour
{

    #region Public variables
    [Header("Damage")]
    public float damagePerTick = 1;
    public float tickInterval;
    #endregion

    #region Private variables
    private PhotonView m_photonView;
    private float timeSinceDamage = 0;
    public List<PlayerHealth_NET> playersOutsideCircle = new List<PlayerHealth_NET>();
    #endregion

    void Start()
    {
        m_photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if(playersOutsideCircle.Count > 0)
            DamagePlayer();
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

}
