using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    //bullet will destroy when collided with wall
    void OnCollisionExit(Collision collisionInfo){
        if (collisionInfo.transform.CompareTag("Walls")){
            Destroy(gameObject);
        }
    }
}