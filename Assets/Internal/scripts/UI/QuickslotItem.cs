using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickslotItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quickslotPosition;
    private GameObject equipmentItem;
    private EquipmentType type;
    Button btn;
    private void Start()
    {
        btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() =>
            {
                EquipmentItem();
            });
        }
    }
    public void UpdateQuickslotTxt(int pos, GameObject equipmentItem, EquipmentType type)
    {
        quickslotPosition.text = pos.ToString();
        this.equipmentItem = equipmentItem;
        this.type = type;
    }
    public void EquipmentItem()
    {
        if (equipmentItem != null)
        {
            EquipmentController.instance.Equipment(equipmentItem, type, true);
        }
    }
}
