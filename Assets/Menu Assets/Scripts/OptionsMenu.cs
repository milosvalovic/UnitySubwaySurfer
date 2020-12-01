using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMusicVolume (float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetSoundEffectVolume (float volume)
    {
        audioMixer.SetFloat("SoundEffects", volume);
    }
    
    public void setQuality (int qualityIndex)
    {
        if(qualityIndex == 0)
        {
            QualitySettings.SetQualityLevel(2);
        }else if(qualityIndex == 2) 
        {
            QualitySettings.SetQualityLevel(0);
        }else 
        {
            QualitySettings.SetQualityLevel(1);
        }
        
    }
}
