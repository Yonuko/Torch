using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class KeysManager : MonoBehaviour {

    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    // Use this for initialization
    void Awake () {
        if (Loader.IsLoader())
        {
            Loader l = GameObject.FindWithTag("Loader").GetComponent<Loader>();
            keys = l.datas.keys;
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    public KeyCode GetButtonDown(string buttonName)
    {
        if (keys.ContainsKey(buttonName) == false)
        {
            Debug.LogError("Il n'existe pas de controle ayant pour nom : " + buttonName);
            return KeyCode.None;
        }

        return keys[buttonName];
    }

    public string[] GetButtonNames()
    {

        return keys.Keys.ToArray();
    }

    public void SetButtonForKey(string buttonName, KeyCode keyCode)
    {
        keys[buttonName] = keyCode;
    }

}
