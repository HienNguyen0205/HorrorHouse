using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    [SerializeField] private AudioClip charVoice;

    [SerializeField] private AudioSource sound;

    [SerializeField] private int delay;
    private bool isClose = true;
    private bool isNear;
    private bool isTalk = false;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
           if(isNear && Input.GetKeyDown(KeyCode.F))
           {
                VoiceControl();
           }
    }
    private void VoiceControl() {
        if(isClose) {
            if(!isTalk)
            {
                Invoke("checkVoice",delay);
                isTalk = true;
            }
            isClose = false;
        }
    }
    private void checkVoice() {
        sound.PlayOneShot(charVoice);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
        }
    }
    
}
