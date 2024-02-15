using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputManager : MonoBehaviour
{
    public float minAngleOffset;

    private float angleToReach = 30f;
    private bool movementDone = false;
    public MenuPausa menuPausa;


    private Vector3 accel = Vector3.zero;

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

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        if(menuPausa != null && menuPausa.enPausa)
        {
            return;
        }

        if (GameManager.Instance.clientConnected)
        {  // Solo detecta el Input cuando haya un cliente conectado

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // golpear
                if (GameManager.Instance.ballCreated)
                {
                    Debug.Log("Pelota/bomba golpeada");
                    GameManager.Instance.ballHit = true;
                }
            }


            float currentInclination = CalculateInclination();
            float movementPercent = Mathf.Clamp01(currentInclination / angleToReach);
            UIManager.Instance.UpdateSlider(movementPercent);

            if (currentInclination >= angleToReach && !movementDone)
            {
                Debug.Log($"INPUT MANAGER: Inclination angle -> {currentInclination}");
                movementDone = true;
                Debug.Log("INPUT MANAGER: Angulo maximo alcanzado");
            }


            if (movementDone && currentInclination <= minAngleOffset)
            {
                movementDone = false;
                Debug.Log("INPUT MANAGER: Vuelta a la posicion inicial");
                DoSomething();
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

    private void DoSomething()
    {
        // golpear
        if (GameManager.Instance.ballCreated)
        {
            Debug.Log("Pelota/bomba golpeada");
            GameManager.Instance.ballHit = true;
        }
    }
}

