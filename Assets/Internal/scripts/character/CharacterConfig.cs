using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterConfig : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public Animator GetAnimator()
    {
        return animator;
    }
}
