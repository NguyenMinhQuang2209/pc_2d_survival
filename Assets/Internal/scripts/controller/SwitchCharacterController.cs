using System.Collections.Generic;
using UnityEngine;

public class SwitchCharacterController : MonoBehaviour
{
    public static SwitchCharacterController instance;

    [SerializeField] private List<CharacterItem> characters = new();
    [SerializeField] private Transform characterShowContainer;
    [SerializeField] private SelectCharacterItem spawnItem;

    private List<SelectCharacterItem> items = new();
    int currentIndex = 0;
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
    private void Start()
    {
        ShowCharacterItem();
    }
    public void ShowCharacterItem()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            SelectCharacterItem tempItem = Instantiate(spawnItem, characterShowContainer);
            tempItem.InitSelect(false, characters[i].sprite, items == null ? 0 : items.Count);
            items.Add(tempItem);
        }
        items[currentIndex].Selected(true);
    }

    public CharacterConfig ChooseCharacter()
    {
        return characters[currentIndex].config;
    }
    public List<CharacterItem> GetAllCharacter()
    {
        return characters;
    }
    public void SwitchItem(int newIndex)
    {
        items[currentIndex].Selected(false);
        currentIndex = newIndex;
        items[currentIndex].Selected(true);
    }
}
[System.Serializable]
public class CharacterItem
{
    public CharacterConfig config;
    public Sprite sprite;
}