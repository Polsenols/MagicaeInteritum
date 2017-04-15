using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekNearbyPlayers : MonoBehaviour
{

    SpellMovement _spellMovement;
    SpellData spellData;

    private float timestamp = 0;
    private Vector3 direction;

    void Start()
    {
        spellData = GetComponentInParent<SpellData>();
        _spellMovement = GetComponentInParent<SpellMovement>();
        direction = _spellMovement.GetSpellDir();
    }

    void SeekPlayers(Transform targetThisObject)
    {
        spellData.owner.SetSpellDirection(spellData.InstantiateID(), transform.parent.position, targetThisObject.position, spellData.ownerID());
    }

    void OnTriggerEnter(Collider other)
    {
        CharacterManager_NET player = other.GetComponent<CharacterManager_NET>();
        if (player.m_PhotonView.isMine)
        {
            if (direction == _spellMovement.GetSpellDir())
            {
                if (other.CompareTag("Player") && player.playerID != spellData.ownerID())
                {
                    SeekPlayers(other.transform);
                }
            }
        }
    }
}
