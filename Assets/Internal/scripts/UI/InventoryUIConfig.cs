
using UnityEngine;

public class InventoryUIConfig : UIConfigAbstract
{
    public GameObject inventoryContent;
    public GameObject storeContent;
    private void OnEnable()
    {
        inventoryContent.SetActive(true);
        storeContent.SetActive(false);
    }
    public override void SwitchItem(int pos)
    {
        base.SwitchItem(pos);
    }
}
