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

    void Start()
    {
        if (visualItem1 != null) visualItem1.SetActive(false);
        if (visualItem2 != null) visualItem2.SetActive(false);
    }

    void Update()
    {
        if (item == null || platform == null) return;
        float distanceGet = Vector3.Distance(transform.position, item.transform.position);
        float distancePut = Vector3.Distance(transform.position, platform.transform.position);

        if (distanceGet < 3.5f && TriggerDoorControler.keyType == "" && Input.GetKeyDown(KeyCode.F))
        {
            if (visualItem1 != null)
            {
                visualItem1.SetActive(true);
                Collider[] itemCols = visualItem1.GetComponentsInChildren<Collider>();
                foreach (Collider c in itemCols)
                {
                    c.enabled = false;
                }
            }
            if (item != null) item.SetActive(false);
            isPickUp = true;
        }

        if (isPickUp && distancePut < 3.5f && Input.GetKeyDown(KeyCode.F))
        {
            if (visualItem1 != null) visualItem1.SetActive(false);
            if (visualItem2 != null) visualItem2.SetActive(true);

            if (LoreCollector.Instance != null)
            {
                LoreCollector.Instance.EvaluateEnding();
            }

            StartCoroutine(LoadSceneAsync(3));
        }
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
