using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowUIBook : MonoBehaviour
{
    public GameObject touchUI_1;
    private bool isNear;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isNear || (isNear && Input.GetKey(KeyCode.F)))
        {
            isNear = false;
            touchUI_1.SetActive(false);
        }
        else
        {
            touchUI_1.SetActive(true);
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
