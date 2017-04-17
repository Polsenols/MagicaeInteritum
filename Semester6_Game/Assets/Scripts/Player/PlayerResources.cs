using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources
{

    private int currentResources;
    private int moneyWaitTime = 2;

    public int CurrentResources
    {
        get
        {
            return currentResources;
        }
        set
        {
            currentResources = value;
        }
    }

    public int MoneyWaitTime
    {
        get
        {
            return moneyWaitTime;
        }
    }
}
