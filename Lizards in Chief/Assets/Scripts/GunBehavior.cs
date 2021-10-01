using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    [SerializeField]
    AudioSource shootSound;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform bulletSpawn;
    [SerializeField]
    [Range(1, 5)]
    int numberBullets = 1;
    [SerializeField]
    [Range(1, 10)]
    public int bulletSpeed = 5;
    [SerializeField]
    [Range(0, 3)]
    int bulletRicochets = 0;
    [SerializeField]
    [Range(1, 10)]
    int fireRate = 3;

    float timer = 0.0f;
    int globalSeconds = 0;
    bool waitToFire = false;
    public float timelimit = 0;

    private void Awake()
    {
    }

    public void Shoot(Vector2 direction)
    {
        if (!waitToFire)
        {
            if (numberBullets > 1)
            {
                ShootMultiple(direction);
                return;
            }
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.identity);
            BlasterBullet behavior = bullet.GetComponent<BlasterBullet>();
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed * 10, ForceMode2D.Impulse);
            behavior.SetRicochetCount(bulletRicochets);
            behavior.SetSourceWeapon(gameObject);
            waitToFire = true;
            shootSound.Play();
        }
    }

    private void ShootMultiple(Vector2 direction)
    {
        for (int i = 0; i < numberBullets; i++)
        {
            if (i > 0)
            {
                bulletSpawn.rotation = Quaternion.Euler(bulletSpawn.rotation.eulerAngles.x, bulletSpawn.rotation.eulerAngles.y, Random.Range(-5, 5));
            }
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.identity);
            BlasterBullet behavior = bullet.GetComponent<BlasterBullet>();
            bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpawn.right * bulletSpeed * 10, ForceMode2D.Impulse);
            behavior.SetRicochetCount(bulletRicochets);
            behavior.SetSourceWeapon(gameObject);
        }
        waitToFire = true;
        shootSound.Play();
    }

    private void Update()
    {
        if (waitToFire)
            waitToFire = TimerTick((10 - Mathf.Round(fireRate)) / 7);
    }

    public void SetFireRate(int newRate)
    {
        fireRate = newRate;
    }

    public void SetRicochets(int newRicos)
    {
        bulletRicochets = newRicos;
    }

    bool TimerTick(float limit)
    {
        timelimit = limit;
        if (limit < timer)
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
