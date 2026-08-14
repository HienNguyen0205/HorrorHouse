using System.Collections;
using UnityEngine;

public class SanityManager : MonoBehaviour
{
    public static SanityManager Instance { get; private set; }

    [Header("Sanity Settings")]
    [SerializeField] private float maxSanity = 100f;
    [SerializeField] private float currentSanity = 100f;
    [SerializeField] private float darkDrainRate = 2f;
    [SerializeField] private float ghoulProximityDrainRate = 10f;
    [SerializeField] private float lightRecoveryRate = 4f;

    [Header("Audio Feedback")]
    [SerializeField] private AudioSource heartbeatAudio;
    [SerializeField] private AudioClip heartbeatClip;

    [Header("Visual Fear Feedback")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float maxShakeIntensity = 0.15f;

    private BossController ghoulBoss;
    private ElectricTorchOnOff flashlight;
    private Vector3 originalCamLocalPos;

    public float CurrentSanity => currentSanity;
    public float SanityPercent => currentSanity / maxSanity;
    public bool IsTerrified => currentSanity <= 30f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        currentSanity = maxSanity;

        if (heartbeatAudio == null) heartbeatAudio = GetComponent<AudioSource>();
        if (heartbeatAudio == null) heartbeatAudio = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        ghoulBoss = FindObjectOfType<BossController>();
        flashlight = FindObjectOfType<ElectricTorchOnOff>();

        if (playerCamera == null && Camera.main != null)
        {
            playerCamera = Camera.main.transform;
        }

        if (playerCamera != null)
        {
            originalCamLocalPos = playerCamera.localPosition;
        }
    }

    private void Update()
    {
        UpdateSanityState();
        UpdateHeartbeatAudio();
        UpdateCameraFearShake();
    }

    private void UpdateSanityState()
    {
        float ghoulDist = GetGhoulDistance();
        bool isFlashlightOn = flashlight != null && flashlight.IsFlashlightOn;

        if (ghoulDist < 18f)
        {
            float proxFactor = Mathf.Clamp01(1f - (ghoulDist / 18f));
            currentSanity -= ghoulProximityDrainRate * proxFactor * Time.deltaTime;
        }

        if (!isFlashlightOn)
        {
            currentSanity -= darkDrainRate * Time.deltaTime;
        }
        else if (ghoulDist >= 18f)
        {
            currentSanity += lightRecoveryRate * Time.deltaTime;
        }

        currentSanity = Mathf.Clamp(currentSanity, 0f, maxSanity);
    }

    private float GetGhoulDistance()
    {
        if (ghoulBoss == null) ghoulBoss = FindObjectOfType<BossController>();
        if (ghoulBoss != null)
        {
            return Vector3.Distance(transform.position, ghoulBoss.transform.position);
        }
        return 999f;
    }

    private void UpdateHeartbeatAudio()
    {
        if (heartbeatAudio == null) return;

        float ghoulDist = GetGhoulDistance();
        bool shouldPlay = ghoulDist < 20f || IsTerrified;

        if (shouldPlay)
        {
            if (!heartbeatAudio.isPlaying)
            {
                if (heartbeatClip != null) heartbeatAudio.clip = heartbeatClip;
                heartbeatAudio.loop = true;
                heartbeatAudio.Play();
            }

            float threatFactor = Mathf.Max(Mathf.Clamp01(1f - (ghoulDist / 20f)), (1f - SanityPercent));
            heartbeatAudio.volume = Mathf.Lerp(0.2f, 1.0f, threatFactor);
            heartbeatAudio.pitch = Mathf.Lerp(0.85f, 1.6f, threatFactor);
        }
        else if (heartbeatAudio.isPlaying)
        {
            heartbeatAudio.Stop();
        }
    }

    private void UpdateCameraFearShake()
    {
        if (playerCamera == null) return;

        if (IsTerrified || GetGhoulDistance() < 8f)
        {
            float intensity = Mathf.Lerp(0f, maxShakeIntensity, 1f - SanityPercent);
            Vector3 shakeOffset = Random.insideUnitSphere * intensity;
            shakeOffset.z = 0f;
            playerCamera.localPosition = originalCamLocalPos + shakeOffset;
        }
        else
        {
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, originalCamLocalPos, Time.deltaTime * 5f);
        }
    }
}
