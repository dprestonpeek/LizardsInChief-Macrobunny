using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    Slider healthBar;
    [SerializeField]
    Text healthText;

    float lastBlipX;
    public float lastTurnAround;
    bool rightStep = true;

    public bool Jumping;
    public bool Falling;
    public bool Walking;
    public bool HoldingObj;
    public bool GrabbingLedge;
    public bool iFramesOn;
    public bool LookBack;
    public int Direction;

    [SerializeField]
    public bool HasUnlockedGlide;
    [SerializeField]
    public bool HasUnlockedDashing;
    [SerializeField]
    public bool HasUnlockedLedgeGrabbing;
    [SerializeField]
    public bool HasUnlockedDoubleJump;

    public bool CanJump;
    private bool CanHold;
    private bool JumpHold;
    private bool BackwardsJumping;
    private bool Running;
    private bool Dashing;
    private bool CanDash;
    private bool PostDash;
    private bool CanGlide;
    private bool LedgeJumping;
    private bool CanGrabOrDrop = true;
    private bool StoringItem;
    private bool EquippingItem;
    private bool Aiming;
    private bool UsingUtility;
    private int PrevDirection;

    public bool DoubleJump = false;
    public bool FallingDoubleJump = false;

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
    [Range(1, 3)]
    private int glideFall = 1;
    [SerializeField]
    [Range(1, 15)]
    private int fallSpeed = 3;
    [SerializeField]
    [Range(1, 5)]
    private int strength = 1;
    private int jumpCount = 0;

    [SerializeField]
    [Range(1, 10)]
    private int walkSpeed = 5;
    [SerializeField]
    [Range(1, 10)]
    private int footstepSpeed = 1;
    [SerializeField]
    [Range(1, 10)]
    private int dashSpeed = 5;
    [SerializeField]
    [Range(1, 10)]
    private int dashTime = 1;
    [Range(1, 10)]
    [SerializeField]
    private int PostDashTime = 10;
    [SerializeField]
    [Range(1, 10)]
    private int lookbackThreshold = 1;
    [SerializeField]
    [Range(0, 10)]
    private int iFrames;
    [SerializeField]
    [Range(0, 1)]
    private float iFrameTime;
    private int curriFrames = 0;
    private float dashWalkTransition = 0;

    public float xVelocity;
    public float yVelocity;
    public float horLAxis;
    public float vertLAxis;
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
    [SerializeField]
    GameObject crosshair;

    private float timer = 0.0f;
    private int globalSeconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<ProtagAnimator>();
        inventory = GetComponentInChildren<PlayerInventory>();
        objInHands = null;
        itemInHands = null;
        lastTurnAround = transform.position.x;

        LoadAbilities();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DoMovement();

        #region Inventory

        if (Input.GetButton("Grab"))
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

        rightJoystickHor = Input.GetAxis("InventoryHor");
        rightJoystickVert = Input.GetAxis("InventoryVert");

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
        if (gunInHands)
        {
            if (Input.GetAxis("Fire1") == 1)
            {
                if (Aiming)
                    gunInHands.Shoot(crosshair.transform.localPosition);
                else 
                    gunInHands.Shoot(Vector2.right * Direction);
            }
        }
        #endregion

        #region Visuals
        HoldObjectInHands();
        if (Input.GetAxis("Utility") == 1)
            UseUtility();
        else
            StopUsingUtility();
        #endregion
    }

    private void Update()
    {
        #region Pause
        if (Input.GetButtonDown("Pause"))
        {
            if (Time.timeScale == 1)
                gameMenu.Toggle(true);
            else
                gameMenu.Toggle(false);
        }
        #endregion
    }

    #region Abilities
    private void LoadAbilities()
    {
        HasUnlockedGlide = IntToBool(PlayerPrefs.GetInt("glide", 0));
        HasUnlockedDashing = IntToBool(PlayerPrefs.GetInt("dashing", 0));
        HasUnlockedLedgeGrabbing = IntToBool(PlayerPrefs.GetInt("ledgegrab", 0));
        HasUnlockedDoubleJump = IntToBool(PlayerPrefs.GetInt("doublejump", 0));
    }

    public void DecreaseHeath(int amount)
    {
        healthBar.value -= amount;
        healthText.text = healthBar.value.ToString();
        iFramesOn = true;
    }

    public void IncreaseHealth(int amount)
    {
        healthBar.value += amount / 100;
    }

    public GunBehavior GetGunInHands()
    {
        if (!gunInHands)
            return null;
        return gunInHands.GetComponent<GunBehavior>();
    }

    private void RunIFrames()
    {
        if (curriFrames < iFrames)
        {
            if (!TimerTick(iFrameTime))
            {
                curriFrames++;
                anim.gameObject.SetActive(!anim.gameObject.activeSelf);
            }
        }
        else
        {
            iFramesOn = false;
            curriFrames = 0;
            anim.gameObject.SetActive(true);
        }
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
            BackwardsJumping = false;
            anim.Grounded();
        }

        horLAxis = Input.GetAxis("Horizontal");
        vertLAxis = Input.GetAxis("Vertical");

        if (GrabbingLedge)
        {
            Jumping = false;
            Falling = false;
            BackwardsJumping = false;
        }
        else if (!BackwardsJumping)
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
                PlayFootsteps();
            }
        }
        else
        {
            Walking = false;
        }

        xVelocity = rb.velocity.x;

        DoJumpMovement();

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
            BackwardsJumping = false;
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
                Running = true;
            }
        }
        else
        {
            Running = false;
        }

        if (iFramesOn)
        {
            Dashing = false;
            RunIFrames();
        }
        else
        {
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
            else if (Input.GetButtonDown("Dash") && CanDash)
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
        }
        

        if (Input.GetButton("Grab") && HasUnlockedLedgeGrabbing && !objInHands && !LedgeJumping)
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

    void PlayFootsteps()
    {
        if (transform.position.x != lastBlipX)
        {
            if (Direction == 1)
            {
                if (transform.position.x > lastBlipX + (footstepSpeed * .1f))
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
                if (transform.position.x < lastBlipX - (footstepSpeed * .1f))
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

    void DoJumpMovement()
    {
        if (Input.GetButton("Jump"))
        {
            JumpHold = true;
            if (CanJump)
            {
                if (GrabbingLedge)
                {
                    GrabbingLedge = false;
                    rb.simulated = true;
                    anim.LedgeGrabbing();
                }
                if (LookBack)
                {
                    BackwardsJumping = true;
                }
                Jump(jumpSpeed);
                jumpCount++;
                CanJump = false;
                anim.Jumping();
                CanGlide = true;
            }
            else
            {
                if (yVelocity < -0.25f)
                {
                    if (HasUnlockedGlide && !CanJump && !DoubleJump && !IsGrounded())
                        Glide();
                }
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
        PrevDirection = Direction;
        if (dir > 0)
        {
            Direction = 1;
        }
        else if (dir < 0)
        {
            Direction = -1;
        }

        if (PrevDirection != Direction)
        {
            LookBack = true;
            lastTurnAround = transform.position.x;
        }
        if (LookBack)
        {
            if (transform.position.x < lastTurnAround - (lookbackThreshold * .1f) && Direction == -1 ||
                transform.position.x > lastTurnAround + (lookbackThreshold * .1f) && Direction == 1)
            {
                LookBack = false;
            }
        }

        if ((transform.position.x > lastTurnAround - (lookbackThreshold * .1f) && transform.position.x <= lastTurnAround && Direction == -1) ||
            transform.position.x < lastTurnAround + (lookbackThreshold * .1f) && transform.position.x >= lastTurnAround && Direction == 1)
        {
            LookBack = true;
        }
        else
            LookBack = false;

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

    void Glide()
    {
        rb.velocity = new Vector2(rb.velocity.x, (4 - glideFall) * -1);
        //rb.AddForce(Vector2.up * 2 * glideFall);
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

    GameObject DropObject()
    {
        if (objInHands != null)
        {
            GameObject droppedObj;
            objInHands.GetComponent<Rigidbody2D>().simulated = true;
            objInHands.layer = 0;
            objInHands.transform.parent = null;
            Vector2 newPos = objInHands.transform.position;
            newPos.x += 1 * Direction;
            objInHands.transform.position = newPos;
            droppedObj = objInHands;
            objInHands = null;
            itemInHands = null;
            HoldingObj = false;

            Vector2 newRot = hands.transform.eulerAngles;
            newRot.y = 0;
            hands.transform.eulerAngles = newRot;
            return droppedObj;
        }
        return null;
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

    void UseUtility()
    {
        UsingUtility = true;
        if (itemInHands)
        {
            if (itemInHands.type == Item.Type.PROJECTILE || itemInHands.type == Item.Type.GUN)
            {
                crosshair.SetActive(true);
                Aiming = true;
                crosshair.transform.localPosition = new Vector2(horLAxis, vertLAxis);
            }
        }
    }

    void StopUsingUtility()
    {
        if (UsingUtility)
        {
            if (itemInHands)
            {
                if (itemInHands.type == Item.Type.PROJECTILE || itemInHands.type == Item.Type.GUN)
                {
                    if (itemInHands.type == Item.Type.PROJECTILE)
                    {
                        GameObject droppedobj = DropObject();
                        droppedobj.gameObject.GetComponent<Rigidbody2D>().AddForce(crosshair.transform.localPosition * strength * 5, ForceMode2D.Impulse);
                        CanGrabOrDrop = false;
                    }
                    crosshair.SetActive(false);
                    Aiming = false;
                }
            }

            if (!CanGrabOrDrop)
            {
                CanGrabOrDrop = !TimerTick(.5f);
            }
            UsingUtility = false;
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
            if (hit.transform.CompareTag("Floor") || hit.transform.CompareTag("Wall") || hit.transform.CompareTag("Object"))
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
