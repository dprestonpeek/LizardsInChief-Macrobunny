using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject entityToSpawn;
    [SerializeField]
    [Range(0,10)]
    int delay;

    private float timer = 0.0f;
    private int globalSeconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!TimerTick(delay))
        {
            Instantiate(entityToSpawn, transform.position, Quaternion.identity);
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
