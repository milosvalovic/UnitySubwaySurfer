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
}
