using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public static StaminaManager Instance { get; private set; }

    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float staminaDrainRate = 25f;
    [SerializeField] private float staminaRegenRate = 18f;
    [SerializeField] private float exhaustionCooldown = 3f;

    [Header("UI Feedback")]
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Image staminaFillImage;

    [Header("Audio Feedback")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pantingSound;

    public float CurrentStamina { get; private set; }
    public bool IsExhausted { get; private set; }
    public bool IsPanting => CurrentStamina <= 25f || IsExhausted;

    private float cooldownTimer = 0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        CurrentStamina = maxStamina;
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        UpdateUI();
        HandlePantingSound();

        if (IsExhausted)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f && CurrentStamina >= 20f)
            {
                IsExhausted = false;
            }
        }
    }

    public bool ConsumeStamina(float deltaTime)
    {
        if (IsExhausted) return false;

        CurrentStamina -= staminaDrainRate * deltaTime;
        if (CurrentStamina <= 0f)
        {
            CurrentStamina = 0f;
            IsExhausted = true;
            cooldownTimer = exhaustionCooldown;
            return false;
        }

        return true;
    }

    public void RegenerateStamina(float deltaTime)
    {
        if (CurrentStamina < maxStamina)
        {
            CurrentStamina += staminaRegenRate * deltaTime;
            if (CurrentStamina > maxStamina) CurrentStamina = maxStamina;
        }
    }

    private void HandlePantingSound()
    {
        if (pantingSound == null || audioSource == null) return;

        if (IsPanting && !audioSource.isPlaying)
        {
            audioSource.clip = pantingSound;
            audioSource.loop = true;
            audioSource.volume = IsExhausted ? 0.8f : 0.4f;
            audioSource.Play();
        }
        else if (!IsPanting && audioSource.isPlaying && audioSource.clip == pantingSound)
        {
            audioSource.Stop();
        }
    }

    private void UpdateUI()
    {
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = CurrentStamina;
        }

        if (staminaFillImage != null)
        {
            staminaFillImage.color = IsExhausted ? Color.red : Color.Lerp(Color.yellow, Color.green, CurrentStamina / maxStamina);
        }
    }
}
