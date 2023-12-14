using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public Transform equipmentContainer;
    private List<EquipmentItem> items = new();
    public void Equipment(EquipmentItem item)
    {
        if (items != null)
        {
            GameObject tempObject = null;
            int pos = -1;
            for (int i = 0; i < items.Count; i++)
            {
                EquipmentItem equipment = items[i];
                if (equipment.itemName == item.itemName)
                {
                    pos = i;
                    tempObject = equipment.item;
                    break;
                }
            }
            if (pos != -1)
            {
                if (tempObject != null)
                {
                    Destroy(tempObject);
                }
                items.RemoveAt(pos);
            }
        }
        GameObject tempNewItem = Instantiate(item.item, equipmentContainer.transform);
        items.Add(new(tempNewItem, item.itemName));
    }
}
[System.Serializable]
public class EquipmentItem
{
    public GameObject item;
    public ItemName itemName;
    public EquipmentItem(GameObject item, ItemName itemName)
    {
        this.item = item;
        this.itemName = itemName;
    }
}
