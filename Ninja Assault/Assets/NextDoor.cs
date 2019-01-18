using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDoor : MonoBehaviour {

    public enum Direction {Left,Right,Top,Bot};

    //public LayerMask obstacleLayer;

    public Direction doorDirection;

    public int nextDoorDistance;

    public Color color;

    // Use this for initialization
    void Start () {
	}

	// Update is called once per frame
	void Update () {

        Debug.DrawLine(transform.position, DirectionDoor(), color);
   
    }

    Vector2 DirectionDoor() {
        //Vector2 direction = new Vector2(0,0);
        //Vector2 origin = transform.position;

        CamRoomMove crm = CamRoomMove.instance;

        Vector2 vec = new Vector2(0, 0);
        Vector2 aux = transform.position;

        if (doorDirection == Direction.Left) { //Setting Values according to the character's door enter

            vec = new Vector2(aux.x - nextDoorDistance, aux.y);
            crm.MovCam(-1,0);

        } else if (doorDirection == Direction.Right) {
            vec = new Vector2(aux.x + nextDoorDistance, aux.y);
            crm.MovCam(1, 0);

        } else if (doorDirection == Direction.Top) {
            vec = new Vector2(aux.x, aux.y + nextDoorDistance);
            crm.MovCam(0, 1);

        } else if (doorDirection == Direction.Bot) {
            vec = new Vector2(aux.x, aux.y - nextDoorDistance);
            crm.MovCam(0, -1);

            //direction = new Vector2(0, -1);
            //origin = new Vector2(aux.x, aux.y - 30);
        }

        //RaycastHit2D hit = Physics2D.Raycast(origin, direction, nextDoorDistance, obstacleLayer);

        return vec;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player")) {
            PlayerController.instance.transform.position = DirectionDoor();
        }
    }


}
