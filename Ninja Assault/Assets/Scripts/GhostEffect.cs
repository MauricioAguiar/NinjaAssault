using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour {


    public SpriteRenderer sprite;

    float timer = 0.5f;

    private void Start(){

        sprite = GetComponent<SpriteRenderer>();

        transform.position = PlayerController.instance.transform.position;

        transform.localScale = PlayerController.instance.transform.localScale;

        sprite.sprite = PlayerController.instance.playerSprite.sprite;
        sprite.color = new Vector4(0, 50, 50, 0.1f);
    }

    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;

        if (timer <= 0)
            Destroy(gameObject);

	}

}
