using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSelector : MonoBehaviour {

    SpriteRenderer LBC, LB, LT, LTC, TL, TR, RBC, RB, RT, RTC, DL, DR;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Walls")) {
            if (collision.transform.position.x == gameObject.transform.position.x) {

            }

        }
    }
}
