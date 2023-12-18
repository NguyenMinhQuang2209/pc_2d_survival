using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadNewScene(SceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString(), LoadSceneMode.Single);
    }
}
public enum SceneName
{
    SelectCharacter,
    Main
}