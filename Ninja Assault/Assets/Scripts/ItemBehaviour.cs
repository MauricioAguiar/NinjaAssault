using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour {

    public GameObject EspecialEffect;

    public String itemName, itemDescription;

    public float addHealth, addHealthPercent, addDamage, addDamagePercent, addDamageOverTime,addSpeed;

    public float addPoison, addLifeSteal;

    private void OnTriggerEnter2D(Collider2D collision) {
        

        if (collision.CompareTag("Player")){

            PowerEffect(collision);            

        }

    }

    private void PowerEffect(Collider2D collision) {

        Instantiate(EspecialEffect, transform.position, transform.rotation);

        collision.gameObject.GetComponent<PlayerController>().SetVelocity(addSpeed);

        Damageable dmgProp = collision.gameObject.GetComponent<Damageable>();

        dmgProp.IncreaseHealth(addHealth);
        dmgProp.IncreasePerCentHealth(addHealthPercent);

    }
}
