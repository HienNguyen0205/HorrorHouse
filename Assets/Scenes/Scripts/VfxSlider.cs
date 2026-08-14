using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class VfxSlider : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public TextMeshProUGUI sliderText;
    // Start is called before the first frame update
    void Start()
    {
        float value = PlayerPrefs.GetFloat("VfxVolumn", 0.5f);
        if (sliderText != null) sliderText.text = (value * 100).ToString("0");
        if (slider != null) slider.value = value;
    }

    public void TextUpdate(float value)
    {
        if (sliderText != null) sliderText.text = (value * 100).ToString("0");
    }

    public void updateVfxVolumn(float value)
    {
        PlayerPrefs.SetFloat("VfxVolumn", value);
    }
}
