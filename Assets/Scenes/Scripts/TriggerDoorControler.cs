using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine;

public class TriggerDoorControler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int doorType;
    [SerializeField] private new Collider collider;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    [SerializeField] private AudioSource sound;
    public static string keyType = "";
    public static string currDoor = "";
    private bool isClose = true;
    private bool isNear = false;
    private bool isActive = false;
    public static Dictionary<string, string> lockDoor = new Dictionary<string, string>() {
        {"Living_Door", "Key_LivingTable"},
        {"Rainer_Door", "Key_RainerTable"},
        {"Wanda_Door", "Key_WandaTable"},
        {"Final_Door", "Key_FinalTable"},
    };

    private void Update()
    {
        if (isNear && Input.GetKeyDown(KeyCode.F) && !isActive)
        {
            if (lockDoor.ContainsKey(collider.gameObject.name))
            {
                if(lockDoor[collider.gameObject.name] == keyType)
                {
                    DoorControl();
                    GameObject.Find("Key_Hand").SetActive(false);
                    keyType = "";
                    lockDoor.Remove(collider.gameObject.name); 
                }
            }
            else
            {
                DoorControl();
            }
        }
        checkAnimation();
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
                case 5:
                    animator.Play("DoorOpen5", 0, 0.0f);
                    break;
            }
            sound.PlayOneShot(openSound);
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
                case 5:
                    animator.Play("DoorClose5", 0, 0.0f);
                    break;
            }
            sound.PlayOneShot(closeSound);
            isClose = true;
        }
    }

    private void checkAnimation()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            collider.isTrigger = true;
            isActive = true;
        }
        else
        {
            collider.isTrigger = false;
            isActive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = true;
            currDoor = collider.gameObject.name;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            currDoor = "";
        }
    }
}