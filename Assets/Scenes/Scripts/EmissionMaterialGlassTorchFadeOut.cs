using UnityEngine;

public class EmissionMaterialGlassTorchFadeOut : MonoBehaviour
{
    private Renderer _mat;
    private Material _instancedMaterial;
    private float _intensity = 0;

    Color _alphaStart;

    ElectricTorchOnOff _torchOnOff;

    private void Start()
    {
        _mat = GetComponent<Renderer>();
        if (_mat != null)
        {
            _instancedMaterial = _mat.material;
            if (_instancedMaterial != null)
            {
                _alphaStart = _instancedMaterial.color;
            }
        }

        GameObject _torchLight = GameObject.Find("Torch Light");
        if (_torchLight == null) _torchLight = GameObject.FindWithTag("TorchLight");

        if (_torchLight != null)
        {
            _torchOnOff = _torchLight.GetComponent<ElectricTorchOnOff>();
        }

        if (_torchOnOff != null)
        {
            _intensity = _torchOnOff.intensityLight;
        }
    }

    private void Update()
    {
        if (_torchOnOff != null)
        {
            _intensity = _torchOnOff.intensityLight;
        }
    }

    public void TimeEmission(float t)
    {
        _intensity -= t * Time.deltaTime;
        if (_instancedMaterial != null)
        {
            _instancedMaterial.SetColor("_EmissionColor", _alphaStart * _intensity);
        }
        if (_intensity < 0)
        {
            _intensity = 0;
        }
    }

    public void OffEmission()
    {
        if (_instancedMaterial != null)
        {
            _instancedMaterial.SetColor("_EmissionColor", Color.black);
        }
    }    
    public void OnEmission()
    {
        if (_instancedMaterial != null)
        {
            _instancedMaterial.SetColor("_EmissionColor", _alphaStart * _intensity);
        }
    }
}
