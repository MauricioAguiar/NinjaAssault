﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public List<GameObject> PowerUps;

    public float damage, damageOverTime, lifeStealAmount, health, maxHealth, percentAdded, playerSpeed, ExtraSpeed;

    public int timeDuration;

    public bool hasLifeSteal;

    public bool damagerFromPlayer;

    public static PlayerStats instance;

    void ToInstance() {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        ToInstance();
        ExtraSpeed = 200;
        playerSpeed = (1 + ExtraSpeed / 10) * 150;
    }

    // Update is called once per frame
    private void FixedUpdate() {
        SetValues();
        playerSpeed = (1 + ExtraSpeed / 10) * 150;
    }


    void SetValues() {
        health = gameObject.GetComponent<Damageable>().health;
        maxHealth = gameObject.GetComponent<Damageable>().maxHealth;
    }
}
