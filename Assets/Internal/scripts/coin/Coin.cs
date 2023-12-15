using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int coinGet = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PlayerConfigController.PLAYER_TAG))
        {
            CoinController.instance.AddCoin(coinGet);
            Destroy(gameObject);
        }
    }
    public void InitCoin(int v)
    {
        coinGet = v;
    }
}
