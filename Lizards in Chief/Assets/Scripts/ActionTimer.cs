using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTimer : MonoBehaviour
{
    public float timer = 0.0f;
    public int globalSeconds = 0;
    public float limit = 0;
    bool runTimer = false;

    public void TimerStart()
    {
        timer = limit;
        runTimer = true;
    }

    private void Update()
    {
        if (runTimer)
            runTimer = TimerTick();
    }

    bool TimerTick()
    {
        if (limit < timer)
        {
            globalSeconds = 0;
            timer = 0;
            runTimer = false;
            return false;
        }
        timer += Time.deltaTime;
        int seconds = (int)timer % 60;
        return true;
    }
}
