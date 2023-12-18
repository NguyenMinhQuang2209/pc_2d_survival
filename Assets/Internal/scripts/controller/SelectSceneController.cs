using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSceneController : MonoBehaviour
{
    public static SelectSceneController instance;
    public Transform characterShowContainer;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        SwitchCharacterController.instance.ShowCharacterItem();
    }
    public void LoadMainScene()
    {
        GameSceneManager.instance.LoadNewScene(SceneName.Main);
    }
}
