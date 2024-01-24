using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if(GameManager.Instance.clientConnected) { } // Solo detecta el Input cuando haya un cliente conectado
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // golpear
            if (GameManager.Instance.ballCreated)
            {
                Debug.Log("Pelota/bomba golpeada");
                GameManager.Instance.ballHit = true;
            }
        }
    }
}
