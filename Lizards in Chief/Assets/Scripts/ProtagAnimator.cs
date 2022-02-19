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

        //Holding
        anim.SetBool("Holding", player.HoldingObj);

        ////Looking Back
        //if (player.LookBack)
        //{
        //    if (player.HoldingObj)
        //    {
        //        anim.SetBool("LookBackHold", player.LookBack);
        //    }
        //    else
        //    {
        //        anim.SetBool("LookBack", player.LookBack);
        //    }
        //}
        //else if (player.LookBack)
        //{
        //}
    }

    public void Walking()
    {
        anim.SetBool("Walking", player.Walking);
    }

    public void Jumping()
    {
        anim.SetBool("Jumping", player.Jumping);
    }

    public void Falling()
    {
        anim.SetBool("Falling", player.Falling);
    }

    public void Holding()
    {
        anim.SetBool("Holding", player.HoldingObj);
    }

    public void LedgeGrabbing()
    {
        anim.SetBool("LedgeGrabbing", player.GrabbingLedge);
    }

    public void LookingBack()
    {
        //if (player.HoldingObj && player.LookBack)
        //{
        //    anim.SetBool("LookingBackHold", true);
        //    anim.SetBool("LookingBack", false);
        //}
        //else if (player.LookBack)
        //{
        //    anim.SetBool("LookingBack", true);
        //    anim.SetBool("LookingBackHold", false);
        //}
        //else
        //{
        //    anim.SetBool("LookingBack", false);
        //    anim.SetBool("LookingBackHold", false);
        //}
    }

    public void Grounded()
    {
        anim.SetBool("Jumping", false);
        anim.SetBool("Falling", false);
    }
}
