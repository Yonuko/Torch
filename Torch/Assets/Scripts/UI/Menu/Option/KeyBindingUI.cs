using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeyBindingUI : MonoBehaviour
{

    public GameObject keyContentObject, keyBindingPrefab;
    KeysManager keysManager;
    Loader loader;

    Dictionary<string, Text> buttonsName = new Dictionary<string, Text>();

    string keyToRebind = null;

    // Start is called before the first frame update
    void Start()
    {
        if (Loader.IsLoader())
        {
            loader = Loader.get();
        }

        keysManager = GetComponent<KeysManager>();

        foreach (string key in loader.datas.keys.Keys)
        {
            KeyCode bn = loader.datas.keys[key];
            GameObject go = Instantiate(keyBindingPrefab, transform.position, transform.rotation, keyContentObject.transform);
            go.transform.GetChild(0).GetComponent<Text>().text = key;
            go.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = bn.ToString();
            go.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { StartRebindFor(key); });
            buttonsName.Add(key, go.transform.GetChild(1).GetChild(0).GetComponent<Text>());
        }

    }

    void Update()
    {
        if (keyToRebind != null)
        {
            if (Input.anyKeyDown)
            {
                Array kcs = Enum.GetValues(typeof(KeyCode));

                foreach (KeyCode kc in kcs)
                {
                    if (Input.GetKeyDown(kc))
                    {
                        keysManager.SetButtonForKey(keyToRebind, kc);
                        UpdateKeyShowing(keyToRebind);
                        keyToRebind = null;
                        loader.SaveKeys();
                        break;
                    }
                }
            }
        }

    }

    void StartRebindFor(string buttonNames)
    {
        keyToRebind = buttonNames;
    }

    public void QuitOption()
    {
        loader.Save();
        SceneManager.LoadScene("Menu");
    }

    void UpdateKeyShowing(string temp)
    {
        foreach (string key in loader.datas.keys.Keys)
        {
            buttonsName[key].text = loader.datas.keys[key].ToString();
        }
    }
}
