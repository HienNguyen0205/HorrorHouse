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
            menuAudioVolume = PlayerPrefs.GetFloat("BgVolumn");

        }

        if (PlayerPrefs.HasKey("VfxVolumn"))
        {
            menuAudioVolume = PlayerPrefs.GetFloat("VfxVolumn");
        }
        else
        {
            PlayerPrefs.SetFloat("VfxVolumn", 0.5f);
            menuAudioVolume = PlayerPrefs.GetFloat("BgVolumn");

        }
    }

    // Update is called once per frame
    void Update()
    {
        menuAudioSource.volume = PlayerPrefs.GetFloat("BgVolumn");
    }

    public void ChangeVolumn(float volume)
    {
        menuAudioVolume = volume;
    }
}
