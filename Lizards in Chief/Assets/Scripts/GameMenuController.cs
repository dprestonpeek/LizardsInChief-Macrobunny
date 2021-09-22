using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuController : MonoBehaviour
{
    [SerializeField]
    PlayerScript player;

    [SerializeField]
    List<GameObject> menus = new List<GameObject>();

    public void Toggle()
    {
        if (Time.timeScale == 1)
            Toggle(true);
        else
            Toggle(false);
    }

    public void Toggle(bool enable)
    {
        if (enable)
        {
            foreach (GameObject menu in menus)
            {
                if (menu.Equals(menus[0]))
                {
                    menu.SetActive(true);
                }
                else
                {
                    menu.SetActive(false);
                }
            }
            Pause(true);
        }
        else
        {
            foreach (GameObject menu in menus)
            {
                menu.SetActive(false);
            }
            Pause(false);
        }
    }

    public static bool Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            return true;
        }
        else
        {
            Time.timeScale = 1;
            return false;
        }
    }

    public static bool Pause(bool pauseGame)
    {
        if (pauseGame)
        {
            Time.timeScale = 0;
            return true;
        }
        else
        {
            Time.timeScale = 1;
            return false;
        }
    }

    public void PauseGame()
    {
        Pause();
    }

    public PlayerScript GetPlayer()
    {
        return player;
    }

    public WeaponMenuController GetWeaponsMenu()
    {
        foreach (GameObject menu in menus)
        {
            if (menu.name.Contains("Weapon"))
                return menu.GetComponent<WeaponMenuController>();
        }
        return null;
    }

    public void GoToMainMenu()
    {
        SceneHelper.LoadSingleScene(SceneHelper.Scenes.MainMenu);
    }

    public void ResetScene()
    {
        SceneHelper.LoadSingleScene(SceneHelper.Scenes.Playground);
    }
}
