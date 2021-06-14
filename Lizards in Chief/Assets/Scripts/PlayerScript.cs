using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;

    public bool KeyboardIsP1;

    public bool CanJump;
    public bool CanHold;
    public bool Jumping;
    public bool JumpHold;
    public bool Falling;
    public bool Walking;
    public bool HoldingObj;
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
    private int fallSpeed = 3;

    [SerializeField]
    [Range(1, 10)]
    private int walkSpeed = 5;
    [SerializeField]


    public float xVelocity;
    public float yVelocity;
    public float horLAxis;
    [SerializeField]
    public float jumpVelocity;
    [SerializeField]
    public float fallVelocity;
    [SerializeField]
    public float walkVelocity;

    [SerializeField]
    GameObject hands;
    [SerializeField]
    GameObject objInHands;

    ProtagAnimator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<ProtagAnimator>();
        objInHands = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Movement
        if (IsGrounded())
        {
            if (!JumpHold)
            {
                CanJump = true;
            }
            Falling = false;
            Jumping = false;
            anim.Grounded();
        }

        horLAxis = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horLAxis) > .25f)
        {
            Walk(horLAxis);
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
            JumpHold = true;
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
            JumpHold = false;
        }

        yVelocity = rb.velocity.y;

        if (yVelocity > .1f)
        {
            Jumping = true;
            anim.Jumping();
            anim.Falling();
            if (JumpHold)
            {
                AddHeightToJump();
            }
        }
        else if (yVelocity < -.1f)
        {
            Jumping = false;
            anim.Jumping();
            CanJump = false;
            Falling = true;
            anim.Falling();
        }
        else
        {
            Falling = false;
            anim.Falling();
        }
        if (Falling)
        {
            fallVelocity = yVelocity;
        }

        if (!JumpHold)
        {
            ForceFall();
        }
        #endregion

        #region Inventory
        if (Input.GetButton("Grab"))
        {
            GrabObject();
        }
        else
        {
            DropObject();
        }
        anim.Holding();
        #endregion

        #region Visuals
        PutObjectInHands();
        #endregion
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

    void AddHeightToJump()
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

    void ForceFall()
    {
        rb.AddForce(Vector2.down * 5 * fallSpeed);
    }

    void GrabObject()
    {
        RaycastHit2D hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.4f), Vector2.right * Direction, Color.yellow);

        if (objInHands != null)
        {
            return;
        }
        // Does the ray intersect any objects
        if (hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.4f), Vector2.right * Direction, 1, layerMask))
        {
            if (hit.transform.CompareTag("Object"))
            {
                objInHands = hit.transform.gameObject;
                objInHands.GetComponent<Rigidbody2D>().simulated = false;
                objInHands.layer = 8;
                objInHands.transform.eulerAngles = Vector2.zero;
                objInHands.transform.parent = hands.transform;
                objInHands.transform.localPosition = Vector2.zero;
                PutObjectInHands();
                HoldingObj = true;
            }
        }
        else
        {
        }
    }

    /// <summary>
    /// Call each time an object is picked up, and each time the player changes direction
    /// </summary>
    void PutObjectInHands()
    {
        if (objInHands != null)
        {
            Vector2 newPos = hands.transform.localPosition;
            newPos.x = .4f * Direction;
            hands.transform.localPosition = newPos;
            Vector3 newRot = hands.transform.rotation.eulerAngles;
            if (Direction == 1)
            {
                newRot.y = 0;
            }
            if (Direction == -1)
            {
                newRot.y = 180;
            }
            hands.transform.eulerAngles = newRot;
        }
    }

    void DropObject()
    {
        if (objInHands != null)
        {
            objInHands.GetComponent<Rigidbody2D>().simulated = true;
            objInHands.layer = 0;
            objInHands.transform.parent = null;
            objInHands = null;
            HoldingObj = false;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        // Does the ray intersect any objects
        if (hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, layerMask))
        {
            if (hit.transform.CompareTag("Floor"))
            {
                Debug.DrawRay(transform.position, Vector2.down * 1.1f, Color.yellow);
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
