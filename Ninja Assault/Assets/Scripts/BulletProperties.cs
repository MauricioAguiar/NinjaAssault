using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletProperties :MonoBehaviour {

    private SpriteRenderer sprite;

   // private Color originalColor;

    public Color colorOnHit;

    public float timeToDestroy;

    public bool canColor, destroyOnHit;

    private bool firstTouch;

    // Use this for initialization
    private void Start() {
        firstTouch = true;
        sprite = gameObject.GetComponent<SpriteRenderer>();
      //  originalColor = sprite.color;
        Physics2D.IgnoreLayerCollision(13, 9, false);
        Physics2D.IgnoreLayerCollision(13, 10, true);

    }



    public void SetColor() {
        if (canColor)
            sprite.color = colorOnHit;
    }

    private void DestroyOnHit() {
        if (destroyOnHit)
            Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (firstTouch && collision.gameObject.tag != "Default") {
            SetColor();
            DestroyOnHit();
            firstTouch.Equals(false);
            Physics2D.IgnoreLayerCollision(13, 9, true);
            Physics2D.IgnoreLayerCollision(13, 10, true);
        }
    }
}
