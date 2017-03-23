using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth_NET : Photon.MonoBehaviour
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
    }

    public int getHealth()
    {
        return health;
    }

    public void setHealth(int amount)
    {
        health = amount;
    }

    void Update()
    {
        UpdateHealthBarPos();
        if (m_PhotonView.isMine)
        {
            if (Input.GetKeyDown(KeyCode.K))
                Doit(10);
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
        Debug.Log("I am dead xD");
    }

    public void Doit(int damage)
    {
        m_PhotonView.RPC("spawnShit", PhotonTargets.All, transform.position, PhotonNetwork.player.ID);
    }


    [PunRPC]
    public void spawnShit(Vector3 pos, int playerID)
    {
        GameObject go = Instantiate(meteor, pos, Quaternion.identity) as GameObject;
        go.GetComponentInChildren<MeteorController>().ownerID = playerID;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.fillAmount = Mathf.Clamp((float)health / (float)maxHealth, 0, maxHealth);
        if (health <= 0)
            Die();
        Debug.Log("I now have health: " + health);
    }
}
