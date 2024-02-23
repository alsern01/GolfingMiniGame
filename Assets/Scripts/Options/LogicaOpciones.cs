using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaOpciones : MonoBehaviour
{
    public ControllerOptions panelOptions;
    //public GameObject menuPausa;

    // Start is called before the first frame update
    void Start()
    {
        panelOptions = GameObject.FindGameObjectWithTag("Opciones").GetComponent<ControllerOptions>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowOptions()
    {
        //menuPausa.SetActive(false);
        panelOptions.pantallaOpciones.SetActive(true);
        
    }
}
