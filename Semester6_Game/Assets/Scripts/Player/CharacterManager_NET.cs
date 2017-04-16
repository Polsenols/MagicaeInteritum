using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager_NET : Photon.PunBehaviour {

    public PhotonView m_PhotonView;
    public GameObject audioFollower;
    private Animator anim;
    private PlayerHealth_NET _playerHealth;
    private ShopScript playerShop;
    public SpellManager spellManager;
    private Rigidbody _rigidbody;
    public int playerID;
    public string playerName;
    public int score;
    public float health;
    public List<CharacterManager_NET> Players = new List<CharacterManager_NET>();


    void Awake()
    {
        playerShop = GetComponent<ShopScript>();
        spellManager = GetComponent<SpellManager>();
        _playerHealth = GetComponent<PlayerHealth_NET>();
        _rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        m_PhotonView = GetComponent<PhotonView>();
    }

	void Start () {
        StartScore();
        m_PhotonView.RPC("AddPlayers", PhotonTargets.All);
        if (m_PhotonView.isMine)
        {
            GameObject go = (GameObject)Instantiate(audioFollower, transform.position, audioFollower.transform.rotation);
            go.GetComponent<AudioSourceFollow>().target = transform;   
        }
	}

    void Update()
    {
        if (!m_PhotonView.isMine)
        {
            _playerHealth.setHealth(health);
        }
        else
        {
            health = _playerHealth.getHealth();
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(health);
            stream.SendNext(anim.GetFloat("m_MoveSpeed"));
            stream.SendNext(anim.GetBool("Cast"));
        }
        else
        {
            health = (float)stream.ReceiveNext();
            anim.SetFloat("m_MoveSpeed", (float)stream.ReceiveNext());
            anim.SetBool("Cast", (bool)stream.ReceiveNext());
        }
    }

    [PunRPC]
    public void SetID(int id)
    {
        playerID = id;
    }

    [PunRPC]
    public void AddPlayers()
    {
        GameObject[] playersInScene = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playersInScene.Length; i++)
        {
            CharacterManager_NET player = playersInScene[i].GetComponent<CharacterManager_NET>();
            if (!Players.Contains(player)) {
                Players.Add(player);
                if (!SpawnManager.Instance.Players.Contains(player.GetComponent<PlayerHealth_NET>()))
                    SpawnManager.Instance.Players.Add(player.GetComponent<PlayerHealth_NET>());
            }
        }
    }

    [PunRPC]
    public void SetName(string name)
    {
        playerName = name;
    }

    public Rigidbody rigidBody()
    {
        return _rigidbody;
    }

    public PlayerHealth_NET playerHealth()
    {
        return _playerHealth;
    }

    public void ShoutScore(int score, int resourceGain)
    {
        m_PhotonView.RPC("SetScore", PhotonTargets.All, score, resourceGain);
    }

    [PunRPC]
    public void SetScore(int score, int resourceGain)
    {
        playerShop.AddResource(resourceGain);
        this.score += score;
        string scoreText = playerName + " : " + this.score.ToString();
        GameObject.Find("Player" + playerID).GetComponent<Text>().text = scoreText;
    }

    public void StartScore()
    {
        this.score = 0;
        string scoreText = playerName + " : " + this.score.ToString();
        GameObject.Find("Player" + playerID).GetComponent<Text>().text = scoreText;
    }
}
