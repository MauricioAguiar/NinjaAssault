using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour father to be implemented to differents Objects
public class Damager :MonoBehaviour {

    public float damage, damageOverTime;
        
    public int timeDuration;

    public LayerMask layerToHit;

    private float timerCd;

    private void Start() {
        timerCd = 2;
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (((1 << collision.gameObject.layer) & layerToHit) != 0) {
            
            collision.gameObject.GetComponent<Damageable>().ReceiveDamage(damage);

            if(damageOverTime > 0)
                collision.gameObject.GetComponent<Damageable>().ReceiveDamageOverTime(damageOverTime, timeDuration);
        }        
    }

    private void OnCollisionStay2D(Collision2D collision) {
        
        if (((1 << collision.gameObject.layer) & layerToHit) != 0 & timerCd <= 0 ) {
            collision.gameObject.GetComponent<Damageable>().ReceiveDamage(damage);
            timerCd = 1f;

            if (damageOverTime > 0)
                collision.gameObject.GetComponent<Damageable>().ReceiveDamageOverTime(damageOverTime, timeDuration);
        }
        timerCd -= Time.deltaTime;
    }
}