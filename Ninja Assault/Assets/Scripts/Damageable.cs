﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

// A behaviour father to be implemented to differents Objects
public class Damageable :MonoBehaviour {

    public float health, maxHealth, percentAdded, invunerableTime;

    public Slider healthBar;

    private float damageOverTime, timeDmgOverTime;

    private bool isInvunerable;

    private void Start() {
        invunerableTime = 1;
        isInvunerable = false;
        health = maxHealth;
        healthBar.value = CalculateHealth();
    }

    private float CalculateHealth() {
        return (health / maxHealth);
    }

    private void Update() {
        CheckLifeStatus();
    }

    public void ReceiveDamage(float dmg) {
        if (!isInvunerable) {
            health -= dmg;
            OnRecieveDamage();
            healthBar.value = CalculateHealth(); 
        }
        GainInvunerability(invunerableTime);
    }

    public void ReceiveDamageOverTime(float dmg, float dmgDuration) {
        damageOverTime = dmg;
        timeDmgOverTime = dmgDuration;
        InvokeRepeating("ApplyDmg", 0.3f, dmgDuration);
    }

    void IncreaseHealth(float addHealth) {
        maxHealth += addHealth;
        Heal(addHealth);
    }

    void IncreasePerCentHealth(float addHealthPerCent) {
        float percent = maxHealth * (addHealthPerCent / 100);
        percentAdded += percent;

        IncreaseHealth(percent);
        Heal(percent);
    }

    void Heal(float moreHealth) {
        health += moreHealth;
        LifeCaped();
        healthBar.value = CalculateHealth();
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
        timeDmgOverTime -= Time.deltaTime;
        healthBar.value = CalculateHealth();
    }

    void GainInvunerability(float time) {
        while (time > 0) {
            isInvunerable = true;
            time -= Time.deltaTime;
        }
        isInvunerable = false;
    }

    void OnRecieveDamage() {

    }

    void OnDeath() {
        Debug.Log("Murri");
    }

}