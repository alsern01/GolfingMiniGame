using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InputManager : MonoBehaviour
{
    public float angleToReach = 60f;
    public float minAngleOffset = 10f; // Ajusta según sea necesario
    private bool movementDone = false;

    private Vector3 accel = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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

            // Vector3 accel = Input.acceleration;

            accel.x++;
            accel.y++;
            accel.z++;

            // Invierte los ejes para tener en cuenta la orientación del dispositivo
            float variationX = Mathf.Atan2(accel.z, accel.y) * Mathf.Rad2Deg;

            // Calcula el porcentaje del movimiento
            float movementPercent = Mathf.Clamp01(Mathf.Abs(variationX) / angleToReach);

            // Actualiza la barra de UI
            UIManager.Instance.UpdateSlider(movementPercent);

            // Comprueba si la inclinación supera el umbral y la acción aún no se ha ejecutado
            if (movementPercent >= 1f && !movementDone)
            {
                movementDone = true;
            }
            else if (movementPercent < 1f && Mathf.Abs(variationX) < minAngleOffset)
            {
                // Reinicia la marca de la acción si la inclinación vuelve cerca de cero
                movementDone = false;
            }

            // Ejecuta la acción cuando el dispositivo vuelva a la posición inicial después de la inclinación
            if (movementDone && Mathf.Abs(variationX) < minAngleOffset)
            {
                // Ejecuta tu acción aquí
                DoSomething();
                movementDone = false;
            }

            //Debug.Log($"Orientatcion actual: {NetworkManager.Instance.getDeviceOrientation()}");
        }
    }

    void DoSomething()
    {
        // Coloca aquí la lógica de la acción que deseas ejecutar
        Debug.Log("¡Acción ejecutada!");
    }
}

