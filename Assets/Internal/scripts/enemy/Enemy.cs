using UnityEngine;
using UnityEngine.AI;

public class Enemy : Health
{
    private NavMeshAgent agent;
    [SerializeField] private float speed = 1f;
    [SerializeField] private int damage = 0;

    Transform player;
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
    }
    private void Update()
    {
        if (player != null)
        {
            float check = player.position.x - transform.position.x;
            transform.rotation = Quaternion.Euler(0f, check < 0f ? 180f : 0f, 0f);
            agent.SetDestination(player.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.CompareTag(PlayerConfigController.PLAYER_TAG))
        {
            if (col.TryGetComponent<Health>(out var health))
            {
                health.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
