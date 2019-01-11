using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

// A behaviour father to be implemented to differents Objects
public class Damageable :MonoBehaviour {

    public Color colorOnHit;

    public float health, maxHealth, percentAdded, invunerableTime;

    public bool canDie, isPlayer;

    public Slider healthBar;

    private PlayerStats playerStats;

    private float damageOverTime;

    private int timeDmgOverTime;

    private bool isInvunerable;

    private void Start() {
        isInvunerable = false;
        health = maxHealth;
        //healthBar.value = CalculateHealth();

        if (isPlayer) {
            playerStats = gameObject.GetComponent<PlayerStats>();
            SetStatus();
        }

    }

    public void SetStatus() {
        playerStats.health = health;
        playerStats.maxHealth = maxHealth;
        playerStats.percentAdded = percentAdded;
    }

    private float CalculateHealth() {
        return (health / maxHealth);
    }

    private void Update() {
        CheckLifeStatus();
    }

    public void ReceiveDamage(float dmg) {
        if (!isInvunerable) {

            if(isPlayer){ CameraBehaviour.instance.DoShake(); }

            health -= dmg;
            OnRecieveDamage();
            healthBar.value = CalculateHealth();
            
            isInvunerable = true;
            StartCoroutine(GainInvunerability(invunerableTime));
        }
    }

    public void ReceiveDamageOverTime(float dmg, int dmgDuration) {
        damageOverTime = dmg;
        timeDmgOverTime = dmgDuration;
        InvokeRepeating("ApplyDmg", 1, dmgDuration);
    }

    public void IncreaseHealth(float addHealth) {
        maxHealth += addHealth;
        Heal(addHealth);
    }

    public void IncreasePerCentHealth(float addHealthPerCent) {
        float percent = maxHealth * (addHealthPerCent / 100);
        percentAdded += percent;

        IncreaseHealth(percent);
        Heal(percent);
    }

    public void Heal(float moreHealth) {
        health += moreHealth;
        LifeCaped();
     //   healthBar.value = CalculateHealth();
    }

    void LifeCaped() {
        if (health > maxHealth)
            health = maxHealth;
    }

    bool CheckLifeStatus() {
        if (health <= 0) {
            health = 0;
            OnDeath();
            return true;
        } else
            return false;
    }

    void ApplyDmg() {
        if (timeDmgOverTime <= 0)
            CancelInvoke("ApplyDmg");
        OnRecieveDamage();
        health -= damageOverTime;
        timeDmgOverTime -= 1;
      // healthBar.value = CalculateHealth();
    }

    IEnumerator GainInvunerability(float time) {
        yield return new WaitForSeconds(time);
        isInvunerable = false;
    }

    void OnRecieveDamage() {

    }

    void OnDeath() {
        if (canDie) {
            Destroy(gameObject);
        }
        Debug.Log("Murri");
    }

}