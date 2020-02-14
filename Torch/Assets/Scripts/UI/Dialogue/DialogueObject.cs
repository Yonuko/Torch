using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueObject : MonoBehaviour
{

    DialogueManager dialogueManager;

    public string npcName = "Sans Nom";
    public List<string> textLists = new List<string>();

    float timer = 0, desiredSpeed;

    int dialogueIndex = 0, letterIndex = 0;
    // This boolean is public because I need the escape button to disable the message only if this is false
    public bool dialogue;
    Loader loader;

    Coroutine currentCoroutine;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // If there is no text in the list add a default text
        if (textLists.Count == 0)
        {
            textLists.Add("Valeur par defaut");
        }

        // Get the script that contains all Dialiogue Constante
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        dialogueManager.dialogueBox.text = "";

        // Keep in memory the default speed
        desiredSpeed = dialogueManager.dialogueSpeed;

        loader = Loader.get();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Speed up the dialogue text while holding down a key
        if (Input.GetKey(loader.datas.keys["Action"]))
        {
            dialogueManager.dialogueSpeed = desiredSpeed / 5;
        }
        else
        {
            dialogueManager.dialogueSpeed = desiredSpeed;
        }

        // Show the dialogue box when speaking with a NPC
        dialogueManager.dialogueBox.transform.parent.gameObject.SetActive(dialogue);

        if (dialogue)
        {
            // If you press escape, end the dialogue window.
            if (Input.GetKeyDown(loader.datas.keys["Escape"]))
            {
                dialogue = false;
                GameObject.FindWithTag("Player").GetComponent<PlayerMouvement>().stopMoving = false;
                dialogueIndex = 0;
                letterIndex = 0;
                dialogueManager.dialogueBox.text = "";
                // Return to quit the Update function, so it dosn't try to access a dialogue window that doesn't exist anymore
                return;
            }

            // If you press the left click, skip the current box
            if (Input.GetMouseButtonDown(0))
            {
                dialogueManager.dialogueBox.text = textLists[dialogueIndex];
                letterIndex = textLists[dialogueIndex].Length;
            }

            // Launch the talking animation every 2 secondes
            if (currentCoroutine == null)
            {
                currentCoroutine = StartCoroutine(LaunchAnim());
            }

            // Disable the tips box while talking
            dialogueManager.tipsBox.gameObject.SetActive(false);

            // Show the NPC's name
            dialogueManager.npcNameBox.text = npcName;

            // Show the character with the disired time
            timer += Time.deltaTime;
            if (timer > dialogueManager.dialogueSpeed && letterIndex < textLists[dialogueIndex].Length)
            {
                dialogueManager.dialogueBox.text += textLists[dialogueIndex][letterIndex].ToString();
                letterIndex++;
                timer = 0;
            }

            // Block the player if the dialogue is open (temp)
            GameObject.FindWithTag("Player").GetComponent<PlayerMouvement>().stopMoving = true;

            if (Input.GetKeyDown(loader.datas.keys["Action"]))
            {
                // It's the last window, quit the dialogue 
                if (dialogueIndex == textLists.Count - 1 && letterIndex == textLists[dialogueIndex].Length)
                {
                    dialogue = false;
                    GameObject.FindWithTag("Player").GetComponent<PlayerMouvement>().stopMoving = false;
                    dialogueIndex = 0;
                    letterIndex = 0;
                    dialogueManager.dialogueBox.text = "";
                }
                // Dialogue is ended switch to the next page
                else if (letterIndex == textLists[dialogueIndex].Length)
                {
                    dialogueIndex++;
                    letterIndex = 0;
                    dialogueManager.dialogueBox.text = "";
                }
            }
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.tag == "Player")
        {
            dialogueManager.tipsBox.gameObject.SetActive(true);
            dialogueManager.tipsBox.text = "Appuier sur '" + loader.datas.keys["Action"] + "' pour parler";
        }
    }

    private void OnTriggerStay(Collider hit)
    {
        if (hit.tag == "Player" && Loader.IsLoader() && Input.GetKeyDown(loader.datas.keys["Action"]))
        {
            dialogue = true;
            // Disable everyDialogueNPCScript that aren't talking
            foreach (DialogueObject dialogueObject in FindObjectsOfType<DialogueObject>())
            {
                if (dialogueObject != this)
                {
                    dialogueObject.enabled = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.tag == "Player") {
            dialogueManager.tipsBox.gameObject.SetActive(false);
            dialogueManager.dialogueBox.transform.parent.gameObject.SetActive(false);
            // Enable everyDialogueNPCScript
            foreach (DialogueObject dialogueObject in FindObjectsOfType<DialogueObject>())
            {
                if (dialogueObject != this)
                {
                    dialogueObject.enabled = true;
                }
            }
        }
    }

    IEnumerator LaunchAnim()
    {
        anim.Play("Talking", 0, 0);
        yield return new WaitForSeconds(10);
        currentCoroutine = null;
    }
}
