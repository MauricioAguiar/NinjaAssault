using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy :MonoBehaviour {

    public float speed;

    private Transform target;

    private Rigidbody2D rb;

    public Animator ani;

    public Vector2 atualPosition;
    
    // Use this for initialization
    void Start() {

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = 2;
        ani.GetComponent<Animator>();
        rb.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        movAnimation();
    }

    void movAnimation() {

        atualPosition = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 4) {

          transform.position = atualPosition;

        }


        float x = target.position.x - atualPosition.x;
        float y = target.position.y - atualPosition.y;
        float z = atualPosition.magnitude;

        ani.SetFloat("Horizontal", x);
        ani.SetFloat("Vertical", y);
        ani.SetFloat("Magnitude", z);

    }
}

