using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    public enum Scenes { Playground, MainMenu }
    [SerializeField]
    string sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSingleScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    public static void LoadSingleScene(Scenes sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}
