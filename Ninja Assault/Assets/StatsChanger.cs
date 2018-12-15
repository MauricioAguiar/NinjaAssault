using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsChanger : MonoBehaviour {

    public float damage, damageOverTime, lifeStealAmount, health, maxHealth, percentAdded, addPlayerSpeed;

    public int timeDuration;

    public bool hasLifeSteal;

    public bool damagerFromPlayer;

    private PlayerStats pS;

    // Use this for initialization
    void Start () {
        pS = PlayerStats.instance;
	}

    void SetValues() {
        pS.damage += damage;
        pS.damageOverTime += damageOverTime;
        pS.lifeStealAmount += lifeStealAmount;
        pS.playerSpeed += addPlayerSpeed;
        pS.hasLifeSteal = hasLifeSteal;

    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            SetValues();
        }
    }


}
