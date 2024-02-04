using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TMPro.EditorUtilities;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider movementSlider;
    [SerializeField] private GameObject connectionPanel;

    private static UIManager instance;
    public static UIManager Instance
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
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.clientConnected)
            connectionPanel.SetActive(false);
        else
            connectionPanel.SetActive(true);
    }

    public void UpdateSlider(float value)
    {
        movementSlider.value = value;

        if (value >= 1f)
        {
            movementSlider.fillRect.GetComponent<Image>().color = Color.green;
        }
        else
        {
            movementSlider.fillRect.GetComponent<Image>().color = Color.blue;
        }
    }
}
