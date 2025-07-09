using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    AudioSource audio;
    [SerializeField]
    AudioClip jump;
    [SerializeField]
    AudioClip land;
    [SerializeField]
    AudioClip walk1;
    [SerializeField]
    AudioClip walk2;

    InputManager input;

    float lastBlipX;
    bool rightStep = true;
    bool playStep = true;

    public bool Jumping;
    public bool Falling;
    public bool Walking;
    public bool HoldingObj;
    public bool GrabbingLedge;
    public int Direction;

    [SerializeField]
    public bool HasUnlockedFloatFall;
    [SerializeField]
    public bool HasUnlockedDashing;
    [SerializeField]
    public bool HasUnlockedLedgeGrabbing;
    [SerializeField]
    public bool HasUnlockedDoubleJump;

    private bool CanJump;
    private bool CanHold;
    private bool JumpHold;
    private bool Running;
    private bool Dashing;
    private bool CanDash;
    private bool PostDash;
    public bool LookBack;
    private bool LedgeJumping;
    private bool CanGrabOrDrop = true;
    private bool StoringItem;
    private bool EquippingItem;
    private int PrevDirection;

    private bool DoubleJump = false;
    private bool FallingDoubleJump = false;

    List<string> grabbableTags = new List<string>
    {
        "Object", "Weapon"
    };

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
    private int extraHeightSpeed = 1;
    [SerializeField]
    [Range(1, 10)]
    private int floatyFall = 3;
    [SerializeField]
    [Range(1, 15)]
    private int fallSpeed = 3;
    private int jumpCount = 0;

    [SerializeField]
    [Range(1, 10)]
    private int walkSpeed = 5;
    [SerializeField]
    [Range(1, 10)]
    private int dashSpeed = 5;
    [SerializeField]
    [Range(1, 10)]
    private int dashTime = 1;
    [SerializeField]
    [Range(1, 10)]
    private int PostDashTime = 10;
    public float dashWalkTransition = 0;


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
    public float rightJoystickHor;
    [SerializeField]
    public float rightJoystickVert;
    [SerializeField]
    public float rightJoystickMag;

    [SerializeField]
    GameObject hands;
    [SerializeField]
    GameObject objInHands;
    [SerializeField]
    Item itemInHands;
    [SerializeField]
    GunBehavior gunInHands;

    ProtagAnimator anim;
    PlayerInventory inventory;
    [SerializeField]
    GameMenuController gameMenu;

    //Gamepad gamepad;

    private float timer = 0.0f;
    private int globalSeconds = 0;
    public int frameTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<InputManager>();
        //gamepad = Gamepad.current;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<ProtagAnimator>();
        inventory = GetComponentInChildren<PlayerInventory>();
        objInHands = null;
        itemInHands = null;

        LoadAbilities();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DoMovement();

        #region Inventory

        if (input.GrabButtonPressed())
        {
            if (CanGrabOrDrop)
            {
                if (!HoldingObj)
                {
                    CanGrabOrDrop = !GrabObject();
                }
                else
                {
                    DropObject();
                    CanGrabOrDrop = false;
                }
            }
            inventory.selectedSlot = -1;
        }
        else
        {
            CanGrabOrDrop = true;
        }

        anim.Holding();

        rightJoystickHor = input.GetRightStickValues().x;
        rightJoystickVert = input.GetRightStickValues().y;

        if (inventory.ShowInventory(rightJoystickHor, rightJoystickVert))
        {
            if (objInHands != null)
                StoringItem = true;
            else
                EquippingItem = true;
        }
        else if (StoringItem)
        {
            if (inventory.ItemExistsInSlot(inventory.selectedSlot))
            {
                GameObject equippedItem = inventory.SwapItems(objInHands);
                if (equippedItem != null)
                {
                    equippedItem.transform.parent = hands.transform;
                    equippedItem.transform.localPosition = Vector3.zero;
                    objInHands = equippedItem;
                    itemInHands = objInHands.GetComponent<Item>();
                    HoldingObj = true;
                    EquippingItem = false;
                }
            }
            else
            {
                inventory.StoreItem(objInHands);
                Destroy(objInHands);
                HoldingObj = false;
            }
            StoringItem = false;
        }
        else if (EquippingItem)
        {
            GameObject item = inventory.EquipItem();
            if (item != null)
            {
                item.transform.parent = hands.transform;
                item.transform.localPosition = Vector3.zero;
                objInHands = item;
                itemInHands = objInHands.GetComponent<Item>();
                HoldingObj = true;
                EquippingItem = false;
            }
        }
        #endregion

        #region Weapons
        //Fire
        if (gunInHands)
        {
            if (input.FireButtonPressed() == 1)
            {
                gunInHands.Shoot(Direction);
            }
        }
        #endregion

        #region Visuals
        HoldObjectInHands();
        #endregion
    }

    private void Update()
    {
        #region Pause
        if (input.StartButtonPressed() && Time.timeScale == 1)
            gameMenu.Toggle(true);
        else if (input.StartButtonPressed() && Time.timeScale == 0)
            gameMenu.Toggle(false);
        #endregion
    }

    #region Abilities
    private void LoadAbilities()
    {
        HasUnlockedFloatFall = IntToBool(PlayerPrefs.GetInt("floatfall", 0));
        HasUnlockedDashing = IntToBool(PlayerPrefs.GetInt("dashing", 0));
        HasUnlockedLedgeGrabbing = IntToBool(PlayerPrefs.GetInt("ledgegrab", 0));
        HasUnlockedDoubleJump = IntToBool(PlayerPrefs.GetInt("doublejump", 0));
    }

    public GunBehavior GetGunInHands()
    {
        if (!gunInHands)
            return null;
        return gunInHands.GetComponent<GunBehavior>();
    }

    private bool IntToBool(int input)
    {
        if (input == 0)
            return false;
        else 
            return true;
    }
    #endregion
    void DoMovement()
    {
        if (IsGrounded())
        {
            jumpVelocity = 0;
            jumpCount = 0;
            if (!JumpHold)
            {
                CanJump = true;
            }
            if (Falling)
            {
                audio.PlayOneShot(land);
            }
            Falling = false;
            Jumping = false;
            DoubleJump = false;
            anim.Grounded();
        }

        horLAxis = input.GetLeftStickValues().x;

        if (GrabbingLedge)
        {
            Jumping = false;
            Falling = false;
        }
        else
        {
            DetermineDirection(horLAxis);
        }

        if (Mathf.Abs(horLAxis) > Mathf.Abs(.8f))
        {
            CanDash = true;
        }
        else
        {
            CanDash = false;
            Dashing = false;
        }
        if (Mathf.Abs(horLAxis) > .25f)
        {
            Walk(horLAxis);
            Walking = true;

            if (!Jumping && !Falling)
            {
                if (transform.position.x != lastBlipX)
                {
                    if (Direction == 1)
                    {
                        if (transform.position.x > lastBlipX + .5f)
                        {
                            if (rightStep)
                            {
                                audio.PlayOneShot(walk1);
                                rightStep = false;
                            }
                            else
                            {
                                audio.PlayOneShot(walk2);
                                rightStep = true;
                            }
                            lastBlipX = transform.position.x;
                        }
                    }
                    else if (Direction == -1)
                    {
                        if (transform.position.x < lastBlipX - .5f)
                        {
                            if (rightStep)
                            {
                                audio.PlayOneShot(walk1);
                                rightStep = false;
                            }
                            else
                            {
                                audio.PlayOneShot(walk2);
                                rightStep = true;
                            }
                            lastBlipX = transform.position.x;
                        }
                    }
                }
            }
        }
        else
        {
            Walking = false;
        }

        xVelocity = rb.velocity.x;

        //Jump
        if (input.JumpButtonPressed())
        {
            if (CanJump)
            {
                if (GrabbingLedge)
                {
                    GrabbingLedge = false;
                    rb.simulated = true;
                    anim.LedgeGrabbing();
                }
                Jump(jumpSpeed);
                jumpCount++;
                CanJump = false;
                anim.Jumping();
            }
            JumpHold = true;
            if (Falling)
            {
                if (HasUnlockedFloatFall && !CanJump)
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
            if (jumpCount == 1 && HasUnlockedDoubleJump)
            {
                CanJump = true;
                DoubleJump = true;
            }
        }

        yVelocity = rb.velocity.y;

        if (yVelocity > .1f)
        {
            Jumping = true;
            anim.Jumping();
            anim.Falling();

            if (JumpHold && !DoubleJump)
            {
                AddHeightToJump();
            }
        }
        else if (yVelocity < -.1f)
        {
            Jumping = false;
            LedgeJumping = false;
            anim.Jumping();
            CanJump = false;
            Falling = true;
            anim.Falling();
            if (jumpCount == 1 && HasUnlockedDoubleJump && !JumpHold)
            {
                CanJump = true;
                DoubleJump = true;
                FallingDoubleJump = true;
            }
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

        if (Mathf.Abs(horLAxis) == 1)
        {
            if (!Running)
            {
                PrevDirection = Direction;
                Running = true;
            }
        }
        else
        {
            Running = false;
        }

        //Dash
        if (Dashing)
        {
            Dashing = TimerTick(dashTime);
            if (!Dashing)
            {
                CanDash = false;
                PostDash = true;
                Dashing = false;
            }
        }
        else if (PostDash)
        {
            PostDash = TimerTick(PostDashTime);
            CanDash = false;
        }
        else if (input.DashButtonPressed() && CanDash) //Input.GetButtonDown("Dash") 
        {
            if (Jumping)
            {
                
            }
            else if (Falling)
            {

            }
            else if (HasUnlockedDashing)
            {
                Dash();
            }
        }

        //Grab Ledge
        if (input.GrabButtonPressed() && HasUnlockedLedgeGrabbing && !objInHands && !LedgeJumping) //Input.GetButton("Grab")
        {
            GrabLedge();
        }
        else
        {
            LetGoLedge();
        }

        if (GrabbingLedge && !JumpHold)
        {
            CanJump = true;
        }
    }

    void Walk(float dir)
    {
        float airwalkModifier = 1;
        if (Jumping || Falling)
        {
            airwalkModifier -= (Mathf.Abs(yVelocity) / 50);
        }
        if (Dashing)
        {
            rb.velocity = new Vector2(dir * airwalkModifier * dashSpeed, rb.velocity.y);
        }
        else if (PostDash && (Jumping || Falling))
        {
            dashWalkTransition = Vector2.Lerp(new Vector2(dashSpeed, 0), new Vector2(walkSpeed, 0), .5f).x;
        }
        else
        {
            rb.velocity = new Vector2(dir * airwalkModifier * walkSpeed, rb.velocity.y);
        }
        walkVelocity = xVelocity;
    }

    void DetermineDirection (float dir)
    {
        if ((dir < 0 && Direction == 1) || (dir > 0 && Direction == -1))
            LookBack = true;

        if (dir > 0)
        {
            if (Direction == 1 && frameTimer > 10)
                LookBack = false;
            Direction = 1;
        }
        else if (dir < 0)
        {
            if (Direction == -1 && frameTimer > 10)
                LookBack = false;
            Direction = -1;
        }
        if (LookBack)
        {
            frameTimer++;
        }
        else
        {
            frameTimer = 0;
        }
        anim.LookingBack();
    }

    void Jump(float force)
    {
        if (DoubleJump)
        {
            if (FallingDoubleJump)
            {
                //force *= 1.5f;
                FallingDoubleJump = false;
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
            DoubleJump = false;
        }
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        audio.PlayOneShot(jump);
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
    }

    void ForceFall()
    {
        rb.AddForce(Vector2.down * 5 * fallSpeed);
    }

    void Dash()
    {
        Dashing = true;
    }

    bool TimerTick(float limit)
    {
        if (limit <= timer)
        {
            globalSeconds = 0;
            timer = 0;
            return false;
        }
        timer += Time.deltaTime;
        int seconds = (int)timer % 60;
        return true;
    }

    /// <summary>
    /// Returns true if object is grabbed
    /// </summary>
    private bool GrabObject()
    {
        RaycastHit2D hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.4f), Vector2.right * Direction, Color.yellow);

        if (objInHands != null)
        {
            return false;
        }
        // Does the ray intersect any objects horizontally adjacent
        if (hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.4f), Vector2.right * Direction, 1, layerMask))
        {
            if (grabbableTags.Contains(hit.transform.tag))
            {
                objInHands = hit.transform.gameObject;
                Rigidbody2D objRb = objInHands.GetComponent<Rigidbody2D>();
                objRb.velocity = Vector2.zero;
                objRb.simulated = false;
                objInHands.layer = 8;
                objInHands.transform.eulerAngles = Vector2.zero;
                objInHands.transform.parent = hands.transform;
                objInHands.transform.localPosition = Vector2.zero;
                HoldObjectInHands();
                HoldingObj = true;
                return true;
            }
        }
        // Does the ray intersect any objects vertically downward?
        else if (hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.4f), Vector2.down, 1, layerMask))
        {
            if (grabbableTags.Contains(hit.transform.tag))
            {
                objInHands = hit.transform.gameObject;
                Rigidbody2D objRb = objInHands.GetComponent<Rigidbody2D>();
                objRb.velocity = Vector2.zero;
                objRb.simulated = false;
                objInHands.layer = 8;
                objInHands.transform.eulerAngles = Vector2.zero;
                objInHands.transform.parent = hands.transform;
                objInHands.transform.localPosition = Vector2.zero;
                HoldObjectInHands();
                HoldingObj = true;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Call each time an object is picked up, and each time the player changes direction
    /// </summary>
    void HoldObjectInHands()
    {
        if (objInHands != null)
        {
            if (!itemInHands)
            {
                itemInHands = objInHands.GetComponent<Item>();
            }
            if (itemInHands.type == Item.Type.GUN)
            {
                gunInHands = objInHands.GetComponent<GunBehavior>();

                WeaponMenuController weaponMenu = gameMenu.GetWeaponsMenu();
                gunInHands.SetFireRate(weaponMenu.GetFireRate());
                gunInHands.SetRicochets(weaponMenu.GetRicochets());
            }

            Vector2 newPos = hands.transform.localPosition;
            newPos.x = .4f * Direction;
            hands.transform.localPosition = newPos;
            Vector2 newRot = hands.transform.eulerAngles;
            if (Direction == 1)
            {
                newRot.y = 0;
            }
            if (Direction == -1)
            {
                newRot.y = 180;
            }
            hands.transform.eulerAngles = newRot;
            if (objInHands && objInHands.GetComponent<Item>().forceDirection)
            {
                Vector2 newHandRot = objInHands.transform.rotation.eulerAngles;
                newHandRot.y = newRot.y;
                objInHands.transform.eulerAngles = newHandRot;
            }
        }
    }

    void DropObject()
    {
        if (objInHands != null)
        {
            objInHands.GetComponent<Rigidbody2D>().simulated = true;
            objInHands.layer = 0;
            objInHands.transform.parent = null;
            Vector2 newPos = objInHands.transform.position;
            newPos.x += 1 * Direction;
            objInHands.transform.position = newPos;
            objInHands = null;
            itemInHands = null;
            HoldingObj = false;

            Vector2 newRot = hands.transform.eulerAngles;
            newRot.y = 0;
            hands.transform.eulerAngles = newRot;
        }
    }

    void GrabLedge()
    {
        RaycastHit2D hit;
        int layerMask = LayerMask.GetMask("Ledge");

        // Does the ray intersect any objects
        if (hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), Vector2.right * Direction, 2, layerMask))
        {
            if (hit.collider.CompareTag("Ledge"))
            {
                Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 1), Vector2.right * Direction * 1.25f, Color.yellow);

                rb.velocity = Vector2.zero;
                rb.simulated = false;
                transform.position = Vector2.Lerp(transform.position, new Vector2(hit.collider.transform.position.x + (.75f * Direction) + (hit.collider.transform.localScale.x), hit.collider.transform.position.y), .05f);
                GrabbingLedge = true;
                anim.LedgeGrabbing();
            }
        }
    }

    void LetGoLedge()
    {
        rb.simulated = true;
        GrabbingLedge = false;
        anim.LedgeGrabbing();
    }

    bool IsGrounded()
    {
        RaycastHit2D hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        // Does the ray intersect any objects
        if (hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, layerMask))
        {
            if (hit.transform.CompareTag("Floor") || hit.transform.CompareTag("Wall"))
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
