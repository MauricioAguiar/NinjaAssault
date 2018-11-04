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
    void Start() {

        alreadyPlayed = false;
        col = GetComponent<PolygonCollider2D>();
        aS = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update() {

        OnTriggerEnter2D(col);
        OnTriggerExit2D(col);
    }

    void PlayLocked() {
        if (alreadyPlayed.Equals(false)) {
            aS.PlayOneShot(lockedDoor, 0.5f);
            alreadyPlayed = true;
        }
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
