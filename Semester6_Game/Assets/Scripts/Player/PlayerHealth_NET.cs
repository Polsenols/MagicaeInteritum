using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using MovementEffects;

public class PlayerHealth_NET : Photon.PunBehaviour
{

    public LayerMask mask;
    public Canvas canvas;
    PhotonView m_PhotonView;
    CharacterManager_NET lastAttackedByPlayer;
    public int maxHealth;
    public float healthBarHeight;
    private int health;
    private Image healthBar;

    public GameObject ragdoll;
    public GameObject meteor;
    public GameObject healthbarUI_prefab;
    private GameObject healthbarUI;
    private int lastAttackedByID;
    public bool invulnurable = false;
    private float timestamp = 0;
    private float deathTimer = 3;

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
        setName();
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int amount)
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
        if(timestamp <= Time.time)
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

    private void UpdateHealthBarPos()
    {
        Vector3 healthBarPos = new Vector3(transform.position.x, transform.position.y + healthBarHeight, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(healthBarPos);
        healthbarUI.transform.position = screenPos;
        healthBar.fillAmount = Mathf.Clamp((float)health / (float)maxHealth, 0, maxHealth);        
    }

    public void Die()
    {
        m_PhotonView.RPC("Respawn", PhotonTargets.All);
    }

    [PunRPC]
    public void Respawn()
    {
        GameObject go = (GameObject)Instantiate(ragdoll, transform.position, ragdoll.transform.rotation);
        go.GetComponent<RagdollControl>().PushRagdoll(transform.position);
        Timing.RunCoroutine(_Die(2.0f));
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
        timestamp = Time.time + deathTimer;
        healthbarUI.SetActive(true);
        this.gameObject.SetActive(true);
        //Timing.RunCoroutine(_Invul(3.0f));
    }



    private IEnumerator<float> _Invul(float invul_time)
    {
        yield return Timing.WaitForSeconds(invul_time);
        invulnurable = false;
    }

    public void TakeDamage(int damage, int playerID, CharacterManager_NET charMan)
    {
        if (!invulnurable)
        {
            if (playerID > 0)
            {
                lastAttackedByID = playerID;
                lastAttackedByPlayer = charMan;
            }

            Debug.Log("Player " + playerID + " attacked me");
            health -= damage;
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

}
