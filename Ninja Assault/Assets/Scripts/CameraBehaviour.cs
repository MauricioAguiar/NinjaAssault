using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    private Vector2 position;

    public float smoothTime;

    public float shakeMagnitude,shakeDuration, shakeAmountX, shakeAmountY;

    private GameObject player;

    public static CameraBehaviour instance;

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
        private void Start() {

        ToInstance();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void FixedUpdate() {

        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref position.x, smoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref position.y, smoothTime);


        transform.position = new Vector3(posX, posY, transform.position.z);


    }

    public void DoShake() {
        StopCoroutine(ShakeCoroutine());
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine() {
        Vector3 startPos = transform.localPosition;
        float endTime = Time.time + shakeDuration;
        float currentX = 0;

        while (Time.time < endTime) {
            Vector3 shakeAmount = new Vector3(
                Mathf.PerlinNoise(currentX, 0) - .5f,
                Mathf.PerlinNoise(currentX, 7) - .5f,
                transform.position.z
            );

            transform.localPosition = Vector3.Scale(new Vector3(shakeAmountX, shakeAmountY, transform.position.z), shakeAmount) + startPos;
            currentX += shakeMagnitude;
            yield return null;
        }
    }
}