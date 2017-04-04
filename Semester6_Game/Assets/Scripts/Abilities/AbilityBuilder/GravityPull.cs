using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpellData))]
public class GravityPull : MonoBehaviour {
    
    public enum GravityType
    {
        Impact,
        Continuous
    }

    SpellData spellData;
    public float force = 5;
    public bool doesDamage = true;
    public float damageInterval = 1;
    public GravityType type = GravityType.Continuous;
    List<CharacterManager_NET> _player = new List<CharacterManager_NET>();

    private float timestamp = 0;
    
    void Start () {
        spellData = GetComponent<SpellData>();
	}
	
	void Update () {
        if (_player.Count == 0)
        {
            return;
        }
        AttractPlayers();

        if (doesDamage)
            if(timestamp <= Time.time)
                DamagePlayers();

	}

    void AttractPlayers()
    {        
        for (int i = 0; i < _player.Count; i++)
        {
            if (_player[i].m_PhotonView.isMine)
            {
                Vector3 forceDir = Vector3.Normalize(transform.position - _player[i].transform.position);
                forceDir.y = 0;
                _player[i].rigidBody().AddForce(forceDir * force);
            }
        }
    }

    void DamagePlayers()
    {
        timestamp = Time.time + damageInterval;
        for (int i = 0; i < _player.Count; i++)
        {
            if (_player[i].m_PhotonView.isMine)
            {
                _player[i].playerHealth().TakeDamage(spellData.damage(), spellData.ownerID(), _player[i]);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        CharacterManager_NET player = other.GetComponent<CharacterManager_NET>();
        if (!_player.Contains(player) && player.playerID != spellData.ownerID())
        {
            _player.Add(player);
        }
    }

    void OnTriggerExit(Collider other)
    {
        CharacterManager_NET player = other.GetComponent<CharacterManager_NET>();
        if (_player.Contains(player))
        {
            _player.Remove(player);
        }
    }
}
