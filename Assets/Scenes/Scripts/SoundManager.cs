using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource menuAudioSource; 
    private float menuAudioVolume = 1;
    // Start is called before the first frame update
    void Start()
    {

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
}
