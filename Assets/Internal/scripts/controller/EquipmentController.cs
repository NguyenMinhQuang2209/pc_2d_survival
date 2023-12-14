using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    public static EquipmentController instance;
    public Transform equipmentPosition;

    private GameObject currentGunItem = null;
    private GameObject currentToolItem = null;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void Equipment(GameObject newItem, EquipmentType type)
    {
        switch (type)
        {
            case EquipmentType.Gun:
                if (currentGunItem != null)
                {
                    Destroy(currentGunItem);
                }
                currentGunItem = Instantiate(newItem, equipmentPosition);
                break;
            case EquipmentType.Tool:
                if (currentToolItem != null)
                {
                    Destroy(currentToolItem);
                }
                currentToolItem = Instantiate(newItem, equipmentPosition);
                break;
        }
    }
}
