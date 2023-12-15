using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusCommonConfig : MonoBehaviour
{
    public static PlusCommonConfig instance;
    public PlusKey plusHealthKey;
    public PlusKey plusHealthRecoverKey;
    public PlusKey plusSpeedKey;
    public PlusKey plusDamageKey;
    public PlusKey plusTimeBwtAttackKey;
    public PlusKey plusCoinGetKey;
    public PlusKey plusCoinCircleKey;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public float GetPlusCommon(PlusCommonItem type)
    {
        switch (type)
        {
            case PlusCommonItem.Health:
                return UpgradeController.instance.GetPlus(plusHealthKey.GetKey());
            case PlusCommonItem.Speed:
                return UpgradeController.instance.GetPlus(plusSpeedKey.GetKey());
            case PlusCommonItem.Damage:
                return UpgradeController.instance.GetPlus(plusDamageKey.GetKey());
            case PlusCommonItem.TimeBwtAttack:
                return UpgradeController.instance.GetPlus(plusTimeBwtAttackKey.GetKey());
            case PlusCommonItem.Coin_Get:
                return UpgradeController.instance.GetPlus(plusCoinGetKey.GetKey());
            case PlusCommonItem.Coin_Circle:
                return UpgradeController.instance.GetPlus(plusCoinCircleKey.GetKey());
            default:
                break;
        }
        return 0;
    }
}
