using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle volumeToggle;

    void Start()
    {
        if (PlayerPrefs.GetInt("FirstLoad") == 0)
        {
            //Initialize First Time Player Data
            PersistentData.Volume = volumeSlider.value;
            PlayerPrefs.SetInt("FirstLoad", 1);
        }

        if (PlayerPrefs.GetInt("Mute") == 0) //unmuted
        {
            volumeSlider.value = PersistentData.Volume;
            volumeToggle.isOn = true;

        }
        else //muted
        {
            volumeToggle.isOn = false;
            volumeSlider.value = volumeSlider.minValue;
        }
    }

    int firstUpdateCounter;
    void Update()
    {
        if (firstUpdateCounter < 1)
        {
            firstUpdateCounter++;
        }
    }

    public void SetVolume()
    {
        PersistentData.Volume = volumeSlider.value;
        if (volumeSlider.value > volumeSlider.minValue)
        {
            volumeToggle.isOn = true;
        }
        else
        {
            volumeToggle.isOn = false;
            PlayerPrefs.SetInt("Mute", 1);
            PersistentData.Volume = PlayerPrefs.GetFloat("TempVolume");
        }
    }

    public void Mute()
    {
        if (firstUpdateCounter != 0)
        {
            if (PlayerPrefs.GetInt("Mute") == 0) //mute
            {
                PlayerPrefs.SetFloat("TempVolume", volumeSlider.value);
                volumeSlider.value = volumeSlider.minValue;
                PersistentData.Volume = volumeSlider.minValue;
                PlayerPrefs.SetInt("Mute", 1);
            }
            else //unmute
            {
                PersistentData.Volume = volumeSlider.value = PlayerPrefs.GetFloat("TempVolume");
                PlayerPrefs.SetInt("Mute", 0);
            }
        }
    }
}
