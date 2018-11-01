using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float dashMaxDistance;

    public float dashSpeed;

    public float dashInTime;

    public float dashCooldown;

    private Collider2D coll;

    public Vector2 vec = new Vector2(0,0);

    public static PlayerController instance;

    [SerializeField]
    GameObject ghostEffect;

    public Animator animator;

    public float xtraSpeed = 4f;

    public GameObject crossHair, weapon;

    public SpriteRenderer playerSprite;

    public Rigidbody2D playerRigidBody;

    public LayerMask ObstacleLayer;

    float velo;

    public bool isColliding;

    public bool IsMoving;

    public bool WillCollide(){
        return true;
    }

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

    private void Awake(){

        dashCooldown = 0;
        dashMaxDistance = 150;

        ObstacleLayer = LayerMask.GetMask("Walls");

        velo = (1 + xtraSpeed / 10) * 300;

        ToInstance();

        playerRigidBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();

        Dash();
    }
    
    // Use this for initialization
    void Start(){
        
    }

   
    // Update is called once per frame
    void FixedUpdate(){

        AimAndShoot();

        ApplyForce();

        Dash();
        

    }
    
    // Apply Force on especific direction
    void ApplyForce(){
        Instantiate(ghostEffect, transform.position, transform.rotation);
        float x = Input.GetAxis("Horizontal");

        float y = Input.GetAxis("Vertical");

        vec = new Vector2(x, y);

        playerRigidBody.velocity = (vec * Time.deltaTime * velo);

        if (vec.magnitude> 0) {

            IsMoving = true;

        }else{
            IsMoving = false;
        }
        MovAnimator(vec);

    }

    void Dash()
    {

        Debug.DrawLine(transform.position, transform.position.normalized * 3, Color.blue);

        if (Input.GetButtonDown("Dash"))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position,vec.normalized*dashMaxDistance, dashMaxDistance, ObstacleLayer);

            float dashPrecise = dashMaxDistance;

            Vector2 dashPointer = vec;            

            if (hit)
            {
                dashPrecise -= hit.fraction; 
            }

            if (dashInTime <= 0){

                Debug.Log("Dashing");

                playerRigidBody.AddForce(vec.normalized * dashPrecise, ForceMode2D.Impulse);
                Instantiate(ghostEffect, transform.position, transform.rotation);

                dashInTime = dashCooldown;
            }

        }else{

            dashInTime -= Time.deltaTime;
            Debug.Log("NotDashing");
        }
    }
   
    // Update the animation on Player's atual direction
    void MovAnimator(Vector3 movement){

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
    }
   
    // Responsible for control the Player's trow the current weapon and the aim
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
