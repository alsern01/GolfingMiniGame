using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Start()
    {

    }

    void Update()
    {

    }

    public void PerformMovement()
    {
        if (GameManager.Instance.playerAnim) 
            animator.SetBool("AnimationTrigger", true);
        else animator.SetBool("AnimationTrigger", false);
    }
}
