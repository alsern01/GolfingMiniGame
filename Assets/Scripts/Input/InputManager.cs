using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputManager : MonoBehaviour
{
    public float angleToReach = 10f;
    public float minAngleOffset = 10f; // Ajusta según sea necesario
    private bool movementDone = false;
    private bool movementComplete = false;


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

            // Invierte los ejes para tener en cuenta la orientación del dispositivo
            float variationX = Mathf.Atan2(accel.z, accel.y) * Mathf.Rad2Deg;

            // Calcula el porcentaje del movimiento
            float movementPercent = Mathf.Clamp01(Mathf.Abs(variationX) / angleToReach);

            // Actualiza la barra de UI
            UIManager.Instance.UpdateSlider(movementPercent);

            // Comprueba si la inclinación supera el umbral y la acción aún no se ha ejecutado
            if (movementPercent >= 1f && !movementDone)
            {
                // Marca el movimiento como realizado solo si el dispositivo ha vuelto cerca de la posición inicial
                if (Mathf.Abs(variationX) < minAngleOffset)
                {
                    movementDone = true;
                    Debug.Log("¡Movimiento realizado!");
                }
            }
            else if (Mathf.Abs(variationX) <= minAngleOffset && movementDone && !movementComplete)
            {
                // Ejecuta tu acción aquí
                DoSomething();

                // Marca la acción como ejecutada para evitar repeticiones
                movementComplete = true;

                // Reinicia la marca de la acción si la inclinación vuelve cerca de cero
                movementDone = false;
            }

            if (!movementDone && movementComplete)
            {
                // Reinicia el estado de la acción si el movimiento no se ha completado
                movementComplete = false;
            }

        }
    }

    public void SetAccelVector(Vector3 orientation)
    {
        accel = orientation;
    }

    void DoSomething()
    {
        // Coloca aquí la lógica de la acción que deseas ejecutar
        Debug.Log("¡Acción ejecutada!");
    }
}

