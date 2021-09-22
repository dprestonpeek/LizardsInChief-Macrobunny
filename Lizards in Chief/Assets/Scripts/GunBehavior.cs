using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour
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
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.identity);
            BlasterBullet behavior = bullet.GetComponent<BlasterBullet>();
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * bulletSpeed * 10, ForceMode2D.Impulse);
            behavior.SetRicochetCount(bulletRicochets);
            behavior.SetSourceWeapon(gameObject);
            waitToFire = true;
        }
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
