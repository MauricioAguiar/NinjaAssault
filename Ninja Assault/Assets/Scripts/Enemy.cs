using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy :MonoBehaviour {

    public float speed;

    private Transform target;

    public Rigidbody2D rb;

    public Animator ani;

    public string statusEnemy;

    public LayerMask obstacleLayer;

    public Vector2 atualPosition, prevPosition;

    // Use this for initialization
    void Awake() {

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = 2;
        ani.GetComponent<Animator>();
        rb.GetComponent<Rigidbody2D>();
        obstacleLayer = LayerMask.GetMask("Walls");
    }

    // Update is called once per frame
    void Update() {

        movAnimation();
    }



    void movAnimation() {



        atualPosition = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        Vector2 direction = target.position - transform.position;

        direction.Normalize();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 4, obstacleLayer);

        float x = 0;
        float y = 0;
        float z = 0;

        if (Vector2.Distance(atualPosition, target.position) < 4 && hit){

            transform.position = atualPosition;


            x = atualPosition.x - prevPosition.x;
            y = atualPosition.y - prevPosition.y;
            z = atualPosition.magnitude;
            ani.SetFloat("Horizontal", x);
            ani.SetFloat("Vertical", y);
            ani.SetFloat("Magnitude", z);
            ani.SetBool("Confused", false);
        } else {
            ani.SetBool("Confused", true);
        }

        float t, u;

        t = direction.x * 4;
        u = direction.y * 4;

        Vector2 aux = new Vector2(t + transform.position.x, u + transform.position.y);

        Debug.DrawLine(transform.position, aux, Color.yellow);

        prevPosition = atualPosition;
    }
}


