using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlaster : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform bulletSpawn;
    [SerializeField]
    int numberBullets = 10;
    [SerializeField]
    bool infiniteReload = false;
    [SerializeField]
    [Range(1, 10)]
    public int bulletSpeed = 3;

    public void Shoot(Vector2 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
    }
}
