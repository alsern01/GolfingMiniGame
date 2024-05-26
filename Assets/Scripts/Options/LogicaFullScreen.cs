using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogicaFullScreen : MonoBehaviour
{
    public TMP_Dropdown resolutionDropDown;
    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        ReviewResolution();
    }

    public void ReviewResolution()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        int actuallyResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                actuallyResolution = i;
            }
        }

        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = actuallyResolution;
        resolutionDropDown.RefreshShownValue();

        resolutionDropDown.value = PlayerPrefs.GetInt("numberResolution", 0);

    }

    public void ChangeResolution(int indexResolution)
    {

        PlayerPrefs.SetInt("numberResolution", resolutionDropDown.value);
        Resolution resolution1 = resolutions[indexResolution];
        Screen.SetResolution(resolution1.width, resolution1.height, Screen.fullScreen);
    }

}

