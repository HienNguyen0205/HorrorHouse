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
        float value = PlayerPrefs.GetFloat("VfxVolumn");
        Debug.Log(value);
        sliderText.text = value.ToString("0");
        slider.value = PlayerPrefs.GetFloat("VfxVolumn");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TextUpdate(float value)
    {
        value = value * 100;
        sliderText.text = value.ToString("0");
    }
}
