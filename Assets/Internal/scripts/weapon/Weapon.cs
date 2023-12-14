
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Bullet bullet;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float timeBwtAttack = 1f;
    [SerializeField] protected int bulletAmount = 1;
    [SerializeField] protected float bulletSpeed = 1f;
    [SerializeField] protected float bulletDelayTime = 1f;
    public virtual void Shoot()
    {

    }
}
