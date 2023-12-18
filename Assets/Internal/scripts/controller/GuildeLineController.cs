using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuildeLineController : MonoBehaviour
{
    public static GuildeLineController instance;
    public List<string> guildelineList = new();
    public TextMeshProUGUI txtShow;
    public TextMeshProUGUI nextBtnTxt;
    public GameObject guildeLineObject;
    int currentIndex = 0;
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
        guildeLineObject.SetActive(true);
        txtShow.text = guildelineList[currentIndex];
        Time.timeScale = 0f;
    }
    public void Next()
    {
        if (currentIndex < guildelineList.Count - 1)
        {
            currentIndex += 1;
            txtShow.text = guildelineList[currentIndex];

            if (currentIndex == guildelineList.Count - 1)
            {
                nextBtnTxt.text = "Sẵn sàng";
            }
            else
            {
                nextBtnTxt.text = "Tiếp tục";
            }
        }
        else
        {
            guildeLineObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    public void Previous()
    {
        nextBtnTxt.text = "Tiếp tục";
        if (currentIndex > 0)
        {
            currentIndex -= 1;
            txtShow.text = guildelineList[currentIndex];
        }
    }
}
