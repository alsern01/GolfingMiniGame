using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    #endregion

    #region GAME CONFIG VALUES
    public string PlayerId { get; set; }
    public int numBallHit { get; private set; } = 0;
    public int maxBalls { get; private set; } = 0;
    public float angle { get; private set; } = 0f;

    private int maxRounds;
    private int rounds = 0;
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
    public float gameStartTime { get; set; }
    #endregion


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

            playing = false;
            playerAnim = false;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
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

    public int GetRoundsLeft()
    {
        return maxRounds - rounds;
    }

    private void HitAnimation()
    {
        ballHit = true;
        numBallHit++;
    }

    public void StartRound()
    {
        numBallHit = 0;
        UIManager.Instance.ShowBallsToHit();
        UIManager.Instance.UpdateRoundsText();
        playing = true;
    }

    public void EndRound()
    {
        rounds++;
        playing = false;
        if (rounds < maxRounds)
        {
            Invoke("ResetUI", 2.0f);
        }
        else
        {
            EndGame();
        }
    }

    private void ResetUI()
    {
        UIManager.Instance.StartCountdown(2f);
        UIManager.Instance.ClearBallImages();
    }

    public bool RoundFinished()
    {
        return numBallHit >= maxBalls;
    }

    public void StopGame()
    {
        clientConnected = false;
        playing = false;
    }

    private void EndGame()
    {
        UIManager.Instance.ShowEndGamePanel();
        playing = false;
        PlayerData.Instance().totalScore = _score;
        PlayerData.Instance().gameTime = Time.time - gameStartTime;
        PlayerData.Instance().SaveData();

        RealmController.Instance.SetScore(PlayerId, _score);
        RealmController.Instance.SetGameTime(PlayerId, _score);
    }

    public void LoadConfig()
    {
        maxBalls = RealmController.Instance.GetRepsForPlayer(PlayerId);
        maxRounds = RealmController.Instance.GetSeriesForPlayer(PlayerId);
        angle = RealmController.Instance.GetAngleForPlayer(PlayerId);
    }
}
