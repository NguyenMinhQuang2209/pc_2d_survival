using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject bigContainer;
    public Transform inventoryContent;
    public Transform storeContent;
    public static UIController instance;

    int currentStoreDay = -1;
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
        bigContainer.SetActive(false);
        ResetInventoryStoreItem();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CursorController.instance.SwitchCursor(MessageController.I_OPEN_INVENTORY, new() { bigContainer });
        }
        int currentDay = DayNightController.instance.GetDay();
        if (currentStoreDay != currentDay)
        {
            currentStoreDay = currentDay;
            ResetInventoryStoreItem();
        }
    }
    public void ResetInventoryStoreItem()
    {
        ClearContentItem(inventoryContent);
        ClearContentItem(storeContent);

        List<UpgradeItem> inventoryItems = UpgradeController.instance.GetListInventoryItem();
        foreach (var item in inventoryItems)
        {
            InventoryItem tempItem = Instantiate(item.item, inventoryContent);
            tempItem.MyInitialized(false, item);
        }


        List<UpgradeItem> storeItems = UpgradeController.instance.GetListStoreItemToday();
        foreach (var item in storeItems)
        {
            InventoryItem tempItem = Instantiate(item.item, storeContent);
            tempItem.MyInitialized(!item.MaxLevel(), item);
        }
    }
    private void ClearContentItem(Transform items)
    {
        foreach (Transform item in items)
        {
            Destroy(item.gameObject);
        }
    }
}
