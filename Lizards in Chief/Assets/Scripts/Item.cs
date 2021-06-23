using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private enum Type { PROJECTILE, CONSUMABLE, POWERUP, GUN, MELEE, EXPLOSIVE, OBJECT }

    [SerializeField]
    Type type;

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
