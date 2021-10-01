using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlaying : MonoBehaviour
{
    [SerializeField]
    AudioSource source;

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
