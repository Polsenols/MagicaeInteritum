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
    private int _speed;
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

    public int speed()
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

    public float cooldown()
    {
        return _cooldown;
    }
}
