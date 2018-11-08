using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy :MonoBehaviour {

    public float speed, followTime, TimeFollowing, RangeDetection;

    private Transform target;

    public Rigidbody2D rb;

    public Animator ani;

    public string statusEnemy;

    public LayerMask obstacleLayer, obLayer;

    public Vector2 atualPosition, prevPosition;


    // Use this for initialization
    void Awake() {

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = 2;
        ani.GetComponent<Animator>();
        rb.GetComponent<Rigidbody2D>();
        obstacleLayer = LayerMask.GetMask("Walls");
        obLayer = LayerMask.GetMask("Player");
        TimeFollowing = 0.3f;
        followTime = TimeFollowing;
        RangeDetection = 4;
    }

    // Update is called once per frame
    void FixedUpdate() {

        movAnimation();
    }



    void movAnimation() {

        atualPosition = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        Vector2 direction = target.position - transform.position;

        direction.Normalize();

        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, direction, RangeDetection, obLayer);

        float x = 0;
        float y = 0;
        float z = 0;

        if (Vector2.Distance(atualPosition, target.position) < RangeDetection && hitPlayer) {

            RaycastHit2D hitWall = Physics2D.Raycast(transform.position, direction, RangeDetection, obstacleLayer);

            if (hitWall.fraction <= 0) {

                followTime = TimeFollowing;
                Debug.Log("O Caminho está livre |o/");

                SetPositions(x, y, z);

            }else if (IsPlayerBeforeWall(hitPlayer,hitWall)) {
                followTime = TimeFollowing;
                SetPositions(x, y, z);
                Debug.Log("O Caminho está livre, mas estou de encontro com a parede");

            } else {
                Debug.Log("O Caminho não está livre");

                Debug.Log("Este é o Fraction da Parede: " + hitWall.fraction);
                Debug.Log("Este é o Fraction do Player: " + hitPlayer.fraction);
                followAfterConfused(x, y, z);
            }
        } else {
        }

        float t, u;

        t = direction.x * RangeDetection;
        u = direction.y * RangeDetection;

        Vector2 aux = new Vector2(t + transform.position.x, u + transform.position.y);

        Debug.DrawLine(transform.position, aux, Color.yellow);

        prevPosition = atualPosition;
    }

    void followAfterConfused(float x, float y, float z) {
        followTime -= Time.deltaTime;

        if (followTime > 0)
            SetPositions(x, y, z);
        else
            ani.SetBool("Confused", true);
    }

    void SetPositions(float x, float y, float z) {

        transform.position = atualPosition;

        x = atualPosition.x - prevPosition.x;
        y = atualPosition.y - prevPosition.y;
        z = atualPosition.magnitude;
        ani.SetFloat("Horizontal", x);
        ani.SetFloat("Vertical", y);
        ani.SetFloat("Magnitude", z);
        ani.SetBool("Confused", false);
    }

    bool IsPlayerBeforeWall(RaycastHit2D player, RaycastHit2D wall) {

        if (player.fraction < wall.fraction)
            return true;
        else
            return false;
    }
}


