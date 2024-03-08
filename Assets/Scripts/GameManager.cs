using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    #endregion

    #region GAME CONFIG VALUES
    public int numBallHit = 0;
    public int maxBalls = 10;
    #endregion

    #region GAME STATE VALUES
    private int _score;

    public bool ballCreated { get; set; }
    public bool ballHit { get; set; }
    public bool hitAnim { get; set; }
    public bool playerAnim { get; set; }
    #endregion

    #region GAME FLOW VARIABLES
    public bool clientConnected { get; set; }
    public bool playing { get; private set; }

    public bool enPausa = false;
    #endregion

    [SerializeField] private GameObject preparationCountdownTimer;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            playing = false;
            playerAnim = false;
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
        if (_score < 0)
        {
            _score = 0;
        }

        UIManager.Instance.UpdateScore();
    }

    public int GetScore()
    {
        return _score;
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

    private void HitAnimation()
    {
        ballHit = true;
        numBallHit++;
    }

    public bool RoundFinished()
    {
        return numBallHit >= maxBalls;
    }
}
