using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using MovementEffects;

public class ShopScript : MonoBehaviour
{
    /*
        Spell Index Lookup:
        -------------------
        0 = Fireball
        1 = Meteor
        2 = Frostbolt
        3 = Icespike
        4 = Tornado
        -------------------
        5 = Shield
        6 = Dash
        7 = Gravity Orb
        8 = Telekinesis
        9 = Force Push
        -------------------
        10 = Poison Bolt
        11 = Gas Cloud
        12 = Life Steal
        13 = Raise Zombie
        14 = Curse
        -------------------
    */

    // Array to determine the cost of a spell at a given index
    private int[] spellCostArray = new int[15]
    { 50, 100, 50, 75, 75, 50, 50, 100, 75, 75, 50, 100, 75, 75, 50 };

    // Array to control whether the player got the spell; false = dont have spell; true = players have bought the spell
    // Check this bool array to determine whether a player have a spell so he may use it.
    public bool[] doYouHaveSpell = new bool[15]
    { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };

    // An indexer to control which spell is being bought.
    int spellIndexer;

    PlayerResources playerResource = new PlayerResources();

    #region Canvas Related Variables
    public GameObject canvasPlaceholder;

    public Canvas spellSchoolCanvas1;
    public Canvas spellSchoolCanvas2;
    public Canvas spellSchoolCanvas3;
    public Canvas toolTipCanvas;
    public Canvas buySpellCanvas;

    public Button spellSchooButton1;
    public Button spellSchooButton2;
    public Button spellSchooButton3;
    public Button buySpell;
    public Button dontBuySpell;

    public Text[] spellTxt = new Text[15];
    public Text spellHeadline;
    public Text spellDescription;
    public Text spellCost;
    public Text spellCooldown;
    public Text spellDamage;
    public Text spellDPS;
    public Text spellAOE;
    public Text spellDuration;

    public Text resourcesDisplay;
    public Text resourcesHUDDisplay;

    public Image[] mySpellImages = new Image[15];

    #endregion

    private bool buyingSpells = false;
    private bool closeEnoughToShop = false;
    private bool isCurrentlyShopping = false;
    private PhotonView m_photonView;
    private SpellManager spellManager;

    void Awake()
    {
        spellManager = GetComponent<SpellManager>();
        m_photonView = GetComponent<PhotonView>();
        if (!m_photonView.isMine)
        {
            Destroy(canvasPlaceholder);
            Destroy(this);
        }
        playerResource.CurrentResources = 100;
        Timing.RunCoroutine(_ResourceProvider(playerResource.MoneyWaitTime));
        canvasPlaceholder.SetActive(false);

        spellSchoolCanvas1.enabled = false;
        spellSchoolCanvas2.enabled = false;
        spellSchoolCanvas3.enabled = false;
        toolTipCanvas.enabled = false;
        buySpellCanvas.enabled = false;

        spellSchooButton1 = spellSchooButton1.GetComponent<Button>();
        spellSchooButton2 = spellSchooButton2.GetComponent<Button>();
        spellSchooButton3 = spellSchooButton3.GetComponent<Button>();

        buySpell = buySpell.GetComponent<Button>();
        dontBuySpell = dontBuySpell.GetComponent<Button>();
        buySpell.enabled = false;
        dontBuySpell.enabled = false;
        buyingSpells = false;
        closeEnoughToShop = false;
        isCurrentlyShopping = false;

        for(int i = 0; i < spellTxt.Length; i++)
        {
            spellTxt[i] = spellTxt[i].GetComponent<Text>();
            spellTxt[i].text += " (Cost: " + spellCostArray[i] +")";
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shop")
        {
            closeEnoughToShop = true;
        }
    }


    void Update()
    {
        // Controls for entering the shop; player must press B to enter.
        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Escape) && closeEnoughToShop)
        {
            if (isCurrentlyShopping == false)
            {
                OpenShop();
            }
            else if (isCurrentlyShopping == true)
            {
                CloseShop();
            }
        }

