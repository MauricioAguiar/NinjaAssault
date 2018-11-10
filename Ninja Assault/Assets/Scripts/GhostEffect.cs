using System.Collections;
using UnityEngine;

public class GhostEffect : MonoBehaviour {


    public SpriteRenderer sprite;

    float timer = 0.5f;

    public Color ghostColor;

    private void Start(){

        sprite = GetComponent<SpriteRenderer>();

        transform.position = PlayerController.instance.transform.position;

        transform.localScale = PlayerController.instance.transform.localScale;

        sprite.sprite = PlayerController.instance.playerSprite.sprite;
        sprite.color = ghostColor;

    }

    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;

        if (timer <= 0)
            Destroy(gameObject);

	}

}
