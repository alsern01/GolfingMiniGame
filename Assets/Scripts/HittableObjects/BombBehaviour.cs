using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("HideBomb", 3.0f);
    }

    private void HideBomb()
    {
        GetComponent<Animator>().enabled = true;
    }
}
