using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSecretDoor : MonoBehaviour
{
    [SerializeField] private Animator lightAnimator;
    [SerializeField] private Animator doorAnimator;
    private bool isClose = true;
    private bool isNear = false;

    private void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.F))
        {
            SecretDoorControl();
        }
    }

    private void SecretDoorControl()
    {
        
        if (isClose)
        {
            StartCoroutine(Wait());
            isClose = false;
        }
    }

    private IEnumerator Wait()
    {
        lightAnimator.Play("RotateLight", 0, 0.0f);
        yield return new WaitForSeconds(1.5f);
        doorAnimator.Play("OpenSecretDoor", 0, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
        }
    }
}