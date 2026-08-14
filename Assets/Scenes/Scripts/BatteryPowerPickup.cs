using UnityEngine;

public class BatteryPowerPickup : MonoBehaviour
{
    ElectricTorchOnOff _torchOnOff;
    public float PowerIntensityLight;

    private void Awake()
    {
        _torchOnOff = FindObjectOfType<ElectricTorchOnOff>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            if (_torchOnOff != null)
            {
                _torchOnOff._PowerPickUp = true;
                _torchOnOff.intensityLight = PowerIntensityLight;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            if (_torchOnOff != null) _torchOnOff._PowerPickUp = false;
        }
    }
}
