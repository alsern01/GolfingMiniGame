using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaEntreEscenas : MonoBehaviour
{

    private void Awake()
    {
        var objetos = FindObjectsOfType<LogicaEntreEscenas>();
        if(objetos.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
