using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundManager : MonoBehaviour
{
    public AudioSource menuAudioSource;
    public Slider vfxVolumnSlider;
    private float menuAudioVolume = 1;
    // Start is called before the first frame update
    void Start()
    {
        vfxVolumnSlider.value = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        menuAudioSource.volume = menuAudioVolume / 2;
    }

    public void ChangeVolumn(float volume)
    {
        menuAudioVolume = volume;
    }

    public void StoreVfxVolumnValue()
    {
        PlayerPrefs.SetFloat("VfxVolumn", vfxVolumnSlider.value);
    }
}
