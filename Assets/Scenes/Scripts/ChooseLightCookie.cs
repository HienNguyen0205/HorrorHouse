using System.Collections.Generic;
using UnityEngine;

public class ChooseLightCookie : MonoBehaviour
{
    private string chooseKeyForCookie = "Q";
    private KeyCode _keyCode;
    [Space]
    public List<Texture> lightCookie = new List<Texture>();
    private Light _thisLight;

    void Awake()
    {
        _thisLight = GetComponent<Light>();
    }

     void Start()
    {
        _keyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), chooseKeyForCookie);
        _thisLight.cookie = lightCookie[0];
    }
}
