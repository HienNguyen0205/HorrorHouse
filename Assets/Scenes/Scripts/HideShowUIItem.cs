using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowUIItem : MonoBehaviour
{
    [SerializeField] private GameObject touchUI_1;
    [SerializeField] private GameObject key_hand;
    [SerializeField] private GameObject key_table;
    [SerializeField] private AudioClip pickupKey;
    [SerializeField] private AudioSource sound;
    private bool isNear;
    private bool isPickUp = false;
    // Start is called before the first frame update
    void Start()
    {
        key_hand.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isNear)
        {
            touchUI_1.SetActive(false);
        }
        else
        {
            touchUI_1.SetActive(true);
            CheckPickUp();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isNear = true;

        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            isNear = false;
        }
    }
    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.F) && isPickUp == false && TriggerDoorControler.keyType == "")
        {
            sound.PlayOneShot(pickupKey);
            isPickUp = true;
            TriggerDoorControler.keyType = key_table.name;
            key_hand.SetActive(true);
            key_table.SetActive(false);
        }
    }
    void CheckPickUp()
    {
        if (isPickUp)
        {
            touchUI_1.SetActive(false);
        }
        else
        {
            touchUI_1.SetActive(true);
            InputKey();
        }
    }
}
