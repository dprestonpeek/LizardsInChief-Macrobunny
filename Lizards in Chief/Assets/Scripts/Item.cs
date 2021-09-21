using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { PROJECTILE, CONSUMABLE, POWERUP, GUN, MELEE, EXPLOSIVE, OBJECT }

    [SerializeField]
    public Type type;

    [SerializeField]
    public bool forceDirection;

    [SerializeField]
    object[] typeSettings;

    public Item()
    {
        switch(type)
        {
            case Type.PROJECTILE:
                typeSettings = projSettings;
                break;
            case Type.CONSUMABLE:
                typeSettings = consSettings;
                break;
            case Type.GUN:
                typeSettings = gunSettings;
                break;
        }
    }

    object[] projSettings;
    object[] consSettings;
    object[] powerupSettings;
    object[] gunSettings;
    object[] meleeSettings;
    object[] explSettings;
    object[] objSettings;
}
