using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDataForUI : MonoBehaviour {

    public Sprite[] spellImages = new Sprite[15];
    public int boughtSpellIndex = 0; 
    [HideInInspector] public bool[] spellsAvailability; //
    [HideInInspector] public bool spellAvailable = false;

	void Start ()
    {
        spellsAvailability = new bool[15];

        for (int i = 0; i < spellsAvailability.Length; i++)
        {
            spellsAvailability[i] = true;
        }
    }

    public void SpellAssigned(int index)
    {
        spellsAvailability[index] = false;
    }

    public void SetBoughtSpellIndex(int index)
    {
        boughtSpellIndex = index;
    }

    public Sprite GetSpellSprite()
    {
        if(boughtSpellIndex < 15)
        {
            int index = boughtSpellIndex;
            return spellImages[index];
        }
            return null;
    }
}
