using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    PlayerScript player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float yPos;
        if (player.transform.position.y > 4)
        {
            yPos = player.transform.position.y - 4;
        }
        else
        {
            yPos = 0;
        }
        transform.position = new Vector3(player.transform.position.x, yPos, transform.position.z);
    }
}
