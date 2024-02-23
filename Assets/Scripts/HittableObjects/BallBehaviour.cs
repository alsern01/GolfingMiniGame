using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public int pointsGiven = 50;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.ballHit)
        {
            //Destroy(this.gameObject);
            animator.enabled = true;
            GameManager.Instance.AddPoints(pointsGiven);
            GameManager.Instance.ballHit = false;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.playerAnim = false;
        GameManager.Instance.ballCreated = false;
    }
}
