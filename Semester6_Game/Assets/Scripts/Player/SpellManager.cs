using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MovementEffects;

public class SpellManager : Photon.MonoBehaviour
{

    public PhotonView m_photonView;
    public CharacterManager_NET charMananager;
    public PlayerHealth_NET playerHealth;
    private MousePositionScript mousePos;
    public GameObject[] Spell;
    public SpellData[] m_spellData;
    public Transform spellOrigin;
    private KeyCode[] keyCodes = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T };
    private List<int> mySpells = new List<int>();
    public List<float> myCooldown = new List<float>();
    public List<float> myCooldownMax = new List<float>();
    private KeyCode currentKey = KeyCode.Q;
    private bool spellSelected = false;
    public bool[] isAOE;
    private float[] cooldown;
    public GameObject reticle_AOE;
    public GameObject reticle_Direction;
    public List<SpellData> m_sceneAbilities = new List<SpellData>();
    public int m_LastInstantiatedID = 0;
    private bool canCastSpells = true;


    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth_NET>();
        charMananager = GetComponent<CharacterManager_NET>();
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
            reticle_Direction = (GameObject)Instantiate(reticle_Direction, new Vector3(transform.position.x, transform.position.y + 100, transform.position.x), reticle_Direction.transform.rotation);
        }
    }

    void GetMaxCooldowns()
    {
        cooldown = new float[Spell.Length];
        for (int i = 0; i < Spell.Length; i++)
        {
            cooldown[i] = m_spellData[i].cooldown();
        }
    }

    IEnumerator<float> _StartCooldown(int index)
    {
        myCooldown[index] = myCooldownMax[index];
        while (myCooldown[index] > 0)
        {
            myCooldown[index] -= Time.deltaTime;
            yield return 0f;
        }
    }

    bool isSpellReady(int index)
    {
        if (myCooldown[index] <= 0)
        {
            return true;
        }
        return false;
    }

    private void getSpellTypes()
    {
        isAOE = new bool[Spell.Length];
        for (int i = 0; i < Spell.Length; i++)
        {
            isAOE[i] = m_spellData[i].isAOE();
        }
    }

    private void hideReticles()
    {
        reticle_AOE.SetActive(false);
        reticle_Direction.SetActive(false);
    }

    private void getSpellData()
    {
        m_spellData = new SpellData[Spell.Length];
        for (int i = 0; i < Spell.Length; i++)
        {
            m_spellData[i] = Spell[i].GetComponent<SpellData>();
        }
    }

    private void positionReticles()
    {
        //AOE
        Vector3 reticlePos = mousePos.getMouseWorldPoint();
        reticle_AOE.transform.position = reticlePos;
        reticle_AOE.transform.Rotate(Vector3.up * Time.deltaTime * 20f);

        //Directional
        reticle_Direction.transform.position = transform.position;
        Vector3 targetTransform = mousePos.getMouseWorldPoint();
        reticle_Direction.transform.LookAt(new Vector3(targetTransform.x, 0.0f, targetTransform.z));
    }

    void Update()
    {
        if (canCastSpells)
        {
            if (m_photonView.isMine)
            {
                lastKeySelected();
                positionReticles();
                for (int i = 0; i < keyCodes.Length; i++)
                {
                    if (currentKey == keyCodes[i] && mySpells.Count > i)
                    {
                        if (m_spellData[mySpells[i]].isUtility())
                        {
                            if (Input.GetKeyDown(currentKey) && isSpellReady(i))
                            {
                                ShoutSpell(mySpells[i], GetProjectileSpawnPos(), mousePos.getMouseWorldPoint());
                                Timing.RunCoroutine(_StartCooldown(i));
                            }
                        }
                        else
                        {
                            if (Input.GetMouseButtonDown(0) && spellSelected)
                            {
                                if (isSpellReady(i))
                                {
                                    ShoutSpell(mySpells[i], GetProjectileSpawnPos(), mousePos.getMouseWorldPoint());
                                    Timing.RunCoroutine(_StartCooldown(i));
                                }
                            }
                        }
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
                if (spellSelected)
                {
                    hideReticles();
                    spellSelected = false;
                }
                else
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
    }

    public void addSpell(int spell_ID)
    {
        mySpells.Add(spell_ID);
        setCooldown(spell_ID);
    }

    public void setCooldown(int spell_ID)
    {
        myCooldown.Add(0);
        myCooldownMax.Add(m_spellData[spell_ID].cooldown());
    }

    public void ShoutSpell(int spell_ID, Vector3 origin, Vector3 target)
    {
        hideReticles();
        spellSelected = false;
        m_LastInstantiatedID++;
        m_photonView.RPC("castSpell", PhotonTargets.All, spell_ID, origin, target, PhotonNetwork.player.ID, m_LastInstantiatedID);
    }

    public void SendAbilityHit(int ID, bool displayImpactEffect, bool destroyAbility)
    {
        m_photonView.RPC("OnAbilityHit", PhotonTargets.Others, ID, displayImpactEffect, destroyAbility);
    }

    public void SetSpellDirection(int Instantiate_ID, Vector3 origin, Vector3 target, int OwnerID)
    {
        m_photonView.RPC("setNewSpellDir", PhotonTargets.All, origin, target, Instantiate_ID, OwnerID);
    }

    public void SetLastPlayerHit(int InstantiateID, int index)
    {
        m_photonView.RPC("lastPlayerHit", PhotonTargets.All, InstantiateID, index);
    }

    [PunRPC]
    public void lastPlayerHit(int InstantiateID, int playerhitID)
    {
        m_sceneAbilities.RemoveAll(item => item = null);
        SpellData spell = m_sceneAbilities.Find(item => item.InstantiateID() == InstantiateID);
        for (int i = 0; i < charMananager.Players.Count; i++)
        {
            if (charMananager.Players[i].playerID == playerhitID)
            {
                spell.lastPlayerTarget = charMananager.Players[i].playerHealth();
            }
        }
    }

    [PunRPC]
    void setNewSpellDir(Vector3 origin, Vector3 target, int InstantiateID, int OwnerID, PhotonMessageInfo info)
    {
        double timestamp = info.timestamp;
        m_sceneAbilities.RemoveAll(item => item = null);
        SpellData spell = m_sceneAbilities.Find(item => item.InstantiateID() == InstantiateID);
        if (spell == null)
        {
            Debug.Log("Spell is null!!!!");
        }
        SpellMovement spellMove = spell.GetComponent<SpellMovement>();
        spell.setOwnerID(OwnerID);
        for (int i = 0; i < charMananager.Players.Count; i++)
        {
            if (charMananager.Players[i].playerID == OwnerID)
            {
                spell.setOwner(charMananager.Players[i].GetComponent<SpellManager>());
            }
        }
        spellMove.SetCreationTime(timestamp);
        spellMove.SetStartPosition(origin);
        spellMove.SetSpellDirection(origin, target);

    }

    [PunRPC]
    void castSpell(int spellID, Vector3 startPos, Vector3 targetPos, int ownerID, int InstantiateID, PhotonMessageInfo info)
    {
        double timestamp = info.timestamp;
        bool isUtility = m_spellData[spellID].isUtility();

        if (isAOE[spellID])
        {
            GameObject go = (GameObject)Instantiate(Spell[spellID], targetPos, Quaternion.identity);
            SpellData spellData = go.GetComponent<SpellData>();
            spellData.setOwnerID(ownerID);
            spellData.setOwner(this);
            spellData.setSpellID(spellID);

        }
        else if (isUtility)
        {
            GameObject go = (GameObject)Instantiate(Spell[spellID], transform.position, Quaternion.identity);
            SpellData spellData = go.GetComponent<SpellData>();
            spellData.setOwnerID(ownerID);
            spellData.setOwner(this);
        }
        else
        {
            CreateProjectile(startPos, targetPos, timestamp, ownerID, spellID, InstantiateID);
        }
    }

    [PunRPC]
    public void OnAbilityHit(int ID, bool displayImpactEffect, bool destroyAbility)
    {
        m_sceneAbilities.RemoveAll(item => item = null);
        SpellData spell = m_sceneAbilities.Find(item => item.InstantiateID() == ID);

        if (spell == null)
        {
            Debug.Log("Something went wrong - spell is null");
        }
        if (spell != null)
        {
            if (destroyAbility)
            {
                m_sceneAbilities.Remove(spell);
                if (displayImpactEffect)
                    spell.AbilityImpactEffect();
                Destroy(spell.gameObject);
            }
            else
            {
                spell.AbilityImpactEffect();
            }
        }

    }

    public void CreateProjectile(Vector3 startPos, Vector3 targetPos, double createTime, int ownerID, int spellID, int instantiateID)
    {
        GameObject go = (GameObject)Instantiate(Spell[spellID], startPos, Quaternion.identity);
        SpellData spellData = go.GetComponent<SpellData>();
        SpellMovement projectileMovement = go.GetComponent<SpellMovement>();
        spellData.setOwnerID(ownerID);
        if (ownerID == GetComponent<CharacterManager_NET>().playerID)
        {
            spellData.setOwner(this);
        }
        spellData.setInstantiateID(instantiateID);
        projectileMovement.SetCreationTime(createTime);
        projectileMovement.SetStartPosition(startPos);
        projectileMovement.SetSpellDirection(startPos, targetPos);
        m_sceneAbilities.Add(spellData);
    }


    public Vector3 GetProjectileSpawnPos()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y = 0.64f;
        return spawnPos;
    }

    private void displayReticle(int spellID)
    {
        bool isAOE = m_spellData[spellID].isAOE();
        bool isUtility = m_spellData[spellID].isUtility();
        float AOEScale = m_spellData[spellID].radius() * 2;
        if (isAOE)
        {
            reticle_Direction.SetActive(false);
            reticle_AOE.SetActive(true);
            reticle_AOE.transform.localScale = new Vector3(AOEScale, AOEScale, AOEScale);
        }
        else if (isUtility)
        {
            return;
        }
        else
        {
            reticle_AOE.SetActive(false);
            reticle_Direction.SetActive(true);
        }
    }

    public bool FreezePlayerSpellCasting()
    {
        return canCastSpells = false;
    }

    public bool UnfreezePlayerSpellCasting()
    {
        return canCastSpells = true;
    }
}
