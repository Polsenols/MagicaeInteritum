using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    private int maxHealth;
    private float currentHealth;
    private Image healthFill;

    public PlayerHealth_NET playerStats;

	// Use this for initialization
	void Start ()
    {
        healthFill = GetComponent<Image>();
        healthFill.fillAmount = 1;
        maxHealth = playerStats.maxHealth;
        currentHealth = playerStats.getHealth();
	}
	
	// Instead of using update, this function could be called from PlayerHealth when the player takes damage
	void Update ()
    {
        currentHealth = playerStats.getHealth();
        healthFill.fillAmount = currentHealth / maxHealth;
	}
}
