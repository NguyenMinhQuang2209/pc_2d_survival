using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public static CoinController instance;
    [SerializeField] private TextMeshProUGUI coinTxt;
    [SerializeField] private int currentCoin = 0;
    [SerializeField] private Coin coin;
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
        UpdateCoin();
    }
    private void UpdateCoin()
    {
        coinTxt.text = currentCoin.ToString();
    }
    public void AddCoin(int v)
    {
        currentCoin += v;
        UpdateCoin();
    }
    public void MinusCoin(int v)
    {
        currentCoin -= v;
        UpdateCoin();
    }
    public void ClearCoin()
    {
        currentCoin = 0;
        UpdateCoin();
    }
    public int GetCurrentCoin()
    {
        return currentCoin;
    }
    public void SpawnCoinObject(Vector3 pos, int v)
    {
        Coin tempCoin = Instantiate(coin, pos, Quaternion.identity);
        tempCoin.InitCoin(v);
    }
}
