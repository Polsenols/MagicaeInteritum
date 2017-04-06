using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpellData))]
public class PoisonCloud : MonoBehaviour
{

    public enum DamageType
    {
        Impact,
        Continuous
    }

    SpellData spellData;
    public bool doesDamage = true;
    public float damageInterval = 0.2f;
    public DamageType type = DamageType.Continuous;
    List<CharacterManager_NET> _player = new List<CharacterManager_NET>();

    private float timestamp = 0;

    void Start()
    {
        spellData = GetComponent<SpellData>();
    }

    void Update()
    {
        if (_player.Count == 0)
        {
            return;
        }

        if (doesDamage)
            if (timestamp <= Time.time)
                DamagePlayers();

    }

    void DamagePlayers()
    {
        timestamp = Time.time + damageInterval;
        for (int i = 0; i < _player.Count; i++)
        {

            if (!_player[i].gameObject.activeSelf)
            {
                _player.Remove(_player[i]);
                return;
            }

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
