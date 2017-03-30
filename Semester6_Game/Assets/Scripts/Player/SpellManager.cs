using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpellManager : Photon.PunBehaviour
{

    private PhotonView m_photonView;
    private MousePositionScript mousePos;
    public GameObject[] Spell;
    public SpellData[] spellData;
    private KeyCode[] keyCodes = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T };
    private List<int> mySpells = new List<int>();
    private KeyCode currentKey = KeyCode.Q;
    private bool spellSelected = false;
    public bool[] isAOE;
    public GameObject reticle_AOE;

    void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
        mousePos = GetComponent<MousePositionScript>();
    }

    void Start()
    {
        getSpellData();
        getSpellTypes();
        if (m_photonView.isMine)
        {
            reticle_AOE = (GameObject)Instantiate(reticle_AOE, transform.position, reticle_AOE.transform.rotation);
            //addSpell(0);
        }
    }

    private void getSpellTypes()
    {
        isAOE = new bool[Spell.Length];
        for (int i = 0; i < Spell.Length; i++)
        {
            isAOE[i] = spellData[i].isAOE();
        }
    }

    private void hideReticles()
    {
        reticle_AOE.SetActive(false);
    }

    private void getSpellData()
    {
        spellData = new SpellData[Spell.Length];
        for (int i = 0; i < Spell.Length; i++)
        {
            spellData[i] = Spell[i].GetComponent<SpellData>();
        }
    }

    private void positionReticles()
    {
        Vector3 reticlePos = mousePos.getMouseWorldPoint();
        reticle_AOE.transform.position = reticlePos;
        reticle_AOE.transform.Rotate(Vector3.back * Time.deltaTime * 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_photonView.isMine)
        {
            lastKeySelected();
            positionReticles();
            if (Input.GetMouseButtonDown(0) && spellSelected)
            {
                for (int i = 0; i < keyCodes.Length; i++)
                {
                    if (currentKey == keyCodes[i] && mySpells.Count > i)
                    {
                        ShoutSpell(mySpells[i]);
                    }
                }
            }
        }
    }

    private void lastKeySelected()
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                if (mySpells.Count > 0)
                {
                    displayReticle(mySpells[i]);
                    spellSelected = true;
                    currentKey = keyCodes[i];
                }
            }
        }
    }

    public void addSpell(int spell_ID)
    {
        mySpells.Add(spell_ID);
    }

    public void ShoutSpell(int spell_ID)
    {
        hideReticles();
        spellSelected = false;
        m_photonView.RPC("castSpell", PhotonTargets.AllBuffered, spell_ID, mousePos.getMouseWorldPoint(), PhotonNetwork.player.ID);
    }

    [PunRPC]
    void castSpell(int spellID, Vector3 position, int ownerID)
    {
        if (isAOE[spellID])
        {
            GameObject go = (GameObject)Instantiate(Spell[spellID], position, Quaternion.identity);
            go.GetComponent<SpellData>().setOwnerID(ownerID);
        }
        else
        {
            //GameObject go = (GameObject)Instantiate(Spell[spellID], transform.position, Quaternion.identity);
        }
        //GameObject spell = Instantiate(Spell[spellID],)
    }

    private void displayReticle(int spellID)
    {
        bool isAOE = spellData[spellID].isAOE();
        if (isAOE)
        {
            reticle_AOE.SetActive(true);
            reticle_AOE.GetComponent<Projector>().orthographicSize = spellData[spellID].radius();
        }
        else
        {
            reticle_AOE.SetActive(false);
        }
    }
}
