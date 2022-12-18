using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSecretMeetingroom : MonoBehaviour
{
    [SerializeField] private Animator paintingAnimator;
    [SerializeField] private Animator door1;
    [SerializeField] private Animator door2;
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
        paintingAnimator.Play("RotatePainting", 0, 0.0f);
        yield return new WaitForSeconds(1.5f);
        door1.Play("OpenMeetingRoom1", 0, 0.0f);
        door2.Play("OpenMeetingRoom2", 0, 0.0f);
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