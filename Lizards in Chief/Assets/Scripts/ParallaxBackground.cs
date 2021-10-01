using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    PlayerScript player;

    [SerializeField]
    GameObject top;
    [SerializeField]
    GameObject bottom;
    [SerializeField]
    GameObject focus;

    float initialX;
    float playerX;
    float initFocus;

    float topMod = .5f;
    float botMod = .75f;
    float focMod = .85f;

    int topZ = 15;
    int botZ = 10;
    int focZ = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
        initialX = player.transform.position.x;
        initFocus = focus.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        playerX = player.transform.position.x;
        top.transform.position = new Vector3(playerX * topMod, top.transform.position.y, topZ);
        bottom.transform.position = new Vector3(playerX * botMod, bottom.transform.position.y, botZ);
        focus.transform.position = new Vector3((playerX * focMod) + initFocus, focus.transform.position.y, focZ);
    }
}
