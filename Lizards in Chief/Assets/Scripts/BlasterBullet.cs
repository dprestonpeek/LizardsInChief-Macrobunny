using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBullet : MonoBehaviour
{
    int ricochetLimit = 0;
    int ricochetCount = 0;
    GameObject sourceWeapon = null;
    Rigidbody2D rb = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetRicochetCount(int amount)
    {
        ricochetLimit = amount;
    }

    public void SetSourceWeapon(GameObject weapon)
    {
        sourceWeapon = weapon;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            //Award points
        }
        if (!other.gameObject.Equals(sourceWeapon))
        {
            if (ricochetCount == ricochetLimit)
            {
                Destroy(gameObject);
            }
            else
            {
                ricochetCount++;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (rb.velocity.x < .1f && rb.velocity.y < .1f)
        {
            Destroy(gameObject);
        }
    }
}
