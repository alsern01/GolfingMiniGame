using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodigoVolumen : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    public Toggle mute;
    public float lastVolume;
    //public Image imageUnMute;
    // Start is called before the first frame update
    void Start()
    {
        mute.isOn = false;
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        lastVolume = slider.value;
        RevisarMute();
    }

    public void ChangeSlider(float valor){
        lastVolume = sliderValue;
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = slider.value;
        RevisarMute();
    }

    public void RevisarMute()
    {
        if(sliderValue == 0)
        {
           mute.isOn = true;
        }
        else
        {
            mute.isOn = false;
        }
    }

    public void MuteHandler() {
        if(mute.isOn){
            //mutear
            slider.value = 0;
            AudioListener.volume = 0;
        }
        else{
            //desmutear
            sliderValue = lastVolume;
            slider.value = PlayerPrefs.GetFloat("volumenAudio", sliderValue);
            PlayerPrefs.SetFloat("volumenAudio", sliderValue);
            AudioListener.volume = sliderValue;
        }
    }
}
