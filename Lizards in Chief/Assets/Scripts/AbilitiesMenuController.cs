using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitiesMenuController : MonoBehaviour
{
    [SerializeField]
    GameMenuController menu;
    PlayerScript player;
    [SerializeField]
    Toggle floatFall;
    [SerializeField]
    Toggle dashing;
    [SerializeField]
    Toggle ledgeGrab;
    [SerializeField]
    Toggle doubleJump;

    // Start is called before the first frame update
    void Awake()
    {
        player = menu.GetPlayer();
        floatFall.isOn = player.HasUnlockedFloatFall;
        dashing.isOn = player.HasUnlockedDashing;
        ledgeGrab.isOn = player.HasUnlockedLedgeGrabbing;
        doubleJump.isOn = player.HasUnlockedDoubleJump;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
            player = menu.GetPlayer();
    }

    public void ToggleFloatFall()
    {
        if (!player)
            return;
        player.HasUnlockedFloatFall = floatFall.isOn;
        int unlock = 0;
        if (floatFall.isOn)
            unlock = 1;
        PlayerPrefs.SetInt("floatfall", unlock);
        PlayerPrefs.Save();
    }
    public void ToggleDashing()
    {
        if (!player)
            return;
        player.HasUnlockedDashing = dashing.isOn;
        int unlock = 0;
        if (dashing.isOn)
            unlock = 1;
        PlayerPrefs.SetInt("dashing", unlock);
        PlayerPrefs.Save();
    }
    public void ToggleLedgeGrabbing()
    {
        if (!player)
            return;
        player.HasUnlockedLedgeGrabbing = ledgeGrab.isOn;
        int unlock = 0;
        if (ledgeGrab.isOn)
            unlock = 1;
        PlayerPrefs.SetInt("ledgegrab", unlock);
        PlayerPrefs.Save();
    }
    public void ToggleDoubleJump()
    {
        if (!player)
            return;
        player.HasUnlockedDoubleJump = doubleJump.isOn;
        int unlock = 0;
        if (doubleJump.isOn)
            unlock = 1;
        PlayerPrefs.SetInt("doublejump", unlock);
        PlayerPrefs.Save();
    }
}
