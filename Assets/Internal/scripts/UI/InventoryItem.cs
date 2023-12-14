using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryItem : MonoBehaviour
{
    public Image itemImage;
    public TextMeshProUGUI itemLevel;
    public TextMeshProUGUI itemDescription;
    public Button buyItemBtn;
    public TextMeshProUGUI buytItemTxt;
    private UpgradeItem upgradeItem;
    private void Start()
    {
        buyItemBtn.onClick.AddListener(() =>
        {
            BuyItem();
        });
    }
    public void MyInitialized(bool wasStored, UpgradeItem upgradeItem)
    {
        this.upgradeItem = upgradeItem;
        string level = upgradeItem.GetLevel() < upgradeItem.levels.Count ? upgradeItem.GetLevel().ToString() : "Max";
        itemLevel.text = "Level:" + level;
        buytItemTxt.text = "Mua ( " + this.upgradeItem.GetPrice() + "c )";
        buyItemBtn.gameObject.SetActive(wasStored);
    }
    public void BuyItem()
    {
        if (upgradeItem != null)
        {
            upgradeItem.UpdateLevel();
            UIController.instance.ResetInventoryStoreItem();
        }
    }
}
