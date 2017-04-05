using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MovementEffects;

public class SpellManager : Photon.MonoBehaviour
{

    public PhotonView m_photonView;
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
    private int m_LastInstantiatedID = 0;
    private bool canCastSpells = true;
    private float timeStampSpellCasted = 0;


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
        reticle_AOE.transform.Rotate(Vector3.back * Time.deltaTime * 20f);

        //Directional
        reticle_Direction.transform.position = transform.position;
        Vector3 targetTransform = mousePos.getMouseWorldPoint();
        reticle_Direction.transform.LookAt(new Vector3(targetTransform.x, 0.0f, targetTransform.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (canCastSpells)
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
                            if (isSpellReady(i))
                            {
                                ShoutSpell(mySpells[i]);
                                Timing.RunCoroutine(_StartCooldown(i));
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

    public void ShoutSpell(int spell_ID)
    {
        hideReticles();
        timeStampSpellCasted = Time.time;
        spellSelected = false;
        m_LastInstantiatedID++;
        m_photonView.RPC("castSpell", PhotonTargets.All, spell_ID, GetProjectileSpawnPos(), mousePos.getMouseWorldPoint(), PhotonNetwork.player.ID, m_LastInstantiatedID);
    }

    public void SendAbilityHit(int ID)
    {
        m_photonView.RPC("OnAbilityHit", PhotonTargets.Others, ID);
    }

    [PunRPC]
    void castSpell(int spellID, Vector3 startPos, Vector3 targetPos, int ownerID, int InstantiateID, PhotonMessageInfo info)
    {
        double timestamp = info.timestamp;

        if (isAOE[spellID])
        {
            GameObject go = (GameObject)Instantiate(Spell[spellID], targetPos, Quaternion.identity);
            go.GetComponent<SpellData>().setOwnerID(ownerID);
        }
        else
        {
            CreateProjectile(startPos, targetPos, timestamp, ownerID, spellID, InstantiateID);
        }
    }

    [PunRPC]
    public void OnAbilityHit(int ID)
    {
        m_sceneAbilities.RemoveAll(item => item = null);
        SpellData spell = m_sceneAbilities.Find(item => item.InstantiateID() == ID);

        if (spell == null)
        {
            Debug.Log("Spell is null");
        }
        if (spell != null)
        {
            m_sceneAbilities.Remove(spell);
            Destroy(spell.gameObject);
        }

    }

    public void CreateProjectile(Vector3 startPos, Vector3 targetPos, double createTime, int ownerID, int spellID, int instantiateID)
    {
        GameObject go = (GameObject)Instantiate(Spell[spellID], startPos, Quaternion.identity);
        SpellData spellData = go.GetComponent<SpellData>();
        SpellMovement projectileMovement = go.GetComponent<SpellMovement>();
        spellData.setOwnerID(ownerID);
        spellData.setOwner(this);
        spellData.setInstantiateID(instantiateID);
        Debug.Log("Spell created with ID: " + instantiateID);
        projectileMovement.SetCreationTime(createTime);
        projectileMovement.SetStartPosition(startPos);
        projectileMovement.SetSpellDirection(startPos, targetPos);
        m_sceneAbilities.Add(spellData);
    }


    Vector3 GetProjectileSpawnPos()
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y = 0.64f;
        return spawnPos;
    }

    private void displayReticle(int spellID)
    {
        bool isAOE = m_spellData[spellID].isAOE();
        if (isAOE)
        {
            reticle_Direction.SetActive(false);
            reticle_AOE.SetActive(true);
            reticle_AOE.GetComponent<Projector>().orthographicSize = m_spellData[spellID].radius();
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
