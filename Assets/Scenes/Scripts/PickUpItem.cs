using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] private GameObject visualItem1;
    [SerializeField] private GameObject visualItem2;
    [SerializeField] private GameObject platform;
    private bool isPickUp = false;
    // Start is called before the first frame update
    void Start()
    {
        visualItem1.SetActive(false);
        visualItem2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceGet = Vector3.Distance(transform.position, item.transform.position);
        float distancePut = Vector3.Distance(transform.position, platform.transform.position);
        if (distanceGet < 3.5f && TriggerDoorControler.keyType == "" && Input.GetKeyDown(KeyCode.F))
        {
            visualItem1.SetActive(true);
            item.SetActive(false);
            isPickUp = true;
        }
        if(isPickUp && distancePut < 3.5f && Input.GetKeyDown(KeyCode.F))
        {
            visualItem1.SetActive(false);
            visualItem2.SetActive(true);
            StartCoroutine(LoadSceneAsync(3));

        }
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
