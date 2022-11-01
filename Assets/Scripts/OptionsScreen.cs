using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class OptionsScreen : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;
    public List<ResolutionOptions> resolutions = new List<ResolutionOptions>();
    private int selectedRes;
    public TMP_Text resolutionLabel;
    bool foundRes = false;
    public AudioMixer audMixer;
    public TMP_Text masterLabel, musicLabel, sfxLabel;   
    public Slider masterSlider, musicSlider, sfxSlider; 
    float vol = 0f;
    // Start is called before the first frame update
    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;
        if (QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }

        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
              foundRes = true;
              selectedRes = i;
              UpdateResLabel();
            }
        }
        if (foundRes == false)
        {
            ResolutionOptions newRes = new ResolutionOptions();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;
            resolutions.Add(newRes);
            selectedRes = resolutions.Count - 1;
            UpdateResLabel();
        }
        audMixer.GetFloat("MasterVol", out vol);
        masterSlider.value = vol;
        audMixer.GetFloat("MusicVol", out vol);
        musicSlider.value = vol;
        audMixer.GetFloat("SFXVol", out vol);
        sfxSlider.value = vol;
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResLeft()
        {
            //Prevent user from trying to select a resolution that doesn't exist.
            selectedRes--;
            if (selectedRes < 0)
            {
                selectedRes = 0;
            }
            UpdateResLabel();
        }

    public void ResRight()
        {
            //so user cannot go over how many resolutions actually exist inside the list.
            selectedRes++;
            if (selectedRes > resolutions.Count - 1)
            {
                selectedRes = resolutions.Count - 1;
            }
            UpdateResLabel();
        }
    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedRes].horizontal.ToString() + " x " + resolutions[selectedRes].vertical.ToString();
    }
    
    public void ApplyGraphics()
    {
        //Screen.fullScreen = fullscreenTog.isOn;
        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, fullscreenTog.isOn);
    }

    //functions below will allow slider to be updated properly inside options.
    public void setMasterVolume()
    {
        //have to add 80 because inside of volume mixer the min is -80, and max is 20 for volume
        //adding 80 will allow players to see values from 0-100
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        audMixer.SetFloat("MasterVol", masterSlider.value);
        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }
     public void setMusicVolume()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        audMixer.SetFloat("MusicVol", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }
     public void setSFXVolume()
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
        audMixer.SetFloat("SFXVol", sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }
  
}



[System.Serializable]
public class ResolutionOptions
{
    public int horizontal, vertical;
}
