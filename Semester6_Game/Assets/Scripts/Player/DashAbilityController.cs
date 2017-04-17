using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashAbilityController : MonoBehaviour
{

    public KeyCode triggerKey = KeyCode.Space;

    public Image darkMask;
    public Text coolDownText;

    public PhotonView m_PhotonView;
    public Ability_Dash dashAbility;
    public SpellManager spellManager;

    private float coolDownTimeLeft;
    private float coolDownDuration;
    private float nextReadyTime;

    // Use this for initialization
    void Start()
    {
        if (m_PhotonView.isMine)
        {
            coolDownDuration = dashAbility.coolDownTime;
            darkMask.fillAmount = 0f;
            coolDownText.text = "";
            nextReadyTime = 0;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextReadyTime)
        {
            AbilityReady();

            if (Input.GetKeyDown(triggerKey))
            {
                ButtonTriggered();
            }
        }
        else
        {
            CoolDown();
        }
    }

    private void AbilityReady()
    {
        coolDownText.text = "";
        darkMask.fillAmount = 0f;
    }

    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCD = Mathf.Ceil(coolDownTimeLeft);
        coolDownText.text = roundedCD.ToString();
        darkMask.fillAmount = coolDownTimeLeft / coolDownDuration;
    }

    private void ButtonTriggered()
    {
        if (spellManager.canCastSpells)
        {
            nextReadyTime = coolDownDuration + Time.time;
            coolDownTimeLeft = coolDownDuration;
            dashAbility.Dash();
            spellManager.teleportControl.StopPlayerRecall();
        }
    }
}
