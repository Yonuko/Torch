using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    static SoundManager Instance;

    AudioSource audioSource;

    public float volume;
    public bool soundActive;

    void Awake()
    {

        audioSource = GetComponent<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        if (Loader.IsLoader())
        {
            Loader l = Loader.get();
            l.Load();
            soundActive = l.datas.soundActive;
            volume = l.datas.soundVolume;
        }
    }
	
	// Update is called once per frame
	void Update () {

        audioSource.enabled = soundActive;
        audioSource.volume = volume;

	}
}
