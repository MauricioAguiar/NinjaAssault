using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float dashMaxDistance;

    private Vector2 dashDirection;

    private float dashInTime;

    public float dashCooldown;

    public float ExtraSpeed = 4f;

    private Collider2D coll;

    private Vector2 vec = new Vector2(0,0);

    public static PlayerController instance;

    [SerializeField]
    GameObject ghostEffect;

    public Animator animator;


    public GameObject crossHair, weapon;

    public SpriteRenderer playerSprite;

    public Rigidbody2D playerRigidBody;

    public LayerMask obstacleLayer;

    float velo;

    public bool isColliding;

    public bool IsMoving;

    public bool WillCollide(){
        return true;
    }

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

    private void Awake(){

        dashCooldown = 0.5f;
        dashMaxDistance = 4;

        obstacleLayer = LayerMask.GetMask("Walls");

        velo = (1 + ExtraSpeed / 10) * 300;

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
        
        float x = Input.GetAxis("Horizontal");

        float y = Input.GetAxis("Vertical");

        vec = new Vector2(x, y);

        playerRigidBody.velocity = (vec * Time.deltaTime * velo);

        if (vec.magnitude !=0) {

            dashDirection = vec.normalized;

            IsMoving = true;

        }else{
            IsMoving = false;
        }
        MovAnimator(vec);

    }

    // Dash function using Rigidbody to add velocity to the target
    void Dash(){

        // Vector2 shiftShity = new Vector2(transform.position.x + vec.normalized.x * 4, transform.position.y + vec.normalized.y * 4)
        float futureX  = dashDirection.x * dashMaxDistance;
        float futureY = dashDirection.y * dashMaxDistance;

        Vector2 shiftShity = new Vector2(futureX+transform.position.x, futureY+transform.position.y);


        Debug.DrawLine(transform.position, shiftShity, Color.yellow);

        RaycastHit2D hit = Physics2D.Raycast(transform.position,dashDirection, dashMaxDistance, obstacleLayer);
        float dashPrecise = 1;

        if (Input.GetButtonDown("Dash")){
        
            if (hit){

                dashPrecise = hit.fraction;
            }

            if (dashInTime <= 0){

                playerRigidBody.velocity  = (dashDirection *(dashPrecise * dashMaxDistance* 50));
                Instantiate(ghostEffect, transform.position, transform.rotation);

                dashInTime = dashCooldown;
            }

        }else{
            if (dashInTime < 0){ 
                dashInTime = 0;
            }
            dashInTime -= Time.deltaTime;

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
