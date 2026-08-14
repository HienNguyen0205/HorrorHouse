using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowUIBook : MonoBehaviour
{
    [SerializeField] private GameObject touchUI_1;
    [SerializeField] private GameObject touchUI_2;
    private bool isNear;
    private bool isRead = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isNear)
        {
            if (touchUI_1 != null && touchUI_1.activeSelf) touchUI_1.SetActive(false);
            if (touchUI_2 != null && touchUI_2.activeSelf) touchUI_2.SetActive(false);
        }
        else
        {
            checkRead();
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
        if (Input.GetKeyDown(KeyCode.F))
        {
            isRead = !isRead;
        }
    }
    void checkRead()
    {
        if (isRead)
        {
            if (touchUI_2 != null && !touchUI_2.activeSelf) touchUI_2.SetActive(true);
            if (touchUI_1 != null && touchUI_1.activeSelf) touchUI_1.SetActive(false);
        }
        else
        {
            if (touchUI_2 != null && touchUI_2.activeSelf) touchUI_2.SetActive(false);
            if (touchUI_1 != null && !touchUI_1.activeSelf) touchUI_1.SetActive(true);
        }
        InputKey();
    }
}
