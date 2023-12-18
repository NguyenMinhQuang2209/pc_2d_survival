using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private List<UpgradeItem> upgradeItems = new();
    public static UpgradeController instance;
    private Dictionary<string, float> plusStores = new();

    public event EventHandler OnBuyPlusItem;
    List<UpgradeItem> storeForToday = new();
    int currentStoreDay = -1;
    [SerializeField] private int showItemAmount = 3;
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
        ResetStoreItemPlus();
    }
    public void ResetStoreItem()
    {
        int currentDay = DayNightController.instance.GetDay();
        if (currentDay != currentStoreDay)
        {
            currentStoreDay = currentDay;
            List<UpgradeItem> temp = GetListStoreItem();
            if (temp != null && temp.Count <= showItemAmount)
            {
                storeForToday = temp;
            }
            else
            {
                storeForToday?.Clear();
                for (int i = 0; i < showItemAmount; i++)
                {
                    int randomIndex = UnityEngine.Random.Range(0, temp.Count);
                    storeForToday.Add(temp[randomIndex]);
                }
            }
        }
    }
    public void ResetStoreItemPlus()
    {
        List<UpgradeItem> inventoryItems = GetListInventoryItem();
        plusStores?.Clear();
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            UpgradeItem tempItem = inventoryItems[i];
            for (int j = 0; j < tempItem.GetLevel(); j++)
            {
                UpgradeItemNextLevel item = tempItem.levels[j];
                string key = tempItem.itemName.ToString() + item.plusType;
                plusStores[key] = plusStores.ContainsKey(key) ? plusStores[key] + item.plusValue : item.plusValue;
            }
        }
        OnBuyPlusItem?.Invoke(this, null);
    }
    public void WasBuyItem(UpgradeItem item)
    {
        return;
        if (storeForToday != null)
        {
            for (int i = 0; i < storeForToday.Count; i++)
            {
                if (storeForToday[i] == item)
                {
                    storeForToday.RemoveAt(i);
                    return;
                }
            }
        }
    }
    public List<UpgradeItem> GetListInventoryItem()
    {
        List<UpgradeItem> temp = new();
        if (upgradeItems != null)
        {
            foreach (var item in upgradeItems)
            {
                if (item.GetBuyState())
                {
                    temp.Add(item);
                }
            }
        }
        return temp;
    }
    private List<UpgradeItem> GetListStoreItem()
    {
        List<UpgradeItem> temp = new();
        if (upgradeItems != null)
        {
            foreach (var item in upgradeItems)
            {
                if (!item.GetBuyState())
                {
                    temp.Add(item);
                }
                else
                {
                    if (item.GetLevel() < item.levels.Count)
                    {
                        temp.Add(item);
                    }
                }
            }
        }
        return temp;
    }
    public List<UpgradeItem> GetListStoreItemToday()
    {
        ResetStoreItem();
        return storeForToday;
    }
    public float GetPlus(string key)
    {
        return plusStores.ContainsKey(key) ? plusStores[key] : 0;
    }
    public void AddingPlusItem(string key, float v)
    {
        plusStores[key] = plusStores.ContainsKey(key) ? plusStores[key] + v : v;
        OnBuyPlusItem?.Invoke(this, null);
    }
    public void AddingPlusItem()
    {
        ResetStoreItemPlus();
        OnBuyPlusItem?.Invoke(this, null);
    }
}
[System.Serializable]
public class UpgradeItem
{
    public bool useCustomDescription = false;
    public InventoryItem item;
    private int level = 0;
    public UpdateType itemType;
    public ItemName itemName;
    private bool wasBuy = false;
    public List<UpgradeItemNextLevel> levels = new();

    public bool MaxLevel()
    {
        return level == levels.Count;
    }
    public int GetPrice()
    {
        if (MaxLevel())
        {
            return -1;
        }
        return levels[level].price;
    }
    public int GetLevel()
    {
        return level;
    }
    public bool GetBuyState()
    {
        return wasBuy;
    }
    public string GetItemDescription()
    {
        if (MaxLevel())
        {
            return "";
        }
        return levels[level].description;
    }
    public void UpdateLevel()
    {
        wasBuy = true;

        if (!MaxLevel())
        {
            level += 1;
        }
        UpgradeController.instance.AddingPlusItem();
    }

}
[System.Serializable]
public class UpgradeItemNextLevel
{
    public int price = 0;
    public ItemPlusType plusType;
    public float plusValue = 0f;
    public string description = string.Empty;
}