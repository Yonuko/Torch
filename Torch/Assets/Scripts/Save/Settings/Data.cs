using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class Datas{

    public bool fullScreenOn;
    public bool fpsEnabled;

    public bool musicActive;
    public bool soundActive;
    public float musicVolume;
    public float soundVolume;

    public int fpsCap;
    public int resolutionIndex;
    public int qualityIndex;

    public Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
	
}
