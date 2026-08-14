using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject LoadingScreen;
    public GameObject MainMenuObj;
    public Animator animator;
    private int levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        if (animator != null) animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        LoadScene(levelToLoad);
    }

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
        if (animator != null) animator.SetTrigger("FadeOut");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        if (LoadingScreen != null) LoadingScreen.SetActive(true);
        if (MainMenuObj != null) MainMenuObj.SetActive(false);
        yield return new WaitForSeconds(2);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while (!operation.isDone)
        {
            yield return null;
        }

        if (operation.isDone)
        {
            if (MainMenuObj != null) MainMenuObj.SetActive(true);
            if (LoadingScreen != null) LoadingScreen.SetActive(false);
        }
    }
}
