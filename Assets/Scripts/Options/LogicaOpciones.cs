using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaOpciones : MonoBehaviour
{
    [SerializeField] private ControllerOptions panelOptions;

    public void Start()
    {
        panelOptions = FindAnyObjectByType<ControllerOptions>();
    }

    public void ShowOptions()
    {
        panelOptions.pantallaOpciones.SetActive(true);

    }
}
