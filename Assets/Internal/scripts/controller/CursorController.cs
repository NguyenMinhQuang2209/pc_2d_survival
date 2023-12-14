using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController instance;

    private List<GameObject> currentCursorList = null;
    private string currentCursor = "";
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void SwitchCursor(string newCursor, List<GameObject> newList)
    {
        if (currentCursorList != null)
        {
            foreach (var item in currentCursorList)
            {
                item.SetActive(false);
            }
        }
        if (currentCursor == newCursor)
        {
            currentCursor = "";
            currentCursorList = null;
            return;
        }
        currentCursor = newCursor;
        currentCursorList = newList;
        if (currentCursorList != null)
        {
            foreach (var item in currentCursorList)
            {
                item.SetActive(true);
            }
        }
    }
    public string GetCurrentCursor()
    {
        return currentCursor;
    }
}
