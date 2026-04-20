using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class TileBlock : MonoBehaviour
{
    public virtual void Update()
    {
        transform.localScale = new Vector2(Mathf.RoundToInt(transform.localScale.x), Mathf.RoundToInt(transform.localScale.y));
    }
}
