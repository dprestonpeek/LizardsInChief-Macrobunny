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

    public void Throw(GameObject objInHands, int direction)
    {
        objInHands.GetComponent<Rigidbody2D>().AddForce(objInHands.transform.right * projSpeed * 10, ForceMode2D.Impulse);
    }

}
