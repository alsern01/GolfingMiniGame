using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBombSpawner : MonoBehaviour
{
    public GameObject ball, bomb;

    // Start is called before the first frame update
    void Start()
    {   
        GenerateObject();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void GenerateObject()
    {
        // Spawnea pelota
        if (Random.Range(0, 10) > 8)
        {
            ball.transform.position = this.transform.position;
            Instantiate(ball);
        }
        else // Spawnea bomba
        {
            bomb.transform.position = this.transform.position;
            GameObject instance = Instantiate (bomb);
            HideBomb (instance);
        }
    }

    private void HideBomb(GameObject bomb)
    {
        Destroy(bomb, 5);
    }
}
