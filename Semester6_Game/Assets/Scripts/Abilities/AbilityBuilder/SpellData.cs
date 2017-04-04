using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellData : MonoBehaviour {

    [SerializeField]
    private bool _isAOE;
    [SerializeField]
    private int _damage;
    [SerializeField]
    private int _range;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private float _travelDistance;
    [SerializeField]
    private float _knockbackForce;
    [SerializeField]
    private int _ownerID;
    [SerializeField]
    private float _cooldown;
    [SerializeField]
    private int _InstantiateID;

    public SpellManager owner;

    public void setOwner(SpellManager _owner)
    {
        owner = _owner;
    }

    public bool isAOE()
    {
        return _isAOE;
    }

    public int damage()
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

    public float travelDistance()
    {
        return _travelDistance;
    }

    public float knockbackForce()
    {
        return _knockbackForce;
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
}
