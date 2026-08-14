using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundManager : MonoBehaviour
{
    private AudioSource menuAudioSource;
    private float menuAudioVolume = 1;
    // Start is called before the first frame update
    void Start()
    {
        menuAudioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("BgVolumn"))
        {
            menuAudioVolume = PlayerPrefs.GetFloat("BgVolumn");
        }
        else
        {
            PlayerPrefs.SetFloat("BgVolumn", 1f);
            menuAudioVolume = 1f;
        }

        if (!PlayerPrefs.HasKey("VfxVolumn"))
        {
            PlayerPrefs.SetFloat("VfxVolumn", 0.5f);
        }

        if (menuAudioSource != null)
        {
            menuAudioSource.volume = menuAudioVolume;
        }
    }

    public void ChangeVolumn(float volume)
    {
        menuAudioVolume = volume;
        if (menuAudioSource != null)
        {
            menuAudioSource.volume = volume;
        }
        PlayerPrefs.SetFloat("BgVolumn", volume);
    }
}
