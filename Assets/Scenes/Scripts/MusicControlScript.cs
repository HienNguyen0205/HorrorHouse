using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControlScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static MusicControlScript instance;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("VfxVolumn");
    }
    // Update is called once per frame
    private void Awake(){
        DontDestroyOnLoad(this.gameObject);
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
}
