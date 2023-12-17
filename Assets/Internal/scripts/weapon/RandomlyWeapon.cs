using UnityEngine;

public class RandomlyWeapon : Weapon
{
    float currentTimeBwtAttack = 0f;
    [Space(10)]
    [Header("Track radious")]
    [SerializeField] private RandomlyBullet newBullet;
    [SerializeField] private float trackRadious = 2f;
    [SerializeField] private LayerMask enemymask;

    private void Update()
    {
        currentTimeBwtAttack += Time.deltaTime;
        if (currentTimeBwtAttack >= GetTimeBwtAttack())
        {
            currentTimeBwtAttack = 0f;
            Shoot();
        }
    }
    public override void Shoot()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, trackRadious, enemymask);
        if (hit != null)
        {
            RandomlyBullet tempBullet = Instantiate(newBullet, transform.position, Quaternion.identity);
            tempBullet.InitShoot(hit.transform, GetBulletSpeed(), GetDamage());
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, trackRadious);
    }
}
