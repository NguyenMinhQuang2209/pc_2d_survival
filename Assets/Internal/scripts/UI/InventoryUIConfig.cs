
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIConfig : UIConfigAbstract
{
    public GameObject inventoryContent;
    public GameObject storeContent;

    [Header("Config btn background")]
    public Sprite defaultSprite;
    public Sprite activeSprite;
    public List<Image> btn = new();
    private void OnEnable()
    {
        SwitchItem(0);
    }
    public override void SwitchItem(int pos)
    {
        for (int i = 0; i < btn.Count; i++)
        {
            btn[i].sprite = i == pos ? activeSprite : defaultSprite;
        }
        base.SwitchItem(pos);
    }
    public void BackToSelectScene()
    {
        if (GameSceneManager.instance != null)
        {
            GameSceneManager.instance.LoadNewScene(SceneName.SelectCharacter);
        }
    }
}
