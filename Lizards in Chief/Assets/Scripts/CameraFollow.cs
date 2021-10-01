using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    PlayerScript player;
    [SerializeField]
    float heightLimit = 4;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float yPos;
        if (player.transform.position.y > heightLimit)
        {
            yPos = player.transform.position.y - heightLimit;
        }
        else
        {
            yPos = 0;
        }
        transform.position = new Vector3(player.transform.position.x, yPos, transform.position.z);
    }
}
