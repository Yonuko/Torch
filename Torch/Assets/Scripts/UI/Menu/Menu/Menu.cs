using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Menu : MonoBehaviour {

    public static bool fpsEnable, fullScreen;
    public static int fpsCapValue;

    public GameObject fpsObject;

    private void Awake()
    {
        if (GameObject.Find("Loader") && File.Exists(Application.persistentDataPath + "gameSave.save"))
        {
            Loader l = GameObject.Find("Loader").GetComponent<Loader>();
            l.Load();

            fpsCapValue = l.datas.fpsCap;
            fpsEnable = l.datas.fpsEnabled;

            if (!l.datas.fullScreenOn)
            {
                Resolution resolution = Screen.resolutions[l.datas.resolutionIndex];
                Screen.SetResolution(resolution.width,resolution.height, l.datas.fullScreenOn);
            }
            else
            {
                Screen.fullScreen = true;
            }
        }
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        if (fpsObject != null && Loader.IsLoader())
        {
            fpsObject.SetActive(GameObject.Find("Loader").GetComponent<Loader>().datas.fpsEnabled);
        }
        Application.targetFrameRate = GameObject.Find("Loader").GetComponent<Loader>().datas.fpsCap;

	}

    public void LaunchGame()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void Option()
    {
        SceneManager.LoadScene("OptionMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
