using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekNearbyPlayers : MonoBehaviour
{

    SpellMovement _spellMovement;
    SpellData spellData;

    private float timestamp = 0;

    void Start()
    {
        spellData = GetComponentInParent<SpellData>();
        _spellMovement = GetComponentInParent<SpellMovement>();
    }

    void SeekPlayers(Transform targetThisObject)
    {
        spellData.owner.SetSpellDirection(spellData.InstantiateID(), transform.parent.position, targetThisObject.position, spellData.ownerID());
    }

    void OnTriggerEnter(Collider other)
    {
        CharacterManager_NET player = other.GetComponent<CharacterManager_NET>();
        if (other.CompareTag("Player") && player.playerID != spellData.ownerID())
        {
            SeekPlayers(other.transform);
        }
    }
}
