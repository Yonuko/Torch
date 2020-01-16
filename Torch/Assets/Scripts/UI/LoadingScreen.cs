using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

    public GameObject loadingScreen;

    public Image loadingBar;

    public Text loadingValueText;

    public string sceneToLoadName;

    public List<Sprite> loadingScreens = new List<Sprite>();

    Sprite currentSprite;

    public void LoadScene()
    {
        currentSprite = loadingScreens[Random.Range(0, loadingScreens.Count)];
        StartCoroutine(OnSceneLoading());
    }

    IEnumerator OnSceneLoading()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoadName);
        loadingScreen.SetActive(true);
        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / .9f);
            loadingScreen.transform.GetChild(1).GetComponent<Image>().sprite = currentSprite;
            loadingValueText.text = progress * 100f + "%";
            loadingBar.fillAmount = progress;
            yield return null;
        }
        loadingScreen.SetActive(false);
    }
}
