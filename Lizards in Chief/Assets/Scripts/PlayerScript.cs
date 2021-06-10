using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;

    public bool CanJump;
    public bool CanHold;
    public bool Jumping;
    public bool Holding;
    public bool Falling;
    public bool Walking;
    public int Direction;

    [SerializeField]
    [Range(1, 10)]
    private int jumpSpeed = 7;
    [SerializeField]
    [Range(1, 10)]
    private int jumpHeight = 6;
    [SerializeField]
    [Range(1, 10)]
    private int extraHeight = 6;
    [SerializeField]
    [Range(1, 10)]
    private int floatyFall = 3;

    [SerializeField]
    [Range(1, 10)]
    private int walkSpeed = 5;
    [SerializeField]


    public float xVelocity;
    public float yVelocity;
    [SerializeField]
    public float jumpVelocity;
    [SerializeField]
    public float fallVelocity;
    [SerializeField]
    public float walkVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsGrounded())
        {
            if (!Holding)
            {
                CanJump = true;
            }
            Falling = false;
        }

        float horAxis = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horAxis) > .25f)
        {
            Walk(horAxis);
            Walking = true;
        }
        else
        {
            Walking = false;
        }

        xVelocity = rb.velocity.x;

        if (Input.GetButton("Jump"))
        {
            if (CanJump)
            {
                Jump();
                CanJump = false;
            }
            Holding = true;
            if (Falling)
            {
                FloatFall();
            }
            if (yVelocity > jumpVelocity)
            {
                jumpVelocity = yVelocity;
            }
        }
        else
        {
            Holding = false;
        }

        yVelocity = rb.velocity.y;

        if (yVelocity > 0)
        {
            Jumping = true;
            if (Holding)
            {
                AddHeight();
            }
        }
        else if (yVelocity < 0)
        {
            Jumping = false;
            CanJump = false;
            Falling = true;
            if (Holding)
            {

            }
        }
        if (Falling)
        {
            fallVelocity = yVelocity;
        }
    }

    void Walk(float dir)
    {
        if (dir > 0)
        {
            Direction = 1;
        }
        else if (dir < 0)
        {
            Direction = -1;
        }

        rb.velocity = new Vector2(dir * 1 * walkSpeed, rb.velocity.y);
        walkVelocity = xVelocity;
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    void AddHeight()
    {
        if (rb.velocity.y < jumpHeight)
        {
            rb.AddForce(Vector2.up * jumpSpeed * (extraHeight / 3));
        }
        else
        {
            CanHold = false;
        }
    }

    void FloatFall()
    {
        rb.AddForce(Vector2.up * 2 * floatyFall);
        string[] names = Input.GetJoystickNames();
    }

    bool IsGrounded()
    {
        RaycastHit2D hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        // Does the ray intersect any objects
        if (hit = Physics2D.Raycast(transform.position, Vector2.down, 1.05f, layerMask))
        {
            if (hit.transform.CompareTag("Floor"))
            {
                Debug.DrawRay(transform.position, Vector2.down * 1.05f, Color.yellow);
                return true;
            }
        }
        else
        {
            return false;
        }
        return false;
    }
}
