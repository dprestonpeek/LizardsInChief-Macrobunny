using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    public static SceneHelper Instance;

    public enum Scenes { Playground, MainMenu, Gym, Gym2, Gym3, Gym4 }
    [SerializeField]
    string sceneToLoad;

    Scene currentActiveScene;
    bool updateCurrentScene = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Instance = this;
        currentActiveScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if (updateCurrentScene)
        {
            currentActiveScene = SceneManager.GetActiveScene();
        }
    }

    public static void ResetCurrentScene()
    {
        SceneManager.LoadScene(Instance.currentActiveScene.name);
        Time.timeScale = 1;

    }

    public static void SetSceneToLoad(string toLoad)
    {
        Instance.sceneToLoad = toLoad;
    }

    public static void LoadSingleScene()
    {
        SceneManager.LoadScene(Instance.sceneToLoad);
        Instance.updateCurrentScene = true;
        Time.timeScale = 1;
    }
    public static void LoadSingleScene(Scenes sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
        Instance.updateCurrentScene = true;
        Time.timeScale = 1;
    }
}
