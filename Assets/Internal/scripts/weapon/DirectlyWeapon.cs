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
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePosition = Input.mousePosition;
        Vector3 dir = mousePosition - playerScreenPosition;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new(0f, 0f, angle));
    }
    public override void Shoot()
    {
        Vector3 shootDir = transform.right;
        for (int i = 0; i < bulletAmount; i++)
        {
            float angle = i == 0 ? 0 : (i % 2 == 0) ? (i - 1) * shootAngle : -i * shootAngle;

            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector3 offset = rotation * shootDir;
            Bullet tempBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
            tempBullet.Initialized(damage, bulletSpeed, offset, bulletDelayTime);

            float bulletAngle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
            tempBullet.transform.rotation = Quaternion.Euler(0, 0, bulletAngle + 270f);
        }
    }
}
