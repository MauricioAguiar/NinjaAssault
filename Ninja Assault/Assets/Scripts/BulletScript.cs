using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour{

    public LayerMask obstacleLayer;

    public Color c;

    private void Start() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Walls") {
            SpriteRenderer s = gameObject.GetComponent<SpriteRenderer>();
            Rigidbody2D r = gameObject.GetComponent<Rigidbody2D>();

            s.color = c;

            r.Sleep();

            Destroy(gameObject,3);
        }
    }
}