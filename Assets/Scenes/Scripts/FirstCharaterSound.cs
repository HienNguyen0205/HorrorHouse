using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCharaterSound : MonoBehaviour
{
    [SerializeField] private AudioClip firstVoice;

    [SerializeField] private AudioSource sound;
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
                Invoke("checkVoice",2);
                isTalk = true;
            }
            isClose = false;
        }
    }
    private void checkVoice() {
        sound.PlayOneShot(firstVoice);
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
