using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour father to be implemented to differents Objects
public class Damager :MonoBehaviour {

    public float damage, damageOverTime, timeDuration;

    public LayerMask layerToHit;

    private void Start() {


    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (((1 << collision.gameObject.layer) & layerToHit) != 0) {
            
            collision.gameObject.GetComponent<Damageable>().ReceiveDamage(damage);

            if(damageOverTime > 0)
                collision.gameObject.GetComponent<Damageable>().ReceiveDamageOverTime(damageOverTime, timeDuration);
        }
    }
}