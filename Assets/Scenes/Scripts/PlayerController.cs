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
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;

    void Awake()
    {
        lightTeaching.SetActive(true);
        audioSource = GetComponent<AudioSource>();
    }

    void LateUpdate()
    {
        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            MovePlayerCamera();
        }
    }

    void Update()
    {
        checkLightPressing();
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            audioSource.Play();
            MovePlayer();
            footStepControl();
        }
        else
        {
            audioSource.Stop();
        }
    }

    void MovePlayer()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Speed = 8;
            isRunning = true;
        }
        if(Input.GetKey(KeyCode.LeftShift) == false)
        {
            Speed = 5;
            isRunning = false;
        }
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);
    }

    void MovePlayerCamera()
    {
        xRot -= PlayerMouseInput.y * Sensitivity;
        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        if(xRot > -60f && xRot < 60f)
        {
            PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        }
    }

    void footStepControl()
    {
        if (audioSource.isPlaying == false)
        {
            if (isRunning)
            {
                audioSource.PlayOneShot(runSound, 0.7f);
            }
            else
            {
                audioSource.PlayOneShot(footStep, 0.5f);
            }
        }
    }
    void checkLightPressing()
    {
        if(Input.GetKey(KeyCode.E))
        {
            lightTeaching.SetActive(false);
        }
    }
}