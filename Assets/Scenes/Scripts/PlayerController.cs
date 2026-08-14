using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject lightTeaching;
    [SerializeField] private AudioClip footStep;
    [SerializeField] private AudioClip runSound;
    private AudioSource audioSource;
    private bool isRunning;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float Sensitivity = 2f;

    public bool IsHiding { get; set; } = false;
    public bool IsHoldingBreath { get; private set; } = false;
    public bool IsRunning => isRunning;

    private StaminaManager staminaManager;

    void Awake()
    {
        if (lightTeaching != null) lightTeaching.SetActive(true);
        audioSource = GetComponent<AudioSource>();
        if (PlayerBody == null) PlayerBody = GetComponent<Rigidbody>();
        if (PlayerBody != null)
        {
            PlayerBody.freezeRotation = true;
        }
    }

    void Start()
    {
        staminaManager = StaminaManager.Instance != null ? StaminaManager.Instance : FindObjectOfType<StaminaManager>();
    }

    void LateUpdate()
    {
        if (IsHiding) return;

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            MovePlayerCamera();
        }
    }

    void Update()
    {
        checkLightPressing();
        HandleBreathInput();

        if (IsHiding)
        {
            if (PlayerBody != null) PlayerBody.velocity = Vector3.zero;
            isRunning = false;
            return;
        }

        bool hasInput = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        if (hasInput)
        {
            PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            MovePlayer();
            footStepControl();
        }
        else
        {
            isRunning = false;
            if (staminaManager != null) staminaManager.RegenerateStamina(Time.deltaTime);
            if (PlayerBody != null) PlayerBody.velocity = new Vector3(0f, PlayerBody.velocity.y, 0f);
        }
    }

    void MovePlayer()
    {
        bool wantSprint = Input.GetKey(KeyCode.LeftShift);

        if (wantSprint && staminaManager != null && staminaManager.ConsumeStamina(Time.deltaTime))
        {
            Speed = 8f;
            isRunning = true;
        }
        else if (wantSprint && (staminaManager == null || staminaManager.IsExhausted))
        {
            Speed = 2.5f; // Slow down during exhaustion
            isRunning = false;
        }
        else
        {
            Speed = 5f;
            isRunning = false;
            if (staminaManager != null) staminaManager.RegenerateStamina(Time.deltaTime);
        }

        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        if (PlayerBody != null)
        {
            PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);
        }
    }

    void MovePlayerCamera()
    {
        if (PlayerCamera == null) return;
        xRot -= PlayerMouseInput.y * Sensitivity;
        xRot = Mathf.Clamp(xRot, -60f, 60f);
        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }

    void footStepControl()
    {
        if (audioSource == null) return;
        if (audioSource.isPlaying == false)
        {
            if (isRunning)
            {
                if (runSound != null) audioSource.PlayOneShot(runSound, 0.7f);
            }
            else
            {
                if (footStep != null) audioSource.PlayOneShot(footStep, 0.5f);
            }
        }
    }

    void checkLightPressing()
    {
        if (Input.GetKey(KeyCode.E) && lightTeaching != null)
        {
            lightTeaching.SetActive(false);
        }
    }

    void HandleBreathInput()
    {
        IsHoldingBreath = Input.GetKey(KeyCode.Space);
    }
}