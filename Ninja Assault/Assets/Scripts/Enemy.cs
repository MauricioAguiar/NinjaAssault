using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy :MonoBehaviour {

    public float speed, timeFollowing, rangeDetection, seekNewPos, replayCD;

    public Rigidbody2D rB;

    public Animator ani;

    public AudioClip confused1, confused2, confused3, following1, following2, following3, nhacNhac;

    public LayerMask obstacleLayer, obLayer;

    public Vector2 atualPosition, prevPosition, dir;

    private int musicPriority;

    private float followTime, replaySoundCD, replaySoundCDN;

    private Transform target;

    private GameAudio gameAudio;

    private void Start() {

        gameAudio = GameAudio.instance;

        GameManager.instance.AddEnemy();
        Debug.Log("Enemies: " + GameManager.instance.GetEnemyQuantity());

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed = 2;
        ani.GetComponent<Animator>();
        rB.GetComponent<Rigidbody2D>();
        obstacleLayer = LayerMask.GetMask("Walls");
        obLayer = LayerMask.GetMask("Player");
        timeFollowing = 2;
        seekNewPos = timeFollowing;
        followTime = timeFollowing;
        rangeDetection = 7;
        dir = NewDirection();
        replayCD = 3;
        replaySoundCD = 0;
        replaySoundCDN = 0;
    }

    // Update is called once per frame
    void FixedUpdate() {

        ExplodeEnemy();
        MovAnimation();
    }

    void MovAnimation() {

        Vector2 direction = target.position - transform.position;

        direction.Normalize();

        atualPosition = transform.position;

        RaycastHit2D hitPlayer = Physics2D.Raycast(transform.position, direction, rangeDetection, obLayer);

        if (Vector2.Distance(transform.position, target.position) < rangeDetection && hitPlayer) {

            RaycastHit2D hitWall = Physics2D.Raycast(transform.position, direction, rangeDetection, obstacleLayer);

            if (hitWall.fraction <= 0) {
                followTime = timeFollowing;
                SetPositions(direction);
                ani.SetBool("Confused", false);
                PlayFollowing();

            } else if (IsPlayerBeforeWall(hitPlayer, hitWall)) {
                followTime = timeFollowing;
                SetPositions(direction);
                ani.SetBool("Confused", false);
                PlayFollowing();

            } else {
                FollowAfterConfused(direction);
            }
        } else {
            SeeKPosition();
            SetPositions(dir);
        }

        float t, u;

        t = direction.x * rangeDetection;
        u = direction.y * rangeDetection;

        Vector2 aux = new Vector2(t + transform.position.x, u + transform.position.y);

        Debug.DrawLine(transform.position, aux, Color.yellow);

        prevPosition = atualPosition;
    }

    void FollowAfterConfused(Vector2 direction) {

        followTime -= Time.deltaTime;
        if (followTime >= 0)
            SetPositions(direction);
        else {
            rB.velocity = new Vector2(0, 0);
            followTime = 0;
            SeeKPosition();
            SetPositions(dir);
        }
    }

    void SetPositions(Vector2 direction) {

        transform.position = atualPosition;

        rB.velocity = direction * speed * 50 * Time.deltaTime;

        float x = 0;
        float y = 0;
        float z = 0;

        x = atualPosition.x - prevPosition.x;
        y = atualPosition.y - prevPosition.y;
        z = atualPosition.magnitude;
        ani.SetFloat("Horizontal", x);
        ani.SetFloat("Vertical", y);
        ani.SetFloat("Magnitude", z);
    }

    bool IsPlayerBeforeWall(RaycastHit2D player, RaycastHit2D wall) {

        if (player.fraction < wall.fraction)
            return true;
        else
            return false;
    }

    Vector2 NewDirection() {
        Vector2 directionVec = UnityEngine.Random.insideUnitCircle.normalized;
        return directionVec;
    }

    private void SeeKPosition() {
        seekNewPos -= Time.deltaTime;
        if (seekNewPos < 3 && seekNewPos >= 0) {
            dir = new Vector2(0, 0);
            ani.SetBool("Confused", true);
        } else if (seekNewPos < 0) {
            dir = NewDirection();
            seekNewPos = 4;
        }
    }

    private void ExplodeEnemy() {
        if (Input.GetMouseButton(1))
            Destroy(gameObject);
    }


    public void PlayFollowing() {
        replaySoundCD -= Time.deltaTime;

        if (replaySoundCD <= 0) {
            musicPriority = 3;
            PlayOneOfThree(following1, following2, following3);
            replaySoundCD = replayCD;
        }

    }


    public void PlayOneOfThree(AudioClip a1, AudioClip a2, AudioClip a3) {
        int toPlayRandom = UnityEngine.Random.Range(0, 3);
        if (toPlayRandom == 0)
            gameAudio.PlaySoundEnemy(a1, musicPriority);
        else if (toPlayRandom == 1)
            gameAudio.PlaySoundEnemy(a2, musicPriority);
        else if (toPlayRandom == 2)
            gameAudio.PlaySoundEnemy(a3, musicPriority);
    }

    public void PlayCounfused() {
        musicPriority = 4;
        PlayOneOfThree(confused1, confused2, confused3);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            PlayNhac();
        }
    }

    private void PlayNhac() {

        replaySoundCDN--;
        if (replaySoundCDN <= 0) {
            gameAudio.PlaySoundEnemy(nhacNhac, 2);
            replaySoundCDN = replayCD;
        }
    }

    private void OnDestroy() {
        GameManager.instance.LessOneEnemy();
    }
}
