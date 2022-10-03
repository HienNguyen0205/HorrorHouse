using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowUIBook : MonoBehaviour
{
    [SerializeField] private GameObject touchUI_1;
    [SerializeField] private GameObject touchUI_2;
    private bool isNear;
    // Start is called before the first frame update
    void Start()
    {
        touchUI_2.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isNear)
        {
            touchUI_1.SetActive(false);
            touchUI_2.SetActive(false);
        }
        else
        {
            if (Input.GetKey(KeyCode.F))
            {
                touchUI_2.SetActive(true);
            }
            else if(touchUI_2.activeSelf)
            {
                touchUI_1.SetActive(false);
            }
            else
            {
                touchUI_1.SetActive(true);
            }

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
}
