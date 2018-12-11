using UnityEngine;
using System.Collections;

public class PersonController : MonoBehaviour
{
    public float speed = 200f;
    public float jumpForce = 500f;
    private float xSpeed;
    public float acceleration, deceleration, maxSpeed;
    public bool IsGround;
    public Transform groundCheck;
    public LayerMask groundLayers;
    private float groundCheckRadius = .2f;
    public bool jump;
    private bool facingRight, Idle;
    private Animator anim;

    private int currentScene;
    public bool IsDying = false;
    public bool Open;
    private float horizontalMotion = 0f;//verticalMotion=0f

    // Use this for initialization
    void Start()
    {
        maxSpeed = 4;
        xSpeed = 0;
        acceleration = 10;
        deceleration = 10;
        // rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //jump = false;
        facingRight = true;
        Idle = true;
        anim.SetBool("Idle", true);
    }

    // Update is called once per frame - used for button/key presses
    void Update()
    {
        horizontalMotion = Input.GetAxis("Horizontal");    //X  get the horizontal motion, if any
        //verticalMotion = Input.GetAxis("Vertical");        //X  get the vertical motion, if any
        if (Input.GetKeyDown(KeyCode.Space))
        {//GetButtonDown("Jump")
            jump = true;
        }
        if (IsDying == true)
            IsDying = false;
    }

    void FixedUpdate()
    {
        IsGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);
        anim.SetBool("IsGround", IsGround);
        if (jump == true && IsGround)   //space is pressed and the player isn't already jumping and the player is grounded
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 0);
            this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
        }
        else if (!IsGround)
        {
            jump = false;
        }
        jump = false;
        float moveX = Input.GetAxis("Horizontal");
        if ((Input.GetKey(KeyCode.A)) && (xSpeed > -maxSpeed))
            xSpeed = xSpeed - acceleration * Time.deltaTime;
        else if ((Input.GetKey(KeyCode.D)) && (xSpeed < maxSpeed))
            xSpeed = xSpeed + acceleration * Time.deltaTime;
        else
        {
            if (xSpeed > deceleration * Time.deltaTime)
                xSpeed = xSpeed - deceleration * Time.deltaTime;
            else if (xSpeed < -deceleration * Time.deltaTime)
                xSpeed = xSpeed + deceleration * Time.deltaTime;
            else
                xSpeed = 0;
        }
        Vector2 moving = new Vector2(xSpeed, GetComponent<Rigidbody2D>().velocity.y);
        if (moveX != 0f)
        {
            Idle = false;
        }
        else
        {
            Idle = true;
        }
        anim.SetBool("Idle", Idle);
        if ((moveX > 0.0f && !facingRight) || (moveX < 0.0f && facingRight))
        {
            Flip();
        }
        GetComponent<Rigidbody2D>().velocity = moving;
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 playerScale = this.transform.localScale;
        playerScale.x *= -1;
        this.transform.localScale = playerScale;
    }

    private void OnTriggerEnter2D(Collider2D collision) // Trigger function
    {
        if (collision.gameObject.tag == "coin")
        {
            Debug.Log("Player has collected the coin!");
            AudioManager.Instance.PlaySoundEffect(1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "key")
        {
            Debug.Log("Player has took the key!");
            AudioManager.Instance.PlaySoundEffect(2);
            KeyScript.open = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "bomb")
        {
            IsDying = true;
            Debug.Log("Player hit bomb");
            AudioManager.Instance.PlaySoundEffect(3);
            if (currentScene == 2)
                this.transform.position = new Vector3(-30.77f, 5.63f, 0);// need change based on player location
            else if (currentScene == 3)
                this.transform.position = new Vector3(-7.6f, -0.13f, 0f);// need change based on player location
            else if (currentScene == 4)
                this.transform.position = new Vector3(-7.6f, -0.13f, 0f);// need change based on player location
            TransitionManager.player_life--;
            Destroy(collision.gameObject);
        }
    }
}

/*using UnityEngine;
using System.Collections;

public class PersonController : MonoBehaviour {

    public float speed;
    public Rigidbody2D rb;
    public float jumpForce;
    public bool jump;
    private bool facingRight;
    private bool Idle;
    public Transform groundCheck;
    private Animator anim;
    public LayerMask groundLayers;
    public bool IsGround;

    private float horizontalMotion, verticalMotion;

    private float xSpeed;
    public float acceleration, deceleration, maxSpeed;
    private int currentScene;
    public bool IsDying = false;

    // Use this for initialization
    void Start () {
        maxSpeed = 4;
        xSpeed = 0;
        acceleration = 10;
        deceleration = 10;
        speed = 200f;
        rb = GetComponent<Rigidbody2D>();
        jumpForce = 20;
        jump = false;
        facingRight = true;
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame - used for button/key presses
    void Update () {
        horizontalMotion = Input.GetAxis("Horizontal");    //get the horizontal motion, if any
        verticalMotion = Input.GetAxis("Vertical");        //get the vertical motion, if any
        if (Input.GetKeyDown(KeyCode.Space)) {
            jump = true;
        }
        if (IsDying == true)
            IsDying = false;
    }

    void FixedUpdate()
    {
        IsGround = Physics2D.OverlapCircle(groundCheck.position, .05f, groundLayers);
        anim.SetBool("IsGround", IsGround);
        if (jump && IsGround)   //space is pressed and the player isn't already jumping and the player is grounded
        {
            rb.AddForce(new Vector2(0, jumpForce));    //add the force to have the player jump  
        } else if (!IsGround)
        {
            jump = false;
        }
      

        if (horizontalMotion > 0f || horizontalMotion < 0f)  //if the player is walking
        {
            anim.SetBool("Idle", false);       //control the animation - change the boolean in the animator
        }
        else
        {
            anim.SetBool("Idle", true);
        }
        //the following is for flipping the player sprite so it is always facing the right direction
        if (horizontalMotion < 0 && facingRight)
        {
            Flip();
        }
        else if (horizontalMotion > 0 && !facingRight)
        {
            Flip();
        }
        Vector3 motion = new Vector3(speed * horizontalMotion * Time.deltaTime,0, 0); //standard for vector motion (this is only for x axis)
        this.transform.position = this.transform.position + motion;
        
    }

    void Flip() // function that alters the direction the player is facing
    {
        facingRight = !facingRight;
        Vector3 playerScale = this.transform.localScale;
        playerScale.x *= -1;
        this.transform.localScale = playerScale;
    }

    private void OnTriggerEnter2D(Collider2D collision) // Trigger function
    {
        if (collision.gameObject.tag == "coin")
        {
            Debug.Log("Player has collected the coin!");
            AudioManager.Instance.PlaySoundEffect(1);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "key")
        {
            Debug.Log("Player has took the key!");
            AudioManager.Instance.PlaySoundEffect(2);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "boum")
        {
            IsDying = true;
            Debug.Log("Player hit bomb");
            AudioManager.Instance.PlaySoundEffect(3);
            if (currentScene == 2)
                this.transform.position = new Vector3(-6.2f, 0.5f, 0f);// need change based on player location
            else if (currentScene == 3)
                this.transform.position = new Vector3(-7.6f, -0.13f, 0f);// need change based on player location
            else if (currentScene == 4)
                this.transform.position = new Vector3(-7.6f, -0.13f, 0f);// need change based on player location
            // TransitionManager.player_life--;  
            Destroy(collision.gameObject);
        }

    }

}*/
