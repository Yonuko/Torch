using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KeyBindingUI : MonoBehaviour {

    KeysManager keysManager;
    string[] buttonsName;

    string keyToRebind = null;

    public List<Button> keysButtons = new List<Button>();

    Dictionary<string, Text> buttonToLabel = new Dictionary<string, Text>();

    public Dropdown inputMode;

    public List<Sprite> xboxButtons = new List<Sprite>();
    public List<Sprite> playstationButtons = new List<Sprite>();

	// Use this for initialization
	void Start () {

        keysManager = GameObject.FindWithTag("KeyBind").GetComponent<KeysManager>();

        // Change the inputs cells size to be responsive
        transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<GridLayoutGroup>().cellSize = new Vector2(Screen.width * 0.18f, 40);

        // Fill the button list
        for (int i = 0; i < transform.GetChild(2).GetChild(0).GetChild(0).childCount; i++){
            if (transform.GetChild(2).GetChild(0).GetChild(0).GetChild(i).name.Contains("Key"))
            {
                keysButtons.Add(transform.GetChild(2).GetChild(0).GetChild(0).GetChild(i).GetComponent<Button>());
            }
        }

        buttonsName = keysManager.GetButtonNames();

        for (int i = 0; i < buttonsName.Length; i++)
        {
            string bn = buttonsName[i];
            buttonToLabel[bn] = keysButtons[i].transform.GetChild(0).GetComponent<Text>();
            keysButtons[i].onClick.AddListener( () => { StartRebindFor(bn); });
            buttonToLabel[bn].text = GameObject.FindWithTag("Loader").GetComponent<Loader>().datas.keys[bn].ToString();
        }

        if (Input.GetJoystickNames().Length > 0)
        {
            for (int i = 0; i < Input.GetJoystickNames().Length; i++)
            {
                Dropdown.OptionData option = new Dropdown.OptionData();
                option.text = Input.GetJoystickNames()[i];
                Debug.Log(option);
                Debug.Log(Input.GetJoystickNames().Length);
                inputMode.options.Add(option);
            }
        }
        else
        {
            inputMode.ClearOptions();
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = "Keyboard";
            inputMode.options.Add(option);
        }

    }

    // Update is called once per framed

    void Update() {

        if (keyToRebind != null)
        {
            if (Input.anyKeyDown)
            {
                Array kcs = Enum.GetValues( typeof(KeyCode) );

                foreach (KeyCode kc in kcs)
                {
                    if (Input.GetKeyDown(kc))
                    {
                        keysManager.SetButtonForKey(keyToRebind, kc);
                        if (inputMode.value == 0)
                        {
                            buttonToLabel[keyToRebind].text = kc.ToString();
                        }
                        else
                        {
                            if (0 == 0)
                            {
                                //Si il s'agit d'un manette de PS4
                            }
                            else
                            {
                                //Si il s'agit d'une manette xbox
                            }
                        }
                        keyToRebind = null;
                        GameObject.FindWithTag("Loader").GetComponent<Loader>().SaveKeys();
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
}
