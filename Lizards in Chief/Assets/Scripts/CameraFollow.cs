using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    PlayerScript player;
    [SerializeField]
    float heightLimit = 4;

    [SerializeField]
    float xOffset = 0;
    [SerializeField]
    float yOffset = -1;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float xPos, yPos;
        if (player.transform.position.y > heightLimit)
        {
            yPos = player.transform.position.y - heightLimit + yOffset;
        }
        else
        {
            yPos = yOffset;
        }
        xPos = player.transform.position.x + xOffset;

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    }
}
