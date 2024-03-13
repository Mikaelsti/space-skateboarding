using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private Transform player;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    public GameObject Board;

    float isJumpingCooldown;
    private RaycastHit2D Hit2D;

    public bool isGrounded;
    public bool isLeaning;
    public static bool isFloored;
    //bool isJumping;
    public bool isJumping;

    public static bool isFlying = false;

    public float jumpForce;
    private float rotation;
    public float rotatespeed = 200f;

    float flipCooldown;
    public static int flips = 0;
    // public TMPro.TMP_Text flipText;
    public GameObject textPrefab;

    private float flipTextHideTimer;

    public static float accSpeed;
    //public static int accLevel = 1;
    public float decreaseForce = 1.2f;
    public Vector2 increaseForce = new Vector2(1.4f, 0f);

    Vector2 movement;
    //public static int maxSpeedLevel = 1;
    public static float maxSpeed;

    public bool isRotatingLeft = false;
    public bool isRotatingRight = false;
    bool isAccelerating = false;
    bool isCollidingWithAirObstacle = false;

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isFlying = false;
        flips = 0;
    }

    void Update()
    {
        rotation = Input.GetAxis("Horizontal") * -rotatespeed * Time.deltaTime;
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isJumpingCooldown -= Time.deltaTime;
        flipCooldown -= Time.deltaTime;
        flipTextHideTimer -= Time.deltaTime;

        movement.x = Input.GetAxisRaw("Horizontal");

        rb.AddForce(new Vector2(0, 0));

        //Debug.Log("nopeus: " + rb.velocity.x);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(transform.right * 1.1f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(transform.right * -1.1f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (isRotatingLeft && isGrounded == false)
        {
           // animator.Play("spaceman_backwards_leaning_clip1");
            transform.Rotate(0f, 0f, 200f * Time.deltaTime);
            Debug.Log("Rot Left");
        }

        if (isRotatingRight && isGrounded == false)
        {
           // animator.Play("spaceman_forward_leaning_v1");
            transform.Rotate(0f, 0f, -200f * Time.deltaTime);
            Debug.Log("Rot Right");
        }

        if (isAccelerating == true && isGrounded == true)
        {
            rb.AddForce(transform.right * (accSpeed) * Time.deltaTime);
            Debug.Log("Accelerate");
        }

        if (isGrounded == false)
        {
            rb.freezeRotation = true;
        }
        else
        {
            rb.freezeRotation = false;
        }
        if (isFlying)
        {

        }else
        {
            CheckMaxSpeed();
        }
       

        CheckForBackflip();
        GroundCheckMethod();
    }

    public void Jump()
    {
        if (isGrounded)
        {
            animator.Play("spaceman_jump_v5 0");
            rb.AddForce(new Vector2(0f, jumpForce * 100));
            Debug.Log("Hyppy");
            isJumping = true;
            isJumpingCooldown = 1f;
        }
    }

    public void Accelerate(bool canAccelerate)
    {
        if (isGrounded == true)
        {
            isAccelerating = canAccelerate;
        }
    }

    public void RotationLeft(bool canRotateLeft)
    {
        if (isGrounded == false && isFlying == false)
        {
            isRotatingLeft = canRotateLeft;
        }
        else
        {
            isRotatingLeft = false;
        }
    }

    public void RotationRight(bool canRotateRight)
    {
        if (isGrounded == false && isFlying == false)
        {
            isRotatingRight = canRotateRight;
        }
        else
        {
            isRotatingRight = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GameController")
        {
            isFlying = true;
            isRotatingLeft = false;
            isRotatingRight = false;
            //laittaa hahmon suoraan pystyyn
            rb.transform.rotation = Quaternion.Euler(new Vector2(0f, 0f));
        }

        if (collision.gameObject.tag == "Ramp")
        {
            isGrounded = true;
        }

        if (collision.gameObject.tag == "Floor")
        {
            isFloored = true;
        }

        if (collision.gameObject.tag == "Coin")
        {
            textPrefab.GetComponent<TextMeshPro>().text = "+5";
            Instantiate(textPrefab, new Vector3(gameObject.transform.position.x + 20, player.transform.position.y, 0), Quaternion.identity);

            GameManager.score += 5;
        }
        //Päivittää joka frame eikä vaan kerran, jos vaikka disablois sen colliderin
        if (collision.gameObject.tag == "Air_Obstacle" && isCollidingWithAirObstacle == false)
        {
            rb.velocity = (transform.right * rb.velocity.x / decreaseForce);
            Debug.Log("asdOsuttiin ilmaesteeseen. Nopeus> " + rb.velocity.x);
            isCollidingWithAirObstacle = true;
        }

        if (collision.gameObject.tag == "Flight_Speedup")
        {
            rb.AddForce(transform.right * increaseForce, ForceMode2D.Impulse);
            Debug.Log("asdOtettiin ilmaspeeduppi. Nopeus> " + rb.velocity.x);
        }

        if (collision.gameObject.tag == "Ground_Obstacle")
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ramp")
        {
            isGrounded = false;
        }

        if (collision.gameObject.tag == "Floor")
        {
            isFloored = false;
        }

        if (collision.gameObject.tag == "Air_Obstacle")
        {
            isCollidingWithAirObstacle = false;
            Debug.Log("lähdimmä pois colliderista");
        }
    }

    private void CheckMaxSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
            Debug.Log("maxSpeed");
        }
    }

    private void CheckForBackflip()
    {
        string[] phrases = new string[] { "Nice!", "Cool!", "Wow!", "Amazing :o",};

        if (transform.rotation.eulerAngles.z < 185 && transform.rotation.eulerAngles.z > 175 && flipCooldown <= 0)
        {
            flips += 1;
            flipCooldown = 1;
            flipTextHideTimer = 1;
            Debug.Log(phrases);
            
            string randomPhrase = phrases[Random.Range(0, phrases.Length)];
            textPrefab.GetComponent<TextMeshPro>().text = randomPhrase;
            Instantiate(textPrefab,new Vector3(gameObject.transform.position.x+10, player.transform.position.y,0), Quaternion.identity);
           
        }

        //if (flipTextHideTimer > 0)
        //{
        //    flipText.enabled = true;
        //}
        //else
        //{
        //    flipText.enabled = false;
        //}
    }

    private void GroundCheckMethod()
    {
        //Hit2D = Physics2D.Raycast(groundCheck.position, -Vector2.up, 10f, groundLayer);

        if (/*Hit2D != false && isJumpingCooldown <= 0 && */isGrounded)
        {
            //Vector2 temp = player.position;
            //temp.y = Hit2D.point.y;
            //player.position = temp;
            //isJumping = false;
            if (transform.rotation.eulerAngles.z < 80 && transform.rotation.eulerAngles.z > 25)
            {
                transform.Rotate(0f, 0f, -200f * Time.deltaTime);
                Debug.Log("Leaning left");
            }
            if (transform.rotation.eulerAngles.z > 260 && transform.rotation.eulerAngles.z < 330)
            {
                transform.Rotate(0f, 0f, 200f * Time.deltaTime);
                Debug.Log("Leaning right");
            }
        }
    }
}
