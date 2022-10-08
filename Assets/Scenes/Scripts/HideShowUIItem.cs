using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowUIItem : MonoBehaviour
{
    [SerializeField] private GameObject touchUI_1;
    private bool isNear;
    private bool isPickUp = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
            checkPickUp();
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
        if (Input.GetKeyDown(KeyCode.F) && isPickUp == false)
        {
            isPickUp = true;

        }
    }
    void checkPickUp()
    {
        if (isPickUp)
        {
            touchUI_1.SetActive(false);

        }
        else
        {
            touchUI_1.SetActive(true);
        }
        InputKey();
    }
}
