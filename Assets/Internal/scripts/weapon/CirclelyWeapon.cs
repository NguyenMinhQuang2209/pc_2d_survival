using UnityEngine;

public class CirclelyWeapon : Weapon
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
        for (int i = 0; i < 360f; i += (360 / bulletAmount))
        {
            Bullet tempBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            tempBullet.Initialized(damage, bulletSpeed, transform.forward * 1.5f,bulletDelayTime);
        }

    }
}
