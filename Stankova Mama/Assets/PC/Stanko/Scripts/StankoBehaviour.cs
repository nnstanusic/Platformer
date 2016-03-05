using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StankoBehaviour : MonoBehaviour
{

    #region Public Variables
    public float maxGroundSpeed = 5f;
    public float maxAerialSpeed = 3f;
    public float jumpForce = 700f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public GameObject bullet;
    #endregion

    #region Private Variables
    private float groundRadius = 0.5f;
    private Animator anim;
    private bool facingRight = true;
    private bool isGrounded = false;
    private const short maxBullets = 4;
    private int bulletCounter = 0;

    #endregion

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        jumpTrigger(KeyCode.Space);
        shootTrigger(KeyCode.J);
    }
  
    void FixedUpdate()
    {
        horizontalMovement();
        onGround();
        setAnimatorParameters();
    }

    void setAnimatorParameters()
    {
        anim.SetFloat("VerticalSpeed",
                      this.GetComponent<Rigidbody2D>().velocity.y);

        anim.SetBool("IsGround", isGrounded);
    }
    void onGround()
    {
        isGrounded = Physics2D.OverlapCircle(
                        groundCheck.position,
                        groundRadius,
                        whatIsGround);
    }
    void horizontalMovement()
    {
        float move = Input.GetAxis("Horizontal");
        adjustOrientation(move);
        anim.SetFloat("Speed", Mathf.Abs(move));

        if (isGrounded)
        {
            this.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(
                    move * maxGroundSpeed,
                    this.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            aerialHorizontalMovement(move);
        }
    }
    void aerialHorizontalMovement(float move)
    {
        if (Mathf.Abs(
                this.GetComponent<Rigidbody2D>().velocity.x) > maxAerialSpeed)
        {
            this.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(
                    move * maxGroundSpeed,
                    this.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(
                    move * maxAerialSpeed,
                    this.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
    void adjustOrientation(float move)
    {
        if (move > 0 && !facingRight)
        {
            flip();
        }
        else if (move < 0 && facingRight)
        {
            flip();
        }
    }
    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void jumpTrigger(KeyCode trigger)
    {
        if (isGrounded && Input.GetKeyDown(trigger))
        {
            anim.SetBool("IsGround", false);
            this.GetComponent<Rigidbody2D>().AddForce(
                                                new Vector2(0, jumpForce));
        }

    }
    void shootTrigger(KeyCode trigger)
    {
        if (GameObject.FindGameObjectsWithTag("Bullet").Length <
            maxBullets && Input.GetKeyDown(trigger))
        {
            anim.SetTrigger("Shoot");
            bullet.transform.localScale = 
                new Vector3(
                            Mathf.Abs(bullet.transform.localScale.x) * 
                            this.transform.localScale.x,
                            bullet.transform.localScale.y,
                            bullet.transform.localScale.z);

            Instantiate(bullet,
                        this.transform.position + 
                        new Vector3(0.9f * this.transform.localScale.x,
                            0.75f,
                            0),
                        Quaternion.identity);
        }
    }
}
