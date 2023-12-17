using UnityEngine;
using UnityEngine.AI;

public class Enemy : Health
{
    private NavMeshAgent agent;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int damage = 0;
    [SerializeField] private int coinGet = 1;
    [SerializeField] private float damageBwtTime = 1f;
    float currentDamageBwtTime = 0f;

    Transform player;
    Animator animator;


    private void Start()
    {
        MyInitialised();
        if (TryGetComponent<NavMeshAgent>(out agent))
        {
            agent.speed = speed;
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
        player = GameObject.FindGameObjectWithTag(PlayerConfigController.PLAYER_TAG).transform;
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        currentDamageBwtTime += Time.deltaTime;
        if (player != null)
        {
            float check = player.position.x - transform.position.x;
            transform.rotation = Quaternion.Euler(0f, check < 0f ? 180f : 0f, 0f);
            if (agent != null)
            {
                agent.SetDestination(player.position);
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (currentDamageBwtTime >= damageBwtTime)
        {
            GameObject col = collision.gameObject;
            if (col.CompareTag(PlayerConfigController.PLAYER_TAG))
            {
                if (col.TryGetComponent<Health>(out var health))
                {
                    health.TakeDamage(damage);
                }
            }
            currentDamageBwtTime = 0f;
        }
    }
    public override void ObjectDie()
    {
        CoinController.instance.SpawnCoinObject(transform.position, coinGet);
        base.ObjectDie();
    }
    public override void TakeDamage(int damage)
    {
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }
        base.TakeDamage(damage);
    }
}
