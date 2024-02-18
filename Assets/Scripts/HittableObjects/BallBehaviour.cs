using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public int pointsGiven = 50;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.ballHit)
        {
            Destroy(this.gameObject);
            GameManager.Instance.AddPoints(pointsGiven);
            GameManager.Instance.ballHit = false;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.ballCreated = false;
    }
}
