using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour father to be implemented to differents Objects
public class HealthSystem :  MonoBehaviour {

    public float invunerableCD;

    private float health, maxHealth, percentAdded, startedHealth, invunerableTime;

    public Damager damager;

    private float damage;

    private bool isDead;


    private void Awake() {

    }

    bool ReceiveDamage(Damager dmg) {
        health -= dmg.damage;
       
        return CheckLifeStatus();
    }

    bool ReceiveDamageOverTime(Damager dmg) {

        health -= dmg.damageOverTime;

        return CheckLifeStatus();
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
    }

    void LifeCaped() {
        if (health > maxHealth)
            health = maxHealth;
    }

    bool CheckLifeStatus() {
        if (health <= 0){
            health = 0;
            return true;
        }else
            return false;
    }

    void OnRecieveDamage() {

    }

}