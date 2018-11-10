using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour {

    public static GameAudio instance;

    public AudioSource aS;

    private int audioPriority;

    public float delayPointer;

    private float delayEnemy;

    void ToInstance() {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
    }
    private void Awake() {
        ToInstance();
        delayPointer = 3;
    }

    // Update is called once per frame
    void FixedUpdate () {
        delayEnemy -= Time.deltaTime;
    }

    public void PlaySoundEnemy(AudioClip clip, int priority) {
        if (priority < audioPriority) {
            aS.PlayOneShot(clip);
           

        } else if (!aS.isPlaying && delayEnemy <= 0) {
            aS.PlayOneShot(clip);
            delayEnemy = delayPointer;
        }
        audioPriority = priority;
    }
}
