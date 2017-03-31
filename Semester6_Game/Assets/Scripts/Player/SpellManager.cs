using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MovementEffects;

public class SpellManager : Photon.MonoBehaviour
{

    private PhotonView m_photonView;
    private MousePositionScript mousePos;
    public GameObject[] Spell;
    public SpellData[] spellData;
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
    private List<GameObject> sceneAbilities = new List<GameObject>();
    //private List<float[]> cooldown = new List<float[]>();


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
            cooldown[i] = spellData[i].cooldown();
        }
    }

    IEnumerator<float> _StartCooldown(int index)
    {
        myCooldown[index] = myCooldownMax[index];
        while (myCooldown[index] > 0)
        {
            myCooldown[index] -= Time.deltaTime;
            yield return 0f;
            Debug.Log(myCooldown[index]);
        }
    }

    bool isSpellReady(int index)
    {
        Debug.Log(myCooldown[index]);
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
            isAOE[i] = spellData[i].isAOE();
        }
    }

    private void hideReticles()
    {
        reticle_AOE.SetActive(false);
        reticle_Direction.SetActive(false);
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
        setCooldown(spell_ID);
    }

    public void setCooldown(int spell_ID)
    {
        myCooldown.Add(0);
        myCooldownMax.Add(spellData[spell_ID].cooldown());
    }

    public void ShoutSpell(int spell_ID)
    {
        hideReticles();
        spellSelected = false;
        //m_photonView.RPC("castSpell", PhotonTargets.AllBuffered, spell_ID, mousePos.getMouseWorldPoint(), PhotonNetwork.player.ID);
        m_photonView.RPC("castSpell", PhotonTargets.All, spell_ID, GetProjectileSpawnPos(), mousePos.getMouseWorldPoint(), PhotonNetwork.player.ID);
    }


    [PunRPC]
    void castSpell(int spellID,Vector3 startPos, Vector3 targetPos, int ownerID, PhotonMessageInfo info)
    {
        double timestamp = info.timestamp;

        if (isAOE[spellID])
        {
            GameObject go = (GameObject)Instantiate(Spell[spellID], targetPos, Quaternion.identity);
            go.GetComponent<SpellData>().setOwnerID(ownerID);
        }
        else
        {
            CreateProjectile(startPos, targetPos, timestamp, ownerID, spellID);
        }
        //GameObject spell = Instantiate(Spell[spellID],)
    }

    public void CreateProjectile(Vector3 startPos, Vector3 targetPos, double createTime, int ownerID, int spellID)
    {
        GameObject go = (GameObject)Instantiate(Spell[spellID], startPos, Quaternion.identity);
        sceneAbilities.Add(go);
        SpellMovement projectileMovement = go.GetComponent<SpellMovement>();
        go.GetComponent<SpellData>().setOwnerID(ownerID);
        projectileMovement.SetCreationTime(createTime);
        projectileMovement.SetStartPosition(startPos);
        projectileMovement.SetSpellDirection(startPos, targetPos);
    }


    Vector3 GetProjectileSpawnPos()
    {
        return transform.position;
    }

    private void displayReticle(int spellID)
    {
        bool isAOE = spellData[spellID].isAOE();
        if (isAOE)
        {
            reticle_Direction.SetActive(false);
            reticle_AOE.SetActive(true);
            reticle_AOE.GetComponent<Projector>().orthographicSize = spellData[spellID].radius();
        }
        else
        {
            reticle_AOE.SetActive(false);
            reticle_Direction.SetActive(true);
        }
    }
}
