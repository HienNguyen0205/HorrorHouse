using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderChange : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderText;
    // Start is called before the first frame update
    void Start()
    {
        sliderText.text = "100";
        slider.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TextUpdate(float value){
        value = value * 100;
        sliderText.text = value.ToString("0");
    }
}
