using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySimple : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    GameObject defSprite;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject killSound;

    public bool isGrounded;
    public bool gonnaFall;

    [SerializeField]
    [Range(1, 10)]
    int walkSpeed = 3;
    [SerializeField]
    [Range(1, 10)]
    int animSpeed = 5;
    [SerializeField]
    [Range(1, 100)]
    int damageDealt = 2;
    [SerializeField]
    [Range(1, 100)]
    int health = 10;
    [SerializeField]
    [Range(1, 10)]
    int antiRotation = 4;
    [SerializeField]
    bool checkForLedge = true;
    [SerializeField]
    bool startFacingLeft = true;

    public bool takingDamage = false;
    public bool changingDir = false;
    int Direction;

    List<string> directionChangingTags = new List<string>() { "Wall", "Floor", "Object", "Ledge", "Player", "Entity" };

    private float timer = 0.0f;
    private int globalSeconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (startFacingLeft)
        {
            Direction = -1;
            spriteRenderer.flipX = true;
        }
        else
        {
            Direction = 1;
            spriteRenderer.flipX = false;
        }
        defSprite.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsGrounded();
        if (takingDamage)
        {
            if (Mathf.Abs(rb.velocity.x) < 1)
                takingDamage = false;
        }
        else if (isGrounded)
            Walk();

        if (changingDir)
        {
            changingDir = TimerTick(1 / antiRotation);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Instantiate(killSound);
            Destroy(gameObject);
        }
        takingDamage = true;
    }

    void IsGrounded()
    {
        RaycastHit2D hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        if (checkForLedge)
        {
            // Does the ray intersect any floors horizontally adjacent
            if (hit = Physics2D.Raycast(new Vector2(transform.position.x + Direction, transform.position.y), Vector2.down, 1, layerMask))
            {
                isGrounded = true;
            }
            else
            {
                if (isGrounded)
                    ChangeDirection();
                isGrounded = false;
            }
        }
        Debug.DrawRay(new Vector2(transform.position.x + Direction, transform.position.y), Vector2.down, Color.yellow);
        // Does the ray intersect any floors directly underneath
        if (!(hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.down, 1, layerMask)))
        {
            isGrounded = false;
        }
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.down, Color.yellow);
        // Does the ray intersect any walls horizontally adjacent
        if (hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right * Direction, 1.25f, layerMask))
        {
            if (!hit.collider.gameObject.Equals(gameObject))
            {
                if (directionChangingTags.Contains(hit.collider.gameObject.tag))
                    ChangeDirection();
            }
        }
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * Direction * 1.25f, Color.yellow);
    }

    void Walk()
    {
        transform.position = new Vector2(transform.position.x + .01f * walkSpeed * Direction, transform.position.y);
        //rb.AddForce(Vector2.right * walkSpeed * Direction);
        //rb.velocity = new Vector2(walkSpeed * Direction, rb.velocity.y);
        anim.speed = animSpeed / 2;
    }

    void ChangeDirection()
    {
        if (isGrounded && !changingDir)
        {
            changingDir = true;
            Direction *= -1;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
            player.DecreaseHeath(damageDealt);
            ChangeDirection();
        }
        else if(directionChangingTags.Contains(other.gameObject.tag))
        {
            ChangeDirection();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Ledge"))
        {
            ChangeDirection();
        }
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
}
