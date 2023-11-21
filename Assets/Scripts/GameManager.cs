using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager _instance { get; set; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
