using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBombSpawner : MonoBehaviour
{
    public GameObject ball, bomb;
    public MenuPausa menuP;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //En caso de que esté en pause no puedas interactuar
        if (menuP.enPausa)
        {
            return;
        }

        if (GameManager.Instance.clientConnected) // Solo empieza a generar objetos cuando haya un cliente conectado
        {
            if (!GameManager.Instance.ballCreated)
            {
                Invoke("GenerateObject", 1.0f);
                GameManager.Instance.ballCreated = true;
            }
        }
    }

    private void GenerateObject()
    {
        if (Random.Range(0.0f, 1.0f) > 0.2f)
        {
            // Spawnea pelota
            Instantiate(ball, this.transform.position, Quaternion.identity);
        }
        else
        {
            // Spawnea bomba
            GameObject instance = Instantiate(bomb, this.transform.position, Quaternion.identity);
        }
    }

}
