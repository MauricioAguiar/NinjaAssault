using UnityEngine;
using System.Collections;

public class PlayerController :MonoBehaviour {

   

    public static PlayerController instance;

    public float dashMaxDistance, dashCooldown, dashAjustment;

    public Animator animator, effectAnimator;

    public GameObject crossHair, weapon, ghostEffect;

    public SpriteRenderer playerSprite;

    public Rigidbody2D playerRigidBody;

    public LayerMask obstacleLayer;

    public bool isColliding, IsMoving;

    public Color playerColor;

    private Vector2 vec;
    [SerializeField]

    private GameObject effectPos;

    private Vector2 dashDirection;

    private float velo, dashInTime, ExtraSpeed;

    public bool WillCollide() {
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

    private void Awake() {
        dashAjustment = 30;
        dashCooldown = 0.7f;
        dashMaxDistance = 2f;
        vec = new Vector2(0, 0);
        obstacleLayer = LayerMask.GetMask("Walls");
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();

        ToInstance();

        Dash();
    }
    // Use this for initialization
    void Start() {
        //effectPos = GameObject.FindGameObjectWithTag("PlayerEffect");
        //effectAnimator = effectPos.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() {

        AimAndShoot();

        ApplyForce();

        Dash();
    }

    // Apply Force on especific direction
    void ApplyForce() {

        float x = Input.GetAxis("Horizontal");

        float y = Input.GetAxis("Vertical");

        vec = new Vector2(x, y);

        playerRigidBody.velocity = (vec * Time.deltaTime * PlayerStats.instance.playerSpeed);

        if (vec.magnitude != 0) {

            dashDirection = vec.normalized;

            IsMoving = true;

        } else {
            IsMoving = false;
        }
        MovAnimator(vec);

    }

    // Dash function using Rigidbody to add velocity to the target
    void Dash() {

        float futureX = dashDirection.x * dashMaxDistance;
        float futureY = dashDirection.y * dashMaxDistance;

        Vector2 shiftShity = new Vector2(futureX + transform.position.x, futureY + transform.position.y);

        Debug.DrawLine(transform.position, shiftShity, Color.cyan);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDirection, dashMaxDistance, obstacleLayer);

        float dashPrecise = 1;

        if (Input.GetButtonDown("Dash")) {

            if (hit) {
                Debug.Log("Bateu na parede");
                dashPrecise = hit.fraction;
                Instantiate(ghostEffect, transform.position, transform.rotation);
            }

            if (dashInTime <= 0) {
                Debug.Log("Dash Livre");
                //effectPos.transform.position = playerSprite.transform.position;

                playerRigidBody.velocity = (dashDirection * (dashPrecise * dashMaxDistance* dashAjustment));

                dashInTime = dashCooldown;
                playerSprite.color = new Color(1f, 1f, 1f, 0.8f);
                //effectAnimator.SetBool("isDashing", true);
                Physics2D.IgnoreLayerCollision(13, 9, true);
                Instantiate(ghostEffect, transform.position, transform.rotation);
                //StartCoroutine(InvisibleEffect(0.6f));
            }

        } else {
            if (dashInTime < 0) {
                dashInTime = 0;

            }
            dashInTime -= Time.deltaTime;
        }
    }

    IEnumerator InvisibleEffect(float timeDuration) {

        yield return new WaitForSeconds(timeDuration);
        effectAnimator.SetBool("isDashing", false);
        playerSprite.color = new Color(1f, 1f, 1f, 1f);
        Physics2D.IgnoreLayerCollision(13, 9, false);
    }

    // Update the animation on Player's atual direction
    void MovAnimator(Vector3 movement) {

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
    }

    // Responsible for control the Player's trow the current weapon and the aim
    void AimAndShoot() {

        Vector3 aim = new Vector3(Input.GetAxis("HorizontalShoot"), Input.GetAxis("VerticalShoot"), 0.0f);

        Vector3 shootingDirection = new Vector3(Input.GetAxis("HorizontalShoot"), Input.GetAxis("VerticalShoot"), 0.0f);

        if (aim.magnitude > 0.0f) {
            aim.Normalize();

            crossHair.transform.localPosition = aim;
            crossHair.SetActive(true);

            shootingDirection.Normalize();
            if (Input.GetButtonDown("Fire1")) {

                GameObject bullet = Instantiate(weapon, transform.position, Quaternion.identity);

                bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * 500;
                bullet.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);

                Destroy(bullet, 5);
            }
        } else {
            crossHair.SetActive(false);
        }
    }


}
