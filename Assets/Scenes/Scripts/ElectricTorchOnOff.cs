using UnityEngine;

public class ElectricTorchOnOff : MonoBehaviour
{
    EmissionMaterialGlassTorchFadeOut _emissionMaterialFade;
    BatteryPowerPickup _batteryPower;

    public enum LightChoose
    {
        noBattery,
        withBattery
    }

    public enum TorchMode
    {
        Normal,
        UV
    }

    public LightChoose modoLightChoose;
    [Space]
    public bool _PowerPickUp = false;
    [Space]
    public float intensityLight = 2.5F;
    public float uvIntensityLight = 3.0F;

    private bool _flashLightOn = false;
    private TorchMode currentMode = TorchMode.Normal;
    private Light lightComp;

    private readonly Color normalColor = new Color(1.0f, 0.96f, 0.90f);
    private readonly Color uvColor = new Color(0.61f, 0.19f, 1.0f); // Purple / Violet

    public bool IsFlashlightOn => _flashLightOn;
    public bool IsUVMode => _flashLightOn && currentMode == TorchMode.UV;

    private void Awake()
    {
        _batteryPower = FindObjectOfType<BatteryPowerPickup>();
        lightComp = GetComponent<Light>();
    }

    void Start()
    {
        GameObject _scriptControllerEmissionFade = GameObject.Find("default");

        if (_scriptControllerEmissionFade != null)
        {
            _emissionMaterialFade = _scriptControllerEmissionFade.GetComponent<EmissionMaterialGlassTorchFadeOut>();
        }
        if (_emissionMaterialFade == null)
        {
            _emissionMaterialFade = FindObjectOfType<EmissionMaterialGlassTorchFadeOut>();
        }
    }

    void Update()
    {
        InputKey();

        switch (modoLightChoose)
        {
            case LightChoose.noBattery:
                NoBatteryLight();
                break;
        }

        if (IsUVMode)
        {
            CheckUVSecretRaycast();
        }
    }

    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _flashLightOn = !_flashLightOn;
        }

        if (Input.GetKeyDown(KeyCode.Q) && _flashLightOn)
        {
            currentMode = (currentMode == TorchMode.Normal) ? TorchMode.UV : TorchMode.Normal;
        }
    }

    void NoBatteryLight()
    {
        if (lightComp == null) lightComp = GetComponent<Light>();

        if (_flashLightOn)
        {
            if (lightComp != null)
            {
                if (currentMode == TorchMode.UV)
                {
                    lightComp.intensity = uvIntensityLight;
                    lightComp.color = uvColor;
                }
                else
                {
                    lightComp.intensity = intensityLight;
                    lightComp.color = normalColor;
                }
            }

            if (_emissionMaterialFade != null) _emissionMaterialFade.OnEmission();
        }
        else
        {
            if (lightComp != null) lightComp.intensity = 0.0f;
            if (_emissionMaterialFade != null) _emissionMaterialFade.OffEmission();
        }
    }

    private void CheckUVSecretRaycast()
    {
        Transform camTransform = Camera.main != null ? Camera.main.transform : transform;
        if (Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, 8.0f))
        {
            UVSecretDecal decal = hit.collider.GetComponent<UVSecretDecal>();
            if (decal != null)
            {
                decal.RevealDecal();
            }
        }
    }
}
