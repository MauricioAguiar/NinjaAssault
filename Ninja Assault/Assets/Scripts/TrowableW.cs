﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrowableW : MonoBehaviour {

    public float fireRate = 0;

    public float damage = 10;

    public LayerMask notToHit;

    public float timeToFire = 0;

    public LayerMask whatToHit;

    Transform firePoint;

	// Use this for initialization
	void Awake () {
        firePoint = transform.Find("FirePoint");

        if(firePoint == null){
            Debug.LogError("Bep Bop, FirePoint Error, FirePoint Error");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (fireRate.Equals(0)){
            if (Input.GetButtonDown("Fire1")){
                Shoot();
            }
        }
        else{
            if (Input.GetButton("Fire1") && Time.time > timeToFire){
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            
            }
        }
	}

    void Shoot(){
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.blue);
        if (hit.collider != null)
        {

            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + damage + " damage.");
        }
    }
}
