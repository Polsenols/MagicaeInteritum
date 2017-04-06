using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using MovementEffects;

public class PlayerHealth_NET : Photon.PunBehaviour
{
    public Canvas canvas;
    PhotonView m_PhotonView;
    CharacterManager_NET lastAttackedByPlayer;
    public float maxHealth;
    public float healthBarHeight;
    private float health;
    private Image healthBar;

    public GameObject ragdoll;
    public GameObject meteor;
    public GameObject healthbarUI_prefab;
    private GameObject healthbarUI;
    private int lastAttackedByID;
    public bool invulnurable = false;
    private float timeStamp1 = 0;
    private float timeStamp2 = 0;
    private float timeStamp3 = 0;
    private float deathTimer = 3;
    public float freezeAmount = 2.0f;
    public float curseDuration = 4.0f;
    public float damageAdjuster = 1;
    public GameObject iceBlock, curseMarker;
    private LineRenderer myLine;

    void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();

    }

    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        healthbarUI = Instantiate(healthbarUI_prefab) as GameObject;
        healthBar = healthbarUI.transform.GetChild(0).GetComponent<Image>();
        healthbarUI.transform.SetParent(canvas.transform, false);
        health = maxHealth;
        myLine = this.GetComponent<LineRenderer>();
        setName();
        iceBlock.SetActive(false);
        curseMarker.SetActive(false);
    }

    public float getHealth()
    {
        return health;
    }

    public void setHealth(float amount)
    {
        health = amount;
    }

    public void setName()
    {
        healthbarUI.transform.GetChild(1).GetComponent<Text>().text = GetComponent<CharacterManager_NET>().playerName;
    }

    void Update()
    {
        UpdateHealthBarPos();
        if (timeStamp1 <= Time.time)
        {
            invulnurable = false;
        }
        if (m_PhotonView.isMine)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                #region testing

                #endregion
            }
        }
    }

    void FixedUpdate()
    {
        if (Time.time > timeStamp2 + freezeAmount)
        {
            UnfreezePlayer();
        }


        if (Time.time > timeStamp3 + curseDuration)
        {
            UnCursePlayer();
        }


    }

    private void UpdateHealthBarPos()
    {
        Vector3 healthBarPos = new Vector3(transform.position.x, transform.position.y + healthBarHeight, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(healthBarPos);
        healthbarUI.transform.position = screenPos;
        healthBar.fillAmount = Mathf.Clamp((float)health / (float)maxHealth, 0, maxHealth);
    }

    public void Die()
    {
        if (m_PhotonView.isMine)
        {
            m_PhotonView.RPC("Respawn", PhotonTargets.All);
        }
    }

    [PunRPC]
    public void Respawn()
    {
        GameObject go = (GameObject)Instantiate(ragdoll, transform.position, ragdoll.transform.rotation);
        go.GetComponent<RagdollControl>().PushRagdoll(transform.position);
        Timing.RunCoroutine(_Die(2.0f));
    }

    public void TakeDamageOverTime(int amountOfTicks, int damage, float timeBetweenTicks, CharacterManager_NET charMan, int playerID)
    {
        Timing.RunCoroutine(_TakeDamageOverTime(amountOfTicks, damage, timeBetweenTicks, charMan, playerID));
    }

    private IEnumerator<float> _TakeDamageOverTime(int amountOfTicks, int damage, float timeBetweenTicks, CharacterManager_NET charMan, int playerID)
    {
        for (int i = 0; i < amountOfTicks; i++)
        {
            TakeDamage(damage, playerID, charMan);
            yield return Timing.WaitForSeconds(timeBetweenTicks);
        }
    }

    private IEnumerator<float> _Die(float respawnTime)
    {
        invulnurable = true;
        this.gameObject.SetActive(false);
        healthbarUI.SetActive(false);
        yield return Timing.WaitForSeconds(respawnTime);
        setHealth(maxHealth);
        #region Temporary transform respawner
        Vector3 spawnPos = Vector3.up;
        Vector3 random = Random.insideUnitSphere * 10.0f;
        random.y = 1;
        Vector3 itempos = spawnPos + 1.0f * random;
        transform.position = itempos;
        #endregion
        timeStamp1 = Time.time + deathTimer;
        healthbarUI.SetActive(true);
        this.gameObject.SetActive(true);
        UnfreezePlayer();
        UnCursePlayer();
        myLine.enabled = false;
        //Timing.RunCoroutine(_Invul(3.0f));
    }



    private IEnumerator<float> _Invul(float invul_time)
    {
        yield return Timing.WaitForSeconds(invul_time);
        invulnurable = false;
    }

    public void TakeDamage(float damage, int playerID, CharacterManager_NET charMan)
    {
        if (!invulnurable)
        {
            if (playerID > 0)
            {
                lastAttackedByID = playerID;
                lastAttackedByPlayer = charMan;
            }

            Debug.Log("Player " + playerID + " attacked me");
            health -= damage * damageAdjuster;
            healthBar.fillAmount = Mathf.Clamp((float)health / (float)maxHealth, 0, maxHealth);
            if (health <= 0)
            {
                if (lastAttackedByPlayer != null)
                {
                    lastAttackedByPlayer.ShoutScore();
                }
                Die();
                Debug.Log("I died");
            }

            Debug.Log("I now have health: " + health);
        }
    }

    public void Curse()
    {
        m_PhotonView.RPC("CursePlayer", PhotonTargets.All);
    }

    [PunRPC]
    void CursePlayer()
    {
        Debug.Log("Curse Player");
        if (!invulnurable)
        {
            damageAdjuster = 2.0f; // 1.2 = 20% dmg, 1.5 = 50% and so forth.
            curseMarker.SetActive(true);
            timeStamp3 = Time.time;
        }
    }

    private void UnCursePlayer()
    {
        damageAdjuster = 1;
        timeStamp3 = 0;
        curseMarker.SetActive(false);
    }


    public void Freeze()
    {
        m_PhotonView.RPC("FreezePlayer", PhotonTargets.All);
    }

    [PunRPC]
    void FreezePlayer()
    {
        Debug.Log("Freeze player");
        if (!invulnurable)
        {
            if (this.GetComponent<PlayerMovement>() != null)
                this.GetComponent<PlayerMovement>().FreezePlayerMovement();
            if (this.GetComponent<SpellManager>() != null)
                this.GetComponent<SpellManager>().FreezePlayerSpellCasting();
            iceBlock.SetActive(true);

            timeStamp2 = Time.time;
        }
    }

    private void UnfreezePlayer()
    {
        if (this.GetComponent<PlayerMovement>() != null)
            this.GetComponent<PlayerMovement>().UnfreezePlayerMovemenet();
        if (this.GetComponent<SpellManager>() != null)
            this.GetComponent<SpellManager>().UnfreezePlayerSpellCasting();
        iceBlock.SetActive(false);
        timeStamp2 = 0;
    }
}
