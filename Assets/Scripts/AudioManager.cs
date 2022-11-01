using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audMixer;
    
    //once the game starts up volume will get players preferred volume.
    void Start()
    {
        if(PlayerPrefs.HasKey("MasterVol"))
        {
            audMixer.SetFloat("MasterVol",PlayerPrefs.GetFloat("MasterVol"));
        }
        if(PlayerPrefs.HasKey("MusicVol"))
        {
            audMixer.SetFloat("MusicVol",PlayerPrefs.GetFloat("MusicVol"));
        }
        if(PlayerPrefs.HasKey("SFXVol"))
        {
            audMixer.SetFloat("SFXVol",PlayerPrefs.GetFloat("SFXVol"));
        }
    }
}
