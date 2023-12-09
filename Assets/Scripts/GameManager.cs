using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public bool BallCreated { get; set; }
    public bool BallHit { get; set; }

    private int _score;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPoints(int numPoints)
    {
        _score += numPoints;
    }

    public int GetScore()
    {
        return _score;
    }
}
