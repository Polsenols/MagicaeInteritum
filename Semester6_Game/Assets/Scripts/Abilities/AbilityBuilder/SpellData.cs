using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellData : MonoBehaviour
{

    [SerializeField]
    private bool _isAOE;
    [SerializeField]
    private bool _isUtility;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private int _range;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private float _travelDuration;
    [SerializeField]
    private float _knockbackForce;
    [SerializeField]
    private int _ownerID;
    [SerializeField]
    private int _spellID;
    [SerializeField]
    private float _cooldown;
    [SerializeField]
    private int _InstantiateID;
    [SerializeField]
    [Range(8f, 10.0f)]
    private float _slowMovementSpeed;
    [SerializeField]
    private float _slowDuration;
    [SerializeField]
    private float _curseDuration;
    [SerializeField]
    private float _curseDmgAdjuster;
    [SerializeField]
    private float _freezeDuration;
    [SerializeField]
    private float _lifeStealAmount;
    [SerializeField]
    private GameObject _impactEffect;
    [SerializeField]
    private AudioClip castSound;
    [SerializeField]
    private AudioClip impactSound;

    public new AudioSource audio;
    public SpellManager owner;
    public PlayerHealth_NET lastPlayerTarget;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void setOwner(SpellManager _owner)
    {
        owner = _owner;
    }

    public bool isAOE()
    {
        return _isAOE;
    }

    public bool isUtility()
    {
        return _isUtility;
    }

    public float travelDuration()
    {
        return _travelDuration;
    }

    public float damage()
    {
        return _damage;
    }

    public int range()
    {
        return _range;
    }

    public float speed()
    {
        return _speed;
    }

    public float radius()
    {
        return _radius;
    }

    public float knockbackForce()
    {
        return _knockbackForce;
    }

    public int spellID()
    {
        return _spellID;
    }

    public int ownerID()
    {
        return _ownerID;
    }

    public void setOwnerID(int ID)
    {
        _ownerID = ID;
    }

    public void setInstantiateID(int ID)
    {
        _InstantiateID = ID;
    }

    public float cooldown()
    {
        return _cooldown;
    }

    public int InstantiateID()
    {
        return _InstantiateID;
    }

    public float slowMovementSpeed()
    {
        return _slowMovementSpeed;
    }

    public float slowDuration()
    {
        return _slowDuration;
    }

    public float curseDuration()
    {
        return _curseDuration;
    }

    public float curseDmgAdjuster()
    {
        return _curseDmgAdjuster;
    }

    public float freezeDuration()
    {
        return _freezeDuration;
    }

    public float lifeStealAmount()
    {
        return _lifeStealAmount;
    }

    public void setSpellID(int ID)
    {
        _spellID = ID;
    }

    public GameObject getImpactEffect()
    {
        return _impactEffect;
    }

    public void setImpactEffect(GameObject impactEffect)
    {
        _impactEffect = impactEffect;
    }

    public void AbilityImpactEffect()
    {
        if(_impactEffect != null)
        Instantiate(_impactEffect, transform.position, Quaternion.identity);
    }

    public void PlayLoopSound()
    {
        audio.Play();
    }

    public void PlayImpactSound()
    {
        audio.PlayOneShot(impactSound);
    }

    public AudioClip CastSound()
    {
        return castSound;
    }
}
