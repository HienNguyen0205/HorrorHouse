using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BgVolumnSlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderText;
    // Start is called before the first frame update
    void Start()
    {
        float value = PlayerPrefs.GetFloat("BgVolumn");
        value = value * 100;
        sliderText.text = value.ToString("0");
        slider.value = PlayerPrefs.GetFloat("BgVolumn");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TextUpdate(float value){
        value = value * 100;
        sliderText.text = value.ToString("0");
    }

    public void updateBgVolumn(float value)
    {
        PlayerPrefs.SetFloat("BgVolumn", value);
    } 
}
