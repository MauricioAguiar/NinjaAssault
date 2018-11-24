using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour father to be implemented to differents Objects
public class Damager :MonoBehaviour {

    public float damage, damageOverTime, lifeStealAmount;
        
    public int timeDuration;

    public bool hasLifeSteal;

    public LayerMask layerToHit;

    private float timerCd;

    public bool damagerFromPlayer;

    private GameObject gameObjectWithDamageable;

    private Damageable damageable;

    private void Start() {
        timerCd = 2;

        if (damagerFromPlayer){
            gameObjectWithDamageable = GameObject.FindGameObjectWithTag("Player");
            damageable = gameObjectWithDamageable.GetComponent<Damageable>();
           
        } else if (gameObject.GetComponent<Damageable>() != null)
            damageable = GetComponent<Damageable>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (((1 << collision.gameObject.layer) & layerToHit) != 0) {
            
            float prevLife = collision.gameObject.GetComponent<Damageable>().health;

            collision.gameObject.GetComponent<Damageable>().ReceiveDamage(damage);

            float posLife = collision.gameObject.GetComponent<Damageable>().health;

            if (hasLifeSteal && prevLife!= posLife)
                damageable.Heal(damage* lifeStealAmount*0.01f);

            if(damageOverTime > 0)
                collision.gameObject.GetComponent<Damageable>().ReceiveDamageOverTime(damageOverTime, timeDuration);
        }        
    }

    private void OnCollisionStay2D(Collision2D collision) {
        
        if (((1 << collision.gameObject.layer) & layerToHit) != 0 & timerCd <= 0 ) {

            float prevLife = collision.gameObject.GetComponent<Damageable>().health;

            collision.gameObject.GetComponent<Damageable>().ReceiveDamage(damage);
            timerCd = 1f;

            float posLife = collision.gameObject.GetComponent<Damageable>().health;

            if (hasLifeSteal && prevLife != posLife)
                damageable.Heal(damage * lifeStealAmount * 0.01f);

            if (damageOverTime > 0)
                collision.gameObject.GetComponent<Damageable>().ReceiveDamageOverTime(damageOverTime, timeDuration);
        }
        timerCd -= Time.deltaTime;
    }
}