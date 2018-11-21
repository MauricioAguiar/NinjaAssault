using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    private Vector2 position;

    public float smoothTime;

    private GameObject player;

    private void Start() {

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void FixedUpdate() {

        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref position.x, smoothTime);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref position.y, smoothTime);


        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}