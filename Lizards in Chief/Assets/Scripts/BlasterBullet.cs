using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBullet : MonoBehaviour
{
    int ricochetLimit = 0;
    int ricochetCount = 0;
    GameObject sourceWeapon = null;
    Rigidbody2D rb = null;

    [SerializeField]
    [Range(0, 100)]
    int damage = 10;

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
            other.gameObject.GetComponent<PlayerScript>().DecreaseHeath(damage);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Entity"))
        {
            try
            {
                EntitySimple entity = other.gameObject.GetComponent<EntitySimple>();
                entity.TakeDamage(damage);
                Destroy(gameObject);
            }
            catch
            {
                try
                {
                    //EntityComplex entity = other.gameObject.GetComponent<EntityComplex>();
                }
                catch
                {

                }
            }
        }
        if (!other.gameObject.Equals(sourceWeapon) && !other.gameObject.CompareTag("Bullet"))
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
        if (other.gameObject.CompareTag("OneShot"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!other.gameObject.Equals(sourceWeapon) && !other.gameObject.CompareTag("Bullet"))
        {
            if (rb.velocity.x < .1f && rb.velocity.y < .1f)
            {
                Destroy(gameObject);
            }
        }
    }
}
