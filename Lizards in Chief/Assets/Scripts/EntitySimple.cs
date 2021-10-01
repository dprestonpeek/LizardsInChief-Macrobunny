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
    bool checkForLedge = true;
    [SerializeField]
    bool startFacingLeft = true;

    int Direction;

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
    void Update()
    {
        Walk();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Instantiate(killSound);
            Destroy(gameObject);
        }
    }

    void Walk()
    {
        transform.position = new Vector2(transform.position.x + .01f * walkSpeed * Direction, transform.position.y);
        //rb.AddForce(Vector2.right * walkSpeed * Direction);
        //rb.velocity = new Vector2(walkSpeed * Direction, rb.velocity.y);
        anim.speed = animSpeed / 2;

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
                if (hit.collider.gameObject.CompareTag("Wall") || hit.collider.gameObject.CompareTag("Floor") || hit.collider.gameObject.CompareTag("Object"))
                    ChangeDirection();
            }
        }
    }

    void ChangeDirection()
    {
        Direction *= -1;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
            if (!player.iFramesOn)
            {
                player.DecreaseHeath(damageDealt);
            }
            ChangeDirection();
        }
        else if(other.gameObject.CompareTag("Object") || other.gameObject.CompareTag("Wall"))
        {
            ChangeDirection();
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            ChangeDirection();
        }
    }
}
