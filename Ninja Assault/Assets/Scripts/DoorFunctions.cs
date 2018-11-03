using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFunctions :MonoBehaviour {

    public LayerMask obstacleLayer;

    public Animator animator;

    // Use this for initialization
    void Start() {

        obstacleLayer = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update() {
        checkDoorCollisor();

    }

    void changeState() {

    }

    bool checkDoorCollisor() {

        float x = 4;
        float y;

        Vector2 vecX = new Vector2(transform.position.x, transform.position.y - 1);

        Vector2 vec1X = new Vector2(0, 4);

        Vector2 vec2X = new Vector2(transform.position.x, transform.position.y + 1);

        RaycastHit2D hitX = Physics2D.Raycast(vecX, vec1X, x, obstacleLayer);

        Debug.DrawLine(vecX, vec2X, Color.blue);

        Vector2 vecY = new Vector2(transform.position.x - 1, transform.position.y);

        Vector2 vec1Y = new Vector2(4, 0);

        Vector2 vec2Y = new Vector2(transform.position.x + 1, transform.position.y);

        RaycastHit2D hity = Physics2D.Raycast(vecY, vec1Y, x, obstacleLayer);

        Debug.DrawLine(vecY, vec2Y, Color.green);

        if (hitX || hity) {
            animator.SetBool("hasTryOpen", true);
            
            return true;
        } else {
            animator.SetBool("hasTryOpen", false);
            return false;
        }

    }
}
