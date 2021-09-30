using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardHook : MonoBehaviour
{
    [SerializeField]
    bool inUse;
    [SerializeField]
    [Range(1, 7)]
    int hookLength = 3;

    [SerializeField]
    GameObject shaft;
    [SerializeField]
    GameObject claw;
    [SerializeField]
    SpriteRenderer clawSprite;

    [SerializeField]
    Sprite clawOpen;
    [SerializeField]
    Sprite clawClosed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inUse)
        {
            claw.transform.localScale = Vector2.one;
            shaft.transform.localScale = new Vector2(Mathf.Lerp(0, hookLength, .1f), shaft.transform.localScale.y);
        }
        else
        {
            shaft.transform.localScale = new Vector2(Mathf.Lerp(hookLength, 0, .1f), shaft.transform.localScale.y);
            if (shaft.transform.localScale.x == 0)
            {
                claw.transform.localScale = Vector2.zero;
            }
        }
    }
}