        resourcesDisplay.GetComponent<Text>().text = playerResource.CurrentResources.ToString();
        resourcesHUDDisplay.GetComponent<Text>().text = "Resources: " + playerResource.CurrentResources.ToString();

    }

    private void OpenShop()
    {
        canvasPlaceholder.SetActive(true);
        spellSchoolCanvas1.enabled = true;
        spellSchoolCanvas2.enabled = false;
        spellSchoolCanvas3.enabled = false;
        isCurrentlyShopping = true;
    }

    private void CloseShop()
    {
        canvasPlaceholder.SetActive(false);
        spellSchoolCanvas1.enabled = false;
        spellSchoolCanvas2.enabled = false;
        spellSchoolCanvas3.enabled = false;
        isCurrentlyShopping = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Shop")
        {
            //Set everything to "default" in respect to canvases (i.e. not changing the spell arrays)
            ResetShop();
        }
    }

    public void SpellSchool1Press()
    {
        spellSchoolCanvas1.enabled = true;
        spellSchoolCanvas2.enabled = false;
        spellSchoolCanvas3.enabled = false;

        spellSchooButton1.enabled = false;
        spellSchooButton2.enabled = true;
        spellSchooButton3.enabled = true;
    }

    public void SpellSchool2Press()
    {
        spellSchoolCanvas1.enabled = false;
        spellSchoolCanvas2.enabled = true;
        spellSchoolCanvas3.enabled = false;

        spellSchooButton1.enabled = true;
        spellSchooButton2.enabled = false;
        spellSchooButton3.enabled = true;
    }
    
    public void SpellSchool3Press()
    {
        spellSchoolCanvas1.enabled = false;
        spellSchoolCanvas2.enabled = false;
        spellSchoolCanvas3.enabled = true;

        spellSchooButton1.enabled = true;
        spellSchooButton2.enabled = true;
        spellSchooButton3.enabled = false;
    }

    #region Spells
    #region Elemental Spells Region
    public void TooltipFireball(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Fire Ball: Shoots a ball of fire in a specific" +
                System.Environment.NewLine + "direction, and explodes upon impact";
            SpellTooltipInfo("Fire Ball", setSpellDescipTxt, spellCostArray[0], 2, 10, 4, 0);
        }
    }

    public void TooltipMeteor(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Meteor: Hurls a massive meteor from the sky," +
                System.Environment.NewLine + "which explodes in a large field on impact";
            SpellTooltipInfo("Meteor", setSpellDescipTxt, spellCostArray[1], 10, 30, 10, 0);
        }
    }

    public void TooltipFrostBolt(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Frost Bolt: Shoots a frost bolt in a specific" +
                System.Environment.NewLine + "direction and damages+freezes on impact";
            SpellTooltipInfo("Frost Bolt", setSpellDescipTxt, spellCostArray[2], 3, 7, 0, 0);
        }
    }

    public void TooltipIceSpike(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Ice Spike: Shoots an ice spike in a specific" +
                System.Environment.NewLine + "direction and explodes in pieces upon impact";
            SpellTooltipInfo("Ice Spike", setSpellDescipTxt, spellCostArray[3], 5, 15, 5, 0);
        }
    }

    public void TooltipTornado(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Tornado: Hurls a tornado in a direction " +
                System.Environment.NewLine + "that draws in enemies along the way";
            SpellTooltipInfo("Tornado", setSpellDescipTxt, spellCostArray[4], 10, 2, 10, 5);
        }
    }
    #endregion

    #region Kinetic Spell Region
    public void TooltipShield(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Shield: Creates a kinetic shield around you " +
                System.Environment.NewLine + "that makes you immune for a brief while";
            SpellTooltipInfo("Shield", setSpellDescipTxt, spellCostArray[5], 10, 0, 0, 2);
        }
    }

    public void TooltipDash(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Dash: Use your kinetic energy to dash " +
                System.Environment.NewLine + "forward in a specific direction";
            SpellTooltipInfo("Dash", setSpellDescipTxt, spellCostArray[6], 10, 0, 0, 0);
        }
    }

    public void TooltipOrbOfGravity(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Gravity Orb: create a static gravity orb that pulls in " +
                System.Environment.NewLine + "your enemies and shoots them out afterwards";
            SpellTooltipInfo("Gravity Orb", setSpellDescipTxt, spellCostArray[7], 10, 0, 0, 3);
        }
    }

    public void TooltipTelekinesis(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Telekinesis: pick up debris around you and " +
                System.Environment.NewLine + "and shoot them after your enemies";
            SpellTooltipInfo("Telekinesis", setSpellDescipTxt, spellCostArray[8], 5, 10, 3, 0);
        }
    }

    public void TooltipForcePush(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Force Push: use your jedi force and push away " +
                System.Environment.NewLine + "your enemies while damaging them";
            SpellTooltipInfo("Force Push", setSpellDescipTxt, spellCostArray[9], 5, 20, 0, 0);
        }
    }
    #endregion

    #region Dark Spells Region
    public void TooltipPoisonBolt(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Poison Bolt: Shoots a bolt of poison at your " +
                System.Environment.NewLine + "enemies that damages over time";
            SpellTooltipInfo("Poison Bolt", setSpellDescipTxt, spellCostArray[10], 4, 20, 0, 0);
        }
    }

    public void TooltipGasCloud(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Gas Cloud: summons a gas cloud that moves in " +
                System.Environment.NewLine + "a direction and damages anyone who enters";
            SpellTooltipInfo("Gas Cloud", setSpellDescipTxt, spellCostArray[11], 10, 20, 5, 5);
        }
    }

    public void TooltipLifeSteal(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Life Steal: target an enemy who will get his life " +
                System.Environment.NewLine + "damaged over time while giving you life";
            SpellTooltipInfo("Life Steal", setSpellDescipTxt, spellCostArray[12], 10, 15, 0, 5);
        }
    }

    public void TooltipRaiseZombie(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Raise Zombie: summons a zombie to fight for you! " +
                System.Environment.NewLine + "The zombie will attack foes at random";
            SpellTooltipInfo("Raise Zombie", setSpellDescipTxt, spellCostArray[13], 25, 2, 0, 60);
        }
    }

    public void TooltipCurse(int spellIndexLocator)
    {
        if (!buyingSpells)
        {
            spellIndexer = spellIndexLocator;
            string setSpellDescipTxt = "Curse: places a curse on an enemy player " +
                System.Environment.NewLine + "that increases damage taken from next spell";
            SpellTooltipInfo("Curse", setSpellDescipTxt, spellCostArray[14], 20, 10, 0, 20);
        }
    }

    #endregion
    #endregion

    public void disableTooltip()
    {
        toolTipCanvas.enabled = false;
    }

    //Display canvas to confirm a spell buy
    public void confirmSpellBuy()
    {

        if (playerResource.CurrentResources >= spellCostArray[spellIndexer])
        {
            closeEnoughToShop = false;
            toolTipCanvas.enabled = false;
            buyingSpells = true;
            buySpellCanvas.enabled = true;
            buySpell.enabled = true;
            dontBuySpell.enabled = true;
        }
    }

    //In the event that a player presses "yes", then it should buy the spell
    public void spellBought(int spellIndexNumber)
    {
        spellIndexNumber = spellIndexer; // Get the index number of the spell currently being looked at

        //Checks if player got enough resources and whether he already have the spell
        if (spellCostArray[spellIndexNumber] <= playerResource.CurrentResources && !doYouHaveSpell[spellIndexNumber])
        {
            playerResource.CurrentResources -= spellCostArray[spellIndexNumber]; // Reduce the amount of resources with the spell cost
            doYouHaveSpell[spellIndexNumber] = true; //Set the index position of the spell to true; i.e. player have bought the spell and cannot buy it again.
            spellManager.addSpell(spellIndexNumber);
            Debug.Log("You Bought a Spell!");

            //Return to "default" shop; i.e. close "confirm" canvas.
            buySpellCanvas.enabled = false;
            buySpell.enabled = false;
            dontBuySpell.enabled = false;
            buyingSpells = false;
            closeEnoughToShop = true;
            checkAffordance();

            /****************** Added for HUD to work ******************/

            GetComponent<AbilityDataForUI>().SetBoughtSpellIndex(spellIndexNumber);
            GetComponent<AbilityDataForUI>().spellAvailable = true;

            /****************** Added for HUD to work ******************/
        }
        else
        {
            //Here goes the code for "pop" up saying insufficient fonds; still to be implemented.
            Debug.Log("NOT ENOUGH MONEY OR ALREADY GOT SPELL");
        }
    }

    //If the player presses "No", just return to the shop
    public void spellNotBought()
    {
        buySpellCanvas.enabled = false;
        buySpell.enabled = false;
        dontBuySpell.enabled = false;
        buyingSpells = false;
        closeEnoughToShop = true;
    }

    // A method for display the tooltip for the spells.
    void SpellTooltipInfo(string headLine, string spellDescriptionText, int cost, float cooldown, float damage, float AOERadius, float duration)
    {
        toolTipCanvas.enabled = true;

        spellHeadline.GetComponent<Text>().text = headLine;
        spellDescription.GetComponent<Text>().text = spellDescriptionText;
        spellCost.GetComponent<Text>().text = "Cost: " + cost;
        spellCooldown.GetComponent<Text>().text = "Coooldown: " + cooldown + " sec";
        spellDamage.GetComponent<Text>().text = "Damage: " + damage;
        spellDPS.GetComponent<Text>().text = "DPS: " + (damage / cooldown).ToString("F2"); // ToString F2 rounds to 2 decimals
        spellAOE.GetComponent<Text>().text = "AOE Radius: " + AOERadius;
        spellDuration.GetComponent<Text>().text = "Duration: " + duration + " sec"; ;
    }

    //Reset the canvas shop back to default (e.g. when exiting the shop)
    void ResetShop()
    {
        canvasPlaceholder.SetActive(false);
        spellSchoolCanvas1.enabled = false;
        spellSchoolCanvas2.enabled = false;
        spellSchoolCanvas3.enabled = false;
        toolTipCanvas.enabled = false;
        buySpellCanvas.enabled = false;
        buySpell.enabled = false;
        dontBuySpell.enabled = false;
        buyingSpells = false;
        closeEnoughToShop = false;
        isCurrentlyShopping = false;
        Debug.Log("Shop Exited");
    }

    //IEnumerator to control how fast players gain money over time; the inputted value in StartCoroutine will determine the rate of moneygain.
    private IEnumerator<float> _ResourceProvider(float waitTime)
    {
        while (true)
        {
            playerResource.CurrentResources+=3;

            checkAffordance();

            yield return Timing.WaitForSeconds(waitTime);
        }
    }

    //Checks if player can afford some shit; if he can, dont paint the image shit
    private void checkAffordance()
    {
        for (int i = 0; i < mySpellImages.Length; i++)
        {
            if (spellCostArray[i] > playerResource.CurrentResources)
            {
                mySpellImages[i].GetComponent<Image>().color = Color.red;
            }
            else
            {
                mySpellImages[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
}