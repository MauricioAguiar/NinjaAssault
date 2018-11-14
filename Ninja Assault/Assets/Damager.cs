using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour father to be implemented to differents Objects
public class Damager :MonoBehaviour {

    public float damage, damageOverTime, timeDuration;

    public Collision2D col;

    

    private void OnCollisionEnter2D(Collision2D collision) {
       collision.gameObject.GetComponent<HealthSystem>().
    }
}