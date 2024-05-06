using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            animator.enabled = true;
            GameManager.Instance.AddPoints(pointsGiven);
            GameManager.Instance.ballHit = false;
            UIManager.Instance.ChangeHitImageSprite(GameManager.Instance.numBallHit - 1, pointsGiven < 0 ? true : false);

            if (pointsGiven > 0)
            {
                GameManager.Instance.TotalBallHit++;
            }
            else
            {
                GameManager.Instance.TotalBombHit++;
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.playerAnim = false;
        GameManager.Instance.ballCreated = false;
    }
}
