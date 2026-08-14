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

	public LightChoose modoLightChoose;
	[Space]
	[Space]
	public bool _PowerPickUp = false;
	[Space]
	public float intensityLight = 2.5F;
	private bool _flashLightOn = false;

	private void Awake()
    {
		_batteryPower = FindObjectOfType<BatteryPowerPickup>();
	}
    void Start()
	{
		GameObject _scriptControllerEmissionFade = GameObject.Find("default");

		if (_scriptControllerEmissionFade != null)
		{
			_emissionMaterialFade = _scriptControllerEmissionFade.GetComponent<EmissionMaterialGlassTorchFadeOut>();
		}
		if (_scriptControllerEmissionFade  == null) {Debug.Log("Cannot find 'EmissionMaterialGlassTorchFadeOut' script");}
	}

	void Update()
	{
        switch (modoLightChoose)
        {
            case LightChoose.noBattery:
				NoBatteryLight();
                break;
        }
	}

	void InputKey()
    {
		if (Input.GetKeyDown(KeyCode.E) && _flashLightOn == true)
		{
			_flashLightOn = false;

		}
		else if (Input.GetKeyDown(KeyCode.E) && _flashLightOn == false)
		{
			_flashLightOn = true;

		}
	}

	void NoBatteryLight()
    {
		Light lightComp = GetComponent<Light>();
		if (_flashLightOn)
		{
			if (lightComp != null) lightComp.intensity = intensityLight;
			if (_emissionMaterialFade != null) _emissionMaterialFade.OnEmission();
		}
		else
		{
			if (lightComp != null) lightComp.intensity = 0.0f;
			if (_emissionMaterialFade != null) _emissionMaterialFade.OffEmission();
		}
		InputKey();
	}
}
