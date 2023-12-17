using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SumupController : MonoBehaviour
{
    public static SumupController instance;

    private int currentEnemyDied = 0;
    [SerializeField] private TextMeshProUGUI showEnemyTxt;
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
        showEnemyTxt.text = "Số quái đã giết: " + currentEnemyDied;
    }
    public void AddOne()
    {
        currentEnemyDied += 1;
        showEnemyTxt.text = "Số quái đã giết: " + currentEnemyDied;
    }
}
