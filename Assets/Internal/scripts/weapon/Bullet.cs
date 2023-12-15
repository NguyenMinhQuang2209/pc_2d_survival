
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 0;
    [SerializeField] private LayerMask enemyMask;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialized(int damage, float speed, Vector3 shootDir, float bulletDelayTime)
    {
        this.damage = damage;
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        rb.AddForce(speed * shootDir, ForceMode2D.Force);
        Destroy(gameObject, bulletDelayTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        if (((1 << layer) & enemyMask) != 0)
        {
            if (collision.gameObject.TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
