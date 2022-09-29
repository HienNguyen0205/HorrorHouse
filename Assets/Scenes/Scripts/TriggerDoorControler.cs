using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorControler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int doorType;
    private bool isClose = true;
    private bool isNear = false;

    private void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.E))
        {
            DoorControl();
        }
    }
    
    private void DoorControl()
    {
        if (isClose)
        {
            switch (doorType)
            {
                case 1:
                    animator.Play("DoorOpen1", 0, 0.0f);
                    break;
                case 2:
                    animator.Play("DoorOpen2", 0, 0.0f);
                    break;
                case 3:
                    animator.Play("DoorOpen3", 0, 0.0f);
                    break;
                case 4:
                    animator.Play("DoorOpen4", 0, 0.0f);
                    break;
            }
            isClose = false;
        }
        else
        {
            switch (doorType)
            {
                case 1:
                    animator.Play("DoorClose1", 0, 0.0f);
                    break;
                case 2:
                    animator.Play("DoorClose2", 0, 0.0f);
                    break;
                case 3:
                    animator.Play("DoorClose3", 0, 0.0f);
                    break;
                case 4:
                    animator.Play("DoorClose4", 0, 0.0f);
                    break;
            }
            isClose = true;
        }
        
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