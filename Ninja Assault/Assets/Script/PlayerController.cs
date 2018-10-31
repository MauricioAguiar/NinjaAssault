using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Animator animator;

    public float xtraSpeed = 4f;

    public GameObject crossHair, weapon;

    public Rigidbody2D playerRigidBody;


    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start(){

    }

    // Update is called once per frame
    void FixedUpdate(){


        AimAndShoot();

        ApplyForce();
        

    }

    void ApplyForce(){

        float x = Input.GetAxis("Horizontal");

        float y = Input.GetAxis("Vertical");

        Vector2 vec = new Vector2(x, y);

        float velo = (1+ xtraSpeed/10) * 300;

        playerRigidBody.velocity = (vec * Time.deltaTime * velo);
        MovAnimator(vec);
    }


    void MovAnimator(Vector3 movement){

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
    }

    void AimAndShoot(){

        Vector3 aim = new Vector3(Input.GetAxis("HorizontalShoot"), Input.GetAxis("VerticalShoot"), 0.0f);
      
        Vector3 shootingDirection = new Vector3(Input.GetAxis("HorizontalShoot"), Input.GetAxis("VerticalShoot"), 0.0f);       

        if (aim.magnitude > 0.0f){
            aim.Normalize();

            crossHair.transform.localPosition = aim*0.4f;
            crossHair.SetActive(true);

            shootingDirection.Normalize();
            if (Input.GetButtonDown("Fire1")){

                GameObject bullet = Instantiate(weapon, transform.position, Quaternion.identity);

                Vector3 angle = aim;

                bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection*10;
                bullet.transform.Rotate(0.0f, 0.0f, Mathf.Atan2 (shootingDirection.y,shootingDirection.x)* Mathf.Rad2Deg);
                Destroy(bullet, 2.0f);
            }
        }
        else{
            crossHair.SetActive(false);
        }
    }
}
