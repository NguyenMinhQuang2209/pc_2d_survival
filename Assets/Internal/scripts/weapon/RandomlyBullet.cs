using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomlyBullet : MonoBehaviour
{
    Transform target = null;
    float speed = 0f;
    int damage = 0;
    [SerializeField] private LayerMask enemyMask;
    private void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }
    public void InitShoot(Transform target, float speed, int damage)
    {
        this.target = target;
        this.speed = speed;
        this.damage = damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target != null && collision.transform == target)
        {
            int layer = target.gameObject.layer;
            if (((1 << layer) & enemyMask) != 0)
            {
                if (target.gameObject.TryGetComponent<Health>(out var health))
                {
                    health.TakeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }
    }

}
