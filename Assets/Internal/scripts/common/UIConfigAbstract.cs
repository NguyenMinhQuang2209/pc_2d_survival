using System.Collections.Generic;
using UnityEngine;

public abstract class UIConfigAbstract : MonoBehaviour
{
    public List<GameObject> uiConfigItems = null;
    public virtual void SwitchItem(int pos)
    {
        if (uiConfigItems != null && uiConfigItems.Count > pos)
        {
            foreach (var item in uiConfigItems)
            {
                item.SetActive(false);
            }
            uiConfigItems[pos].SetActive(true);
        }
    }
}
