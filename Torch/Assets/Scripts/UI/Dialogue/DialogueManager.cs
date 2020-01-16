using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text dialogueBox, tipsBox, npcNameBox;

    public float dialogueSpeed = 0.05f;

    // Start is called before the first frame update
    void Awake()
    {
        if (tipsBox == null)
        {
            tipsBox = GameObject.Find("TipsBox").GetComponent<Text>();
            tipsBox.gameObject.SetActive(false);
        }
        if (npcNameBox == null)
        {
            npcNameBox = GameObject.Find("DialogueBox").transform.GetChild(0).GetComponent<Text>();
        }
        if (dialogueBox == null)
        {
            dialogueBox = GameObject.Find("DialogueBox").transform.GetChild(1).GetComponent<Text>();
        }
    }
}
