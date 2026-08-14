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
        float value = PlayerPrefs.GetFloat("BgVolumn", 1f);
        if (sliderText != null) sliderText.text = (value * 100).ToString("0");
        if (slider != null) slider.value = value;
    }

    public void TextUpdate(float value){
        if (sliderText != null) sliderText.text = (value * 100).ToString("0");
    }

    public void updateBgVolumn(float value)
    {
        PlayerPrefs.SetFloat("BgVolumn", value);
    } 
}
