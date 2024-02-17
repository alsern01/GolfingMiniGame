using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    public bool enPausa = false;
    public int numBalls = 0;
    public int maxBalls = 10;

    private int _score;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public bool ballCreated { get; set; }
    public bool ballHit { get; set; }
    public bool clientConnected { get; private set; }
    public bool playing { get; private set; }

    [SerializeField] private GameObject preparationCountdownTimer;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //Convertirlo a false
            clientConnected = false;
            playing = false;
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
        _scoreText.SetText($"Score: {_score}");
    }

    public int GetScore()
    {
        return _score;
    }

    public void ClientConnected()
    {
        clientConnected = true;
    }

    public void StartGame()
    {
        playing = true;
    }

    public void StopGame()
    {
        clientConnected = false;
        playing = false;
    }

}
