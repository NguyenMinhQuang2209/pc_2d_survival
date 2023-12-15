using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class QuickslotController : MonoBehaviour
{
    public static QuickslotController instance;

    public Transform gunQuickslot;
    public Transform toolQuickslot;

    private List<QuickslotStoreItem> guns = new();
    private List<QuickslotStoreItem> tools = new();
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
        foreach (Transform child in gunQuickslot)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in toolQuickslot)
        {
            Destroy(child.gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (guns != null)
            {
                EquipmentController.instance.Equipment(guns[0].equipmentItem, EquipmentType.Gun, true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (guns != null && guns.Count > 1)
            {
                EquipmentController.instance.Equipment(guns[1].equipmentItem, EquipmentType.Gun, true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (guns != null && guns.Count > 2)
            {
                EquipmentController.instance.Equipment(guns[2].equipmentItem, EquipmentType.Gun, true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (tools != null)
            {
                EquipmentController.instance.Equipment(tools[0].equipmentItem, EquipmentType.Tool, true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (tools != null && tools.Count > 1)
            {
                EquipmentController.instance.Equipment(tools[1].equipmentItem, EquipmentType.Tool, true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (tools != null && tools.Count > 2)
            {
                EquipmentController.instance.Equipment(tools[2].equipmentItem, EquipmentType.Tool, true);
            }
        }
    }
    public void AddingQuickslot(QuickslotItem newQuickslot, GameObject equipmentItem, EquipmentType type)
    {
        switch (type)
        {
            case EquipmentType.Gun:
                QuickslotItem tempQuickslotItem = Instantiate(newQuickslot, gunQuickslot);
                tempQuickslotItem.UpdateQuickslotTxt(guns == null ? 1 : guns.Count + 1, equipmentItem, type);
                guns.Add(new(tempQuickslotItem, equipmentItem));
                break;

            case EquipmentType.Tool:
                QuickslotItem tempToolQuickslotItem = Instantiate(newQuickslot, toolQuickslot);
                tempToolQuickslotItem.UpdateQuickslotTxt(tools == null ? 4 : tools.Count + 4, equipmentItem, type);
                tools.Add(new(tempToolQuickslotItem, equipmentItem));
                break;
        }
    }

}
public class QuickslotStoreItem
{
    public QuickslotItem quickslot;
    public GameObject equipmentItem;

    public QuickslotStoreItem(QuickslotItem newQuickslot, GameObject equipmentItem)
    {
        quickslot = newQuickslot;
        this.equipmentItem = equipmentItem;
    }
}