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
            Quaternion rotation = Quaternion.Euler(0, 0, i);
            Bullet tempBullet = Instantiate(bullet, transform.position, rotation);
            tempBullet.Initialized(damage, bulletSpeed, tempBullet.transform.up * 2f, bulletDelayTime);
        }
    }
}
