using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    [SerializeField] private List<UpgradeItem> upgradeItems = new();
    public static UpgradeController instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
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
    public List<UpgradeItem> GetListStoreItem()
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
}
[System.Serializable]
public class UpgradeItem
{
    public InventoryItem item;
    private int level = 0;
    public UpdateName updateName;
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
    public void UpdateLevel()
    {
        wasBuy = true;
        if (!MaxLevel())
        {
            level += 1;
        }
    }

}
[System.Serializable]
public class UpgradeItemNextLevel
{
    public int price = 0;
    public float plusValue = 0f;
}