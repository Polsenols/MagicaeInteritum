using UnityEngine;
using UnityEngine.UI;

public class AbilityCoolDown : MonoBehaviour {

    public Image darkMask;
    public Text coolDownText;
    public GameObject nextSpellSlot;
    public bool canGetASpell = false;
    public int SpellSlotNumber;

    //private spell spellData
    public int spellIndex;
    private float maxSpellCoolDown;

    private Image spellIcon;

    private float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;

    private ShopScript shopScript;
    private SpellManager spellManager;

    private AbilityDataForUI abilityDataForUI;

    public bool hasSpell;

	// Use this for initialization
	void Start ()
    {
        coolDownText.text = "";
        darkMask.fillAmount = 0f;
        abilityDataForUI = transform.root.GetComponent<AbilityDataForUI>();
        spellIcon = GetComponent<Image>();
        PhotonView m_PhotonView = transform.root.GetComponent<PhotonView>();

        if (m_PhotonView.isMine)
        {
            spellManager = transform.root.GetComponent<SpellManager>();
            shopScript = transform.root.GetComponent<ShopScript>();
        }

        hasSpell = false;
    }


	void Update ()
    {
        /*
         * If the spell slot is the next to recieve an icon and it doesn't have a spell already
         */
        if (canGetASpell == true && abilityDataForUI.spellAvailable == true)
       {    
            spellIndex = abilityDataForUI.boughtSpellIndex; //Get the index of the bought spell

            if (abilityDataForUI.spellsAvailability[spellIndex])
            {
                maxSpellCoolDown = spellManager.myCooldownMax[SpellSlotNumber];
                spellIcon.sprite = abilityDataForUI.GetSpellSprite();

                hasSpell = true;
                canGetASpell = false;

                abilityDataForUI.SpellAssigned(spellIndex);
                abilityDataForUI.spellAvailable = false;
            }

            if (nextSpellSlot != null)
            {
                //Will allow next spell slot to get a spell, unless this is the last spell slot, which will make the next slot null
                nextSpellSlot.GetComponent<AbilityCoolDown>().canGetASpell = true;
            }
           
        }
       /*
        * If a spell slot has a spell, this will display cooldown, both number and fill circle
        */
       else if (hasSpell == true)  
       {
            float cooldownTime = spellManager.myCooldown[SpellSlotNumber] / maxSpellCoolDown;
            darkMask.fillAmount = cooldownTime;

            float coolDownDisplayText = Mathf.Ceil(spellManager.myCooldown[SpellSlotNumber]);

            if(coolDownDisplayText > 0)
            {
                coolDownText.text = coolDownDisplayText.ToString();
            }
            else
            {
                coolDownText.text = "";
            }
        }
    }
}

