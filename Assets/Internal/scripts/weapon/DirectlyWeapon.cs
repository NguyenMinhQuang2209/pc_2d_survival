using UnityEngine;

public class DirectlyWeapon : Weapon
{
    float currentTimeBwtAttack = 0f;
    private void Update()
    {
        currentTimeBwtAttack += Time.deltaTime;
        if (currentTimeBwtAttack >= timeBwtAttack)
        {
            currentTimeBwtAttack = 0f;
            Shoot();
        }
    }
    public override void Shoot()
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            Bullet tempBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            tempBullet.Initialized(damage, bulletSpeed, transform.forward * 1.5f, bulletDelayTime);
        }
    }
}
