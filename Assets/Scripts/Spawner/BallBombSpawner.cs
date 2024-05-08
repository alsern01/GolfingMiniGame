using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBombSpawner : MonoBehaviour
{
    public GameObject ball, bomb;
    public MenuPausa menuP;
    private bool bombLastIntanstiated;

    // Start is called before the first frame update
    void Start()
    {
        bombLastIntanstiated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.clientConnected) // Solo empieza a generar objetos cuando haya un cliente conectado
        {
            if (!GameManager.Instance.ballCreated && !GameManager.Instance.RoundFinished())
            {
                Invoke("GenerateObject", 0.5f);
                GameManager.Instance.ballCreated = true;
            }
        }
    }

    private void GenerateObject()
    {
        if (Random.Range(0, 10) > 3)
        {
            GenerateBall();
        }
        else
        {
            if (!bombLastIntanstiated)
            {
                GenerateBomb();
            }
            else
            {
                GenerateBall();
            }
        }
    }

    // Spawnea pelota
    private void GenerateBall()
    {
        Instantiate(ball, this.transform.position, Quaternion.identity);
        bombLastIntanstiated = false;
    }

    // Spawnea bomba
    private void GenerateBomb()
    {
        Instantiate(bomb, this.transform.position, Quaternion.identity);
        bombLastIntanstiated = true;
    }

}
