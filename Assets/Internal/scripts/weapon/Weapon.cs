
using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet bullet;
    [SerializeField] protected float shootAngle = 10f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float timeBwtAttack = 1f;
    [SerializeField] private int bulletAmount = 1;
    [SerializeField] private float bulletSpeed = 1f;
    [SerializeField] private float bulletDelayTime = 1f;
    [SerializeField] protected Transform shootPos;

    [Space(10)]
    [Header("Plus config")]
    [SerializeField] private ItemName weaponName;
    [SerializeField] private ItemPlusType plusBulletAmountKey = ItemPlusType.Value;
    [SerializeField] private ItemPlusType plusDamageKey = ItemPlusType.Damage;
    [SerializeField] private ItemPlusType plusTimeBwtAttackKey = ItemPlusType.TimeBwtAttack;
    [SerializeField] private ItemPlusType plusDelayDieTimeKey = ItemPlusType.DelayDieTime;
    [SerializeField] private ItemPlusType plusBulletSpeedKey = ItemPlusType.Speed;

    int plusDamage = 0;
    float reduceTimeBwtAttack = 0f;
    float plusBulletSpeed = 0;
    float plusBulletDelayTime = 0f;
    int plusBulletAmount = 0;


    private void Start()
    {
        UpgradeController.instance.OnBuyPlusItem += OnAddPlusEvent;
    }

    public int GetDamage()
    {
        return damage + plusDamage;
    }
    public float GetTimeBwtAttack()
    {
        return timeBwtAttack - reduceTimeBwtAttack;
    }
    public float GetBulletSpeed()
    {
        return bulletSpeed + plusBulletSpeed;
    }
    public float GetDelayDieTime()
    {
        return bulletDelayTime + plusBulletDelayTime;
    }
    public int GetBulletAmount()
    {
        return bulletAmount + plusBulletAmount;
    }

    private void OnAddPlusEvent(object sender, EventArgs e)
    {
        string damageKey = weaponName.ToString() + plusDamageKey;
        plusDamage = (int)UpgradeController.instance.GetPlus(damageKey);

        string reduceTimeBwtAttackKey = weaponName.ToString() + plusTimeBwtAttackKey;
        reduceTimeBwtAttack = UpgradeController.instance.GetPlus(reduceTimeBwtAttackKey);

        string bulletSpeedKey = weaponName.ToString() + plusBulletSpeedKey;
        plusBulletSpeed = UpgradeController.instance.GetPlus(bulletSpeedKey);

        string bulletDelayTimeKey = weaponName.ToString() + plusDelayDieTimeKey;
        plusBulletDelayTime = UpgradeController.instance.GetPlus(bulletDelayTimeKey);

        string bulletAmountKey = weaponName.ToString() + plusBulletAmountKey;
        plusBulletAmount = (int)UpgradeController.instance.GetPlus(bulletAmountKey);

    }

    public virtual void Shoot()
    {

    }
}
