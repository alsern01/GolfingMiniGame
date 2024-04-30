using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class InputManager : MonoBehaviour
{
    private float initialAngle = float.NegativeInfinity;
    public float angleToReach = 0f;
    private Vector3 accel = Vector3.zero;
    private float timeBetweenSaves = 1f;
    private float minAngleOffset = 0.5f;

    public bool movementDone { private set; get; }



    private static InputManager instance;
    public static InputManager Instance
    {

        get { return instance; }
        private set
        {
            if (instance == null)
            {
                instance = value;
            }
            else if (instance != value)
            {
                Destroy(value);
            }
        }
    }

    [SerializeField] private Player player;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        angleToReach = GameManager.Instance.angle;
        movementDone = false;
    }

    void Update()
    {

        if (GameManager.Instance.playing && !GameManager.Instance.playerAnim)
        {  // Solo detecta el Input cuando haya un cliente conectado
            if (initialAngle < 0)
            {
                SetInitialInclination();
            }

            if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.enPausa && !GameManager.Instance.RoundFinished())
            {
                // golpear
                if (GameManager.Instance.ballCreated)
                {
                    //GameManager.Instance.numBallHit++;
                }
                PlayerMovement();
            }


            float currentInclination = CalculateInclination();

            if (timeBetweenSaves > 0)
                timeBetweenSaves -= Time.deltaTime;
            else
            {
                float currentTime = Time.time - GameManager.Instance.gameStartTime;
                PlayerData.Instance().AddRawInput(currentTime, currentInclination);

                RawInputData rawInput = new RawInputData
                {
                    TimeStamp = currentTime,
                    Angle = currentInclination,
                };
                RealmController.Instance.AddRawInput(GameManager.Instance.PlayerId, rawInput);

                timeBetweenSaves = 1f;
            }

            float movementPercent = Mathf.Clamp01((currentInclination - initialAngle) / (angleToReach + initialAngle));
            UIManager.Instance.UpdateSlider(movementPercent);

            if (currentInclination >= angleToReach + initialAngle && !movementDone)
            {
                Debug.Log($"INPUT MANAGER: Inclination angle -> {currentInclination}");
                movementDone = true;
                Debug.Log("INPUT MANAGER: Angulo maximo alcanzado");
            }


            if (movementDone && currentInclination <= initialAngle + minAngleOffset)
            {
                movementDone = false;
                Debug.Log("INPUT MANAGER: Vuelta a la posicion inicial");
                PlayerMovement();
            }

        }
    }

    private float CalculateInclination()
    {
        Vector3 acceleration = accel;
        // Z - AXIS
        float inclinationRadians = Mathf.Atan2(acceleration.z, Mathf.Sqrt(acceleration.x * acceleration.x + acceleration.y * acceleration.y));
        // X - AXIS
        //float inclinationRadians = Mathf.Atan2(acceleration.x, Mathf.Sqrt(acceleration.z * acceleration.z + acceleration.y * acceleration.y)) 
        return Mathf.Abs(inclinationRadians * Mathf.Rad2Deg);
    }

    public void SetAccelVector(Vector3 orientation)
    {
        accel = orientation;
    }

    public void ResetMovement()
    {
        movementDone = false;
    }

    private void PlayerMovement()
    {
        GameManager.Instance.playerAnim = true;
        player.PerformMovement();
    }

    private void SetInitialInclination()
    {
        initialAngle = CalculateInclination();
    }
}

