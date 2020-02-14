using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Option : MonoBehaviour {

    MusicManager musicManager;
    SoundManager soundManager;

    //Video Variables--------------------------------------------------------------
    [Header("Videos variables--------------------------------------")]
    public Text fpsCapValue;
    public Slider fpsSlider;
    public Toggle fpsShow;
    public Dropdown fullScreenDropDown, resolutionDropDown, qualityDropDown;

    public static int resolutionIndex, qualityIndex;

    private List<Dropdown.OptionData> resolutionList = new List<Dropdown.OptionData>();
    //Audios Variables---------------------------------------------------------------
    [Header("Audios variables--------------------------------------")]

    public Slider musicSlider;
    public Slider soundSlider;
    public Text musicTextValue, soundTextValue;
    public Toggle musicActive, soundActive;

    int CountOfResolution;

    // Use this for initialization
    void Start () {
        resolutionDropDown.ClearOptions();

        for (int i = Screen.resolutions.Length - 1; i > -1; i--)
        {
            resolutionList.Add(new Dropdown.OptionData(Screen.resolutions[i].ToString()));
        }

        CountOfResolution = resolutionList.Count - 1;
        resolutionDropDown.AddOptions(resolutionList);

        if (Loader.IsLoader())
        {
            Loader l = Loader.get();
            l.Load();

            //Load VideosOption;
            resolutionIndex = l.datas.resolutionIndex;
            qualityIndex = l.datas.qualityIndex;
            fpsShow.isOn = l.datas.fpsEnabled;
            fpsSlider.value = l.datas.fpsCap;
            if (l.datas.fullScreenOn)
            {
                fullScreenDropDown.value = 0;
            }
            else
            {
                fullScreenDropDown.value = 1;
            }

            //Load AudioOption
            musicSlider.value = l.datas.musicVolume;
            musicActive.isOn = l.datas.musicActive;
            soundSlider.value = l.datas.soundVolume;
            soundActive.isOn = l.datas.soundActive;
            
        }
        qualityDropDown.value = qualityIndex;
        resolutionDropDown.value = resolutionIndex;

        musicManager = GameObject.FindWithTag("MusicHolder").GetComponent<MusicManager>();
        soundManager = GameObject.FindWithTag("SoundHolder").GetComponent<SoundManager>();

        ChangePanel(0);
    }
	
	// Update is called once per frame
	void Update () {

        //Videos Option---------------------------------------------------
        Menu.fpsEnable = fpsShow.isOn;

        qualityIndex = qualityDropDown.value;
        resolutionIndex = resolutionDropDown.value;

        if (fullScreenDropDown.value == 0)
        {
            resolutionDropDown.interactable = false;
        }
        else
        {
            resolutionDropDown.interactable = true;
        }

        Menu.fpsCapValue = (int)fpsSlider.value;

        fpsCapValue.text = ((int)fpsSlider.value).ToString();

        //Audio Options---------------------------------------------------
        if (musicManager != null)
        {
            musicManager.volume = musicSlider.value;
            musicManager.musicActive = musicActive.isOn;
            musicTextValue.text = ((int)(musicSlider.value * 100)).ToString();
        }

        if (soundManager != null)
        {
            soundManager.volume = soundSlider.value;
            soundManager.soundActive = soundActive.isOn;
            soundTextValue.text = ((int)(soundSlider.value * 100)).ToString();
        }


    }

    public void Apply()
    {
        if (!resolutionDropDown.interactable)
        {
            Screen.fullScreen = true;
            Menu.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
            int index = CountOfResolution - resolutionDropDown.value;
            Screen.SetResolution(Screen.resolutions[index].width, Screen.resolutions[index].height, false);
            Menu.fullScreen = false;
        }
        QualitySettings.SetQualityLevel(qualityDropDown.value);
        GameObject.Find("Loader").GetComponent<Loader>().Save();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ChangePanel(int PanelID)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i == PanelID)
            {
                GameObject.Find("Main").transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                GameObject.Find("Main").transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
