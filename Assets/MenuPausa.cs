using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject cuadroPausa;

    public void PausarBoton()
    {
        GameManager.Instance.enPausa = true;
        cuadroPausa.SetActive(true);
        Time.timeScale = 0;
    }

    public void ContinuarBoton()
    {
        GameManager.Instance.enPausa = true;
        cuadroPausa.SetActive(false);
        Time.timeScale = 1;
    }
    public void OpcionesBoton()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    public void Salir()
    {
        /*UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();*/
        SceneManager.LoadScene("MenuScene");
    }
}
