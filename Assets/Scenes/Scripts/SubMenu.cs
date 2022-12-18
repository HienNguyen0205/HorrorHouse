using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubMenu : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
        {
            quitGame();
        }

    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        LoadScene(levelToLoad);
    }

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
        animator.SetTrigger("FadeOut");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        yield return new WaitForSeconds(2);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while (!operation.isDone)
        {
            yield return null;
        }

    }
}
