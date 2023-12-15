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

    [Space(10)]
    [Header("For equipment only")]
    public GameObject equipmentObject;
    public QuickslotItem quickSlotItem;
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
            if (!upgradeItem.GetBuyState())
            {
                switch (upgradeItem.itemType)
                {
                    case UpdateType.Gun:
                        if (equipmentObject != null)
                        {
                            EquipmentController.instance.Equipment(equipmentObject, EquipmentType.Gun);
                        }
                        if (quickSlotItem != null)
                        {
                            QuickslotController.instance.AddingQuickslot(quickSlotItem, equipmentObject, EquipmentType.Gun);
                        }
                        break;
                    case UpdateType.Tool:
                        if (equipmentObject != null)
                        {
                            EquipmentController.instance.Equipment(equipmentObject, EquipmentType.Tool);
                        }
                        if (quickSlotItem != null)
                        {
                            QuickslotController.instance.AddingQuickslot(quickSlotItem, equipmentObject, EquipmentType.Tool);
                        }
                        break;
                    default:
                        break;
                }
            }
            upgradeItem.UpdateLevel();
            UIController.instance.ResetInventoryStoreItem();
        }
    }
}
