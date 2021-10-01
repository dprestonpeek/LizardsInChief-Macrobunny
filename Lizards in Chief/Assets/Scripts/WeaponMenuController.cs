using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenuController : MonoBehaviour
{
    [SerializeField]
    GameMenuController menu;
    PlayerScript player;
    [SerializeField]
    Slider fireRate;
    [SerializeField]
    Slider ricochets;

    int defFireRate = 3;
    int defRicochets = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = menu.GetPlayer();
        ricochets.value = PlayerPrefs.GetInt("ricochets", defRicochets);
    }

    private void Update()
    {

    }

    public void SetRicochets()
    {
        if (!player || !player.GetGunInHands())
            return;
        else
        {
            player.GetGunInHands().SetRicochets(Mathf.RoundToInt(ricochets.value));
            PlayerPrefs.SetInt("ricochets", defRicochets);
            PlayerPrefs.Save();
        }
    }

    public int GetRicochets()
    {
        return Mathf.RoundToInt(ricochets.value);
    }
}
