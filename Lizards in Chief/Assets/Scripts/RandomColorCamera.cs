using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorCamera : MonoBehaviour
{
    [SerializeField]
    Material[] colors = new Material[ 0 ];

    // Start is called before the first frame update
    void Start()
    {

        Random rand = new Random();
        GetComponent<Camera>().backgroundColor = colors[Random.Range(0, colors.Length)].color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
