using UnityEngine;
using UnityEngine.UI;

public class SelectCharacterItem : MonoBehaviour
{
    public GameObject selectArrow;
    public GameObject selectBg;
    public Image img;
    public Button btn;
    int pos = 0;
    private void Start()
    {
        if (TryGetComponent<Button>(out btn))
        {
            btn.onClick.AddListener(() =>
            {
                SwitchCharacterController.instance.SwitchItem(pos);
            });
        }
    }
    public void InitSelect(bool select, Sprite c_img, int pos)
    {
        img.sprite = c_img;
        this.pos = pos;
        selectArrow.SetActive(select);
        selectBg.SetActive(select);
    }
    public void Selected(bool v)
    {
        selectArrow.SetActive(v);
        selectBg.SetActive(v);
    }
}
