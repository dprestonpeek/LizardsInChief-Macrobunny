using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagAnimator : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sprite;
    PlayerScript player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        player = GetComponentInParent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Direction
        if (player.Direction == -1)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        //Walking
        anim.SetBool("Walking", player.Walking);
        if (Mathf.Abs(player.xVelocity) > 0)
        {
            anim.speed = Mathf.Abs(player.horLAxis);
        }
        else
        {
            anim.SetBool("Walking", false);
            anim.speed = 1;
        }

        //Jumping & Falling
        anim.SetBool("Jumping", player.Jumping);
        anim.SetBool("Falling", player.Falling);
    }
}
