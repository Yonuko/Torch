using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    static MusicManager Instance;

    AudioSource audioSource;

    public float volume;

    public bool musicActive;

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
            Loader l = GameObject.FindWithTag("Loader").GetComponent<Loader>();
            l.Load();
            musicActive = l.datas.musicActive;
            volume = l.datas.musicVolume;
        }
    }
	
	// Update is called once per frame
	void Update () {

        audioSource.enabled = musicActive;
        audioSource.volume = volume;

	}
}
