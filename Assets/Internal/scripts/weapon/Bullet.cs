
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = 0;
    private float speed = 0f;
    [SerializeField] private LayerMask enemyMask;
    bool wasInit = false;
    private Rigidbody2D rb;
    Vector3 dir;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    /*private void Update()
    {
        if (wasInit)
        {
            rb.velocity = speed * Time.deltaTime * dir;
        }
    }*/
    public void Initialized(int damage, float speed, Vector3 shootDir, float bulletDelayTime)
    {
        this.damage = damage;
        this.speed = speed;
        dir = shootDir;
        wasInit = true;
        rb.AddForce(shootDir * speed * Time.deltaTime, ForceMode2D.Force);
        Destroy(gameObject, bulletDelayTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;
        if (((1 << enemyMask) & layer) != 0)
        {
            if (collision.gameObject.TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(damage);
            }
        }
    }
}
