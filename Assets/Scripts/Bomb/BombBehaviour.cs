using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HideBomb();
    }

    private void HideBomb()
    {
        Destroy(this.gameObject, 3);
    }
}
