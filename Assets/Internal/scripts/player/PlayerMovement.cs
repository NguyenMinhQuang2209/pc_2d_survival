using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 1f;

    private float plusSpeed = 0f;

    Animator animator;

    Vector2 movement;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetCharacter();
        UpgradeController.instance.OnBuyPlusItem += OnChangePlusItemEvent;
        DontDestroyOnLoad(gameObject);
    }

    private void OnChangePlusItemEvent(object sender, EventArgs e)
    {
        plusSpeed = PlusCommonConfig.instance.GetPlusCommon(PlusCommonItem.Speed);
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (animator != null)
        {
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        if (movement.magnitude >= 0.1f)
        {
            Vector3 newRot = new(0f, movement.x < 0f ? 180f : 0f, 0f);
            transform.rotation = Quaternion.Euler(newRot);
            rb.MovePosition(rb.position + (speed + plusSpeed) * Time.fixedDeltaTime * movement.normalized);
        }
    }
    public void OnChooseCharacter(CharacterConfig character)
    {
        Instantiate(character, transform);
        ResetCharacter();
    }
    public void ResetCharacter()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent<CharacterConfig>(out var config))
            {
                animator = config.GetAnimator();
                break;
            }
        }
    }
}
