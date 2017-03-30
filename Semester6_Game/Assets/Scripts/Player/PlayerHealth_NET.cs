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
    public int maxHealth;
    public float healthBarHeight;
    private int health;
    private Image healthBar;

    public GameObject meteor;
    public GameObject healthbarUI_prefab;
    private GameObject healthbarUI;

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
        Timing.RunCoroutine(_Die(2.0f));
    }

    private IEnumerator<float> _Die(float respawnTime)
    {
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
        healthbarUI.SetActive(true);
        this.gameObject.SetActive(true);
    }

    public void TakeDamage(int damage, int playerID, CharacterManager_NET charMan)
    {
        Debug.Log("Player " + playerID + " attacked me");
        health -= damage;
        healthBar.fillAmount = Mathf.Clamp((float)health / (float)maxHealth, 0, maxHealth);
        if (health <= 0)
        {
            Die();
            charMan.ShoutScore();
            Debug.Log("I died");
        }
        Debug.Log("I now have health: " + health);
    }
}
