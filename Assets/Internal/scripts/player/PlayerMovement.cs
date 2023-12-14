
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 1f;

    Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent<CharacterConfig>(out var config))
            {
                animator = config.GetAnimator();
                break;
            }
        }
    }

    private void Update()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            input.y = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            input.y = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            input.x = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            input.x = 1f;
        }

        Movement(input.normalized);
    }
    private void Movement(Vector2 input)
    {
        if (animator != null)
        {
            animator.SetFloat("Speed", input.magnitude >= 0.1f ? 1f : 0f);
        }
        if (input.magnitude >= 0.1f)
        {
            Vector3 newPos = transform.position + speed * Time.deltaTime * (Vector3)input.normalized;
            Vector3 newRot = new(0f, input.x < 0f ? 180f : 0f, 0f);
            transform.rotation = Quaternion.Euler(newRot);
            rb.MovePosition(newPos);
        }
    }
}
