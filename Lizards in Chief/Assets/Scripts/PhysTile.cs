using System;
using UnityEditor.TerrainTools;
using UnityEngine;

[ExecuteInEditMode]
public class PhysTile : MonoBehaviour
{
    private float tileSize = 64;

    public virtual void Update()
    {
        Vector2 newPos = GetAdjustedTilePosition(transform.position.x, transform.position.y);
        transform.position = newPos;

        Vector2 newScale = GetAdjustedTileScale(transform.localScale.x, transform.localScale.y);
        transform.localScale = newScale;
    }

    private Vector2 GetAdjustedTilePosition(float x, float y)
    {
        float offsetX;
        float offsetY;
        //Get Scaling offset
        float xScale = transform.localScale.x;
        float yScale = transform.localScale.y;

        int scaleModX = Mathf.RoundToInt((transform.localScale.x * 100) % (tileSize * 2));
        if (scaleModX == tileSize * 2)
        {
            offsetX = 0;
        }
        else
        {
            offsetX = 32;
        }
        int scaleModY = Mathf.RoundToInt((transform.localScale.y * 100) % (tileSize * 2));
        if (scaleModY == tileSize * 2)
        {
            offsetY = 0;
        }
        else
        {
            offsetY = 32;
        }

        x = (x * 100);
        y = (y * 100);
        x -= offsetX;
        y -= offsetY;

        double currXoffset = Math.Round(((x % tileSize) / 100), 6);
        double currYoffset = Math.Round(((y % tileSize) / 100), 6);
        double newXpos = Math.Round(transform.position.x - currXoffset, 6);
        double newYpos = Math.Round(transform.position.y - currYoffset, 6);

        return new Vector2((float)newXpos, (float)newYpos);
    }

    private Vector2 GetAdjustedTileScale(float x, float y)
    {
        x = (x * 100);
        y = (y * 100);

        double currXoffset = Math.Round(((x % tileSize) / 100), 6);
        if (currXoffset > (tileSize / 2) / 100)
        {
            x += tileSize;
            currXoffset = Math.Round(((x % tileSize) / 100), 6);
        }
        double currYoffset = Math.Round(((y % tileSize) / 100), 6);
        if (currYoffset > (tileSize / 2) / 100)
        {
            y += tileSize;
            currYoffset = Math.Round(((y % tileSize) / 100), 6);
        }

        double newXpos = Math.Round(transform.localScale.x - currXoffset, 6);
        double newYpos = Math.Round(transform.localScale.y - currYoffset, 6);

        return new Vector2((float)newXpos, (float)newYpos);
    }
}
