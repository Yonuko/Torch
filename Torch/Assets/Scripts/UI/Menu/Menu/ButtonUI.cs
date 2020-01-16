using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ButtonUI : MonoBehaviour
{

    public Button newGame , continueButton;

    // Start is called before the first frame update
    void Start()
    {
        newGame.onClick.AddListener(() => { GameObject.FindWithTag("PlayerLoader").GetComponent<PlayerSaveLoader>().NewGameButton(); });
        continueButton.onClick.AddListener(() => { GameObject.FindWithTag("PlayerLoader").GetComponent<PlayerSaveLoader>().ContinueButton(); });

        continueButton.interactable = File.Exists(Application.persistentDataPath + "/" + "PlayerSave.Save");
    }

}
