using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    GameObject actualScene, player, uiObject;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Resume()
    {
        FindObjectOfType<TouchManager>().ClosePausePanel();
    }

    public void GotoOption()
    {
        actualScene = GameObject.FindWithTag("Everything");
        player = GameObject.FindWithTag("Player");
        uiObject = GameObject.FindWithTag("UI");

        StartCoroutine(WaitOptionToLoad(SceneManager.LoadSceneAsync("Option",LoadSceneMode.Additive)));
    }

    public void GoBackToActualScene()
    {
        actualScene.SetActive(true);
        player.SetActive(true);
        uiObject.SetActive(true);
        SceneManager.UnloadSceneAsync("Option");
    }

    IEnumerator WaitOptionToLoad(AsyncOperation async)
    {
        while (!async.isDone)
        {
            yield return null;
        }
        player.SetActive(false);
        uiObject.SetActive(false);
        actualScene.SetActive(false);

        GameObject.FindWithTag("Apply").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.FindWithTag("Apply").GetComponent<Button>().onClick.AddListener(() => { GameObject.Find("Canvas").GetComponent<Option>().Apply(); GoBackToActualScene(); });
    }

    public void GotoMenu()
    {
        Loader.get().Save();
        Destroy(gameObject);
        Destroy(GameObject.FindWithTag("Player").gameObject);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
