using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowUITouch : MonoBehaviour
{
    public GameObject touchUI_1;
    public GameObject touchUI_2;
    private bool isNear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isNear || (isNear && Input.GetKey(KeyCode.F) && (TriggerDoorControler.lockDoor.ContainsKey(TriggerDoorControler.currDoor)
            && TriggerDoorControler.lockDoor[TriggerDoorControler.currDoor] == TriggerDoorControler.keyType)))
        {
            isNear = false;
            touchUI_1.SetActive(false);
            touchUI_2.SetActive(false);
        }
        else
        {
            touchUI_1.SetActive(true);
            touchUI_2.SetActive(true);
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
