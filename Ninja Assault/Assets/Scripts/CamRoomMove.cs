using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRoomMove :MonoBehaviour {

    public static CamRoomMove instance;

    Vector3 moveJump = Vector2.zero;

    //Instance method [Singleton]
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

    void Start() {

        ToInstance();
        SheetAssigner SA = FindObjectOfType<SheetAssigner>();
        Vector2 tempJump = SA.roomDimensions + SA.gutterSize;
        moveJump = new Vector3(tempJump.x, tempJump.y, 0); //distance b/w rooms: to be used for movement
    }

    public void MovCam(int x, int y) {
        Vector3 tempPos = transform.position;
        tempPos += Vector3.right * x * moveJump.x; //jump beetween rooms based on doors
        tempPos += Vector3.up * y * moveJump.y;
        transform.position = tempPos;
    }
}
