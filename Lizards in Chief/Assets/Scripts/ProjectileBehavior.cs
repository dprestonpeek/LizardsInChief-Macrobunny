using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField]
    [Range(1, 10)]
    public int projSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Throw(GameObject objInHands, int direction, float power, float verticalAngle)
    {
        objInHands.GetComponent<Rigidbody2D>().AddForce(new Vector2(objInHands.transform.right.x, verticalAngle) * projSpeed * power, ForceMode2D.Impulse);
    }

}
