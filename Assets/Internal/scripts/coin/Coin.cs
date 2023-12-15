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
        float plusCoin = PlusCommonConfig.instance.GetPlusCommon(PlusCommonItem.Coin_Get);
        float coinCircle = PlusCommonConfig.instance.GetPlusCommon(PlusCommonItem.Coin_Circle);
        Vector2 defaultBoxSize = GetComponent<BoxCollider2D>().size;
        GetComponent<BoxCollider2D>().size = new(defaultBoxSize.x + coinCircle, defaultBoxSize.y + coinCircle);
        coinGet = (int)Mathf.Ceil(v * plusCoin);
    }
}
