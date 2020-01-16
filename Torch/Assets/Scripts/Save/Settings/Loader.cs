using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

    public const string saveFileName = "gameSave.save";

    static Loader Instance;

    public Datas datas;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this);

        if (Instance == null)
        {
            Instance = this;
            datas = new Datas();
        }
        else
        {
            Destroy(gameObject);
        }

        if (!File.Exists(Application.persistentDataPath + "/" + saveFileName))
        {
            DefaultDatas();
        }

        DefaultDatas();

        // Temp des tests
        if (SceneManager.GetActiveScene().name == "Scene de test")
        {
            DefaultDatas();
        }
	}
	
    void DefaultDatas()
    {
        datas.qualityIndex = 3;

        datas.fpsCap = 60;
        datas.fpsEnabled = false;
        datas.fullScreenOn = true;
        datas.musicActive = true;
        datas.soundActive = true;
        
        datas.musicVolume = 0.5f;
        datas.soundVolume = 0.5f;

        datas.keys["Jump"] = KeyCode.Space;
        datas.keys["Map"] = KeyCode.M;
        datas.keys["Escape"] = KeyCode.Escape;
        datas.keys["Sprint"] = KeyCode.LeftShift;
        datas.keys["Action"] = KeyCode.E;
        datas.keys["Inventory"] = KeyCode.I;
        datas.keys["Drop"] = KeyCode.P;
        DataManager.Save(datas, saveFileName);
        Load();
    }
	
	public void Save() {

        SaveOption();

        DataManager.Save(datas,saveFileName);

	}

    public void SaveKeys()
    {
        datas.keys = KeysManager.keys;
    }

    void SaveOption()
    {
        datas.fullScreenOn = Menu.fullScreen;
        datas.fpsCap = Menu.fpsCapValue;
        datas.qualityIndex = Option.qualityIndex;
        datas.resolutionIndex = Option.resolutionIndex;
        datas.fpsEnabled = Menu.fpsEnable;

        //SaveAudioOption
        MusicManager musicManager = GameObject.FindWithTag("MusicHolder").GetComponent<MusicManager>();
        SoundManager soundManager = GameObject.FindWithTag("SoundHolder").GetComponent<SoundManager>();

        datas.musicVolume = musicManager.volume;
        datas.musicActive = musicManager.musicActive;
        datas.soundActive = soundManager.soundActive;
        datas.soundVolume = soundManager.volume;
    }

    public void Load()
    {

        datas = (Datas)DataManager.Load(saveFileName);

    }

    public static bool IsLoader()
    {
        if (GameObject.FindWithTag("Loader") && File.Exists(Application.persistentDataPath + "/" + saveFileName))
        {
            return true;
        }
        else if(!File.Exists(Application.persistentDataPath + "/" + saveFileName))
        {
            Debug.LogError("File doesn't exist");
            return false;
        }
        else
        {
            Debug.LogError("Loader not found");
            return false;
        }
    }
}
