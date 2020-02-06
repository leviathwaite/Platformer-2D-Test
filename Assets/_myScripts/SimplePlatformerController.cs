using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Best platformer I have

public class SimplePlatformerController : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    private Vector2 velocity;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpStrength = 350f;
    public float extraGravity = 10f;
    public float minJumpMaxVelocity = 150f;
    // use over rigidbody linear drag
    public float drag;
    public Transform groundCheck;


    public bool grounded = false;
    public bool jumping = false;
    private Animator anim;
    private Rigidbody2D rb2d;


    // Use this for initialization
    void Awake () 
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update () 
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        Debug.DrawLine(transform.position, groundCheck.position);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("xSpeed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        
        // drag when grounded
        if(grounded){
            velocity.x -= drag;
        }else{
            // fall faster after jump
			rb2d.AddForce(-Vector2.up * extraGravity);
        }

        

        if (h > 0 && !facingRight)
            Flip ();
        else if (h < 0 && facingRight)
            Flip ();

        Jump();

    }

    void Jump(){
        if (jump && !jumping)
        {
            Debug.Log("trying to jump");
            // Instantly push the avatar upward.
            rb2d.AddForce(Vector2.up * jumpStrength * Time.deltaTime, ForceMode2D.Impulse);
            jumping = true;
        }
        else if (jumping)
        {
            if (!Input.GetButton("Jump"))
            {
            // Cancel the jump early.
            var v = rb2d.velocity;
            v.y = Mathf.Min(v.y, minJumpMaxVelocity * Time.deltaTime);
            rb2d.velocity = v;
            jumping = false;
            }
            else if (rb2d.velocity.y <= minJumpMaxVelocity * Time.deltaTime)
            {
            // The jump has now progressed beyond the point where it can be canceled early.
            jumping = false;
            }
        }
        jump = false;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}