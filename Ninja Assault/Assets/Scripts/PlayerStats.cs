using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public float damage, damageOverTime, lifeStealAmount, health, maxHealth, percentAdded, playerSpeed;

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
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
