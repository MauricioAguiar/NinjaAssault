using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFunctions :MonoBehaviour {

    public AudioClip openDoor, closingDoor, lockedDoor;

    AudioSource aS;

    public PolygonCollider2D col;

    public Animator animator;

    bool alreadyPlayed;

    // Use this for initialization
    void Awake() {

        alreadyPlayed = false;
        col = GetComponent<PolygonCollider2D>();
        aS = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() {

        GetEnemyStatus();
    }

    void PlayLocked() {
        if (alreadyPlayed.Equals(false)) {
            aS.PlayOneShot(lockedDoor, 0.5f);
            alreadyPlayed = true;
        }
    }

    void GetEnemyStatus() {
        if (GameManager.instance.HasEnemyLeft())
            animator.SetBool("hasEnemy", true);
        else
            animator.SetBool("hasEnemy", false);
    }

    void PlayClosing() {
        aS.PlayOneShot(closingDoor, 0.5f);
    }

    void PlayOpening() {
        aS.PlayOneShot(openDoor, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player"){
            animator.SetBool("hasTryOpen", true);
            alreadyPlayed = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player")
            animator.SetBool("hasTryOpen", false);
    }
}
