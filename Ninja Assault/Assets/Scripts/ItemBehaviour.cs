using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour {

    public GameObject EspecialEffect;

    public String itemName, itemDescription;

    public float addHealth, addHealthPercent, addDamage, addDamagePercent, addDamageOverTime, addSpeed;

    public int addDmgOverTimeDuration;

    public float addPoison, addLifeStealAmount;

    public bool addLifeSteal,hasDamageOverTime;

    public enum Rarity {Common, Uncommon, Rare, Mythic, Special};

    public Rarity rarity;

    private PlayerStats pS;

    void Start() {
        pS = PlayerStats.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        

        if (collision.CompareTag("Player")){

            PowerEffect(collision);
            pS.PowerUps.Add(gameObject);
            Destroy(gameObject, 1);
        }
    }

    void SetValues() {
        pS.damage += addDamage;
        pS.damageOverTime += addDamageOverTime;
        pS.lifeStealAmount += addLifeStealAmount;
        pS.timeDuration = addDmgOverTimeDuration;
        pS.ExtraSpeed += addSpeed;

        if (addLifeSteal) {
            pS.hasLifeSteal = addLifeSteal;
        }
    }

    private void PowerEffect(Collider2D collision) {

        //Instantiate(EspecialEffect, transform.position, transform.rotation);

        SetValues();

        Damageable dmgProp = collision.gameObject.GetComponent<Damageable>();

        dmgProp.IncreaseHealth(addHealth);
        dmgProp.IncreasePerCentHealth(addHealthPercent);
    }
}
