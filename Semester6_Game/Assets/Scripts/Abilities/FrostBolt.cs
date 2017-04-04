 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBolt : SingleImpact{

    void OnTriggerEnter(Collider other)
    {
        Impact(other);
    }
}