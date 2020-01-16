using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomisationUI : MonoBehaviour {

    public GameObject selectGender;

    public Slider tailleSlider;

    public GameObject malePlayer;
    public GameObject femalePlayer;
    public GameObject malePanel;
    public GameObject femalePanel;

    public GameObject generalPanel;
    public GameObject modelsPanel;
    public GameObject visagePanel;
    public GameObject colorPanel;
    public GameObject eyeColorPanel;

    public Image generalButton;
    public Image modelsButton;
    public Image visageButton;
    public Image colorButton;

    public Image femaleSkinActualColor;
    public Image femaleMouseActualColor;
    public Image femaleEyeActualColor;
    public Image femaleEyeRActualColor;
    public Image femaleEyeLActualColor;

    public Image maleSkinActualColor;
    public Image maleMouseActualColor;
    public Image maleEyeActualColor;

    public GameObject trollText;

    public Material maleEyeColor;
    public Material maleSkinColor;
    public Material maleMouseColor;

    public Material femaleEyeRColor;
    public Material femaleEyeLColor;
    public Material femaleSkinColor;
    public Material femaleMouseColor;

    public Dropdown femaleEyeNumber;

    public GameObject doubleEyesColorPicker;

    public int rotateSpeed;

    bool isFemale;
    bool asSelected;

    public Camera visageCam;
    public Camera mainCam;

    public enum CurrentPanel { General, Model, Visage, Colors};

    public CurrentPanel currentPanel;

	// Use this for initialization
	void Awake () {

        trollText.SetActive(false);

        selectGender.SetActive(true);

        if (File.Exists(Application.persistentDataPath + "/" + "Pseudo.save"))
        {
            CharacterCustomData datas = (CharacterCustomData)DataManager.Load("Pseudo.save");

            if (isFemale)
            {
                femaleEyeActualColor.transform.root.GetComponent<CUIColorPicker>().Color = new Color(datas.eyeRColorR, datas.eyeRColorG, datas.eyeRColorB, datas.eyeRColorA);

                femaleEyeRActualColor.transform.root.GetComponent<CUIColorPicker>().Color = new Color(datas.eyeRColorR, datas.eyeRColorG, datas.eyeRColorB, datas.eyeRColorA);

                femaleEyeLActualColor.transform.root.GetComponent<CUIColorPicker>().Color = new Color(datas.eyeLColorR, datas.eyeLColorG, datas.eyeLColorB, datas.eyeLColorA);

                femaleSkinActualColor.transform.root.GetComponent<CUIColorPicker>().Color = new Color(datas.skinColorR, datas.skinColorG, datas.skinColorB, datas.skinColorA);

                femaleMouseActualColor.transform.root.GetComponent<CUIColorPicker>().Color = new Color(datas.mouseColorR, datas.mouseColorG, datas.mouseColorB, datas.mouseColorA);
            }


        }
        else
        {
            Debug.LogError("File not found");
        }

        //Actualise les couleurs de chaque élements.
        GoToGeneral();
        GoToCouleurs();
        GoToVisage();
        GoToGeneral();

    }
	
	// Update is called once per frame
	void Update () {

        if (!asSelected)
        {
            femalePanel.SetActive(false);
            malePanel.SetActive(false);
            femalePlayer.SetActive(false);
            malePlayer.SetActive(false);
        }
        else
        {
            if (currentPanel == CurrentPanel.Visage || currentPanel == CurrentPanel.Colors)
            {
                visageCam.enabled = true;
                mainCam.enabled = false;
            }
            else
            {
                visageCam.enabled = false;
                mainCam.enabled = true;
            }

            if (isFemale)
            {

                femalePanel.SetActive(true);
                malePanel.SetActive(false);
                femalePlayer.SetActive(true);
                malePlayer.SetActive(false);

                if (femaleEyeNumber.value == 0)
                {
                    doubleEyesColorPicker.SetActive(true);
                    femaleEyeRColor.color = femaleEyeActualColor.color;
                    femaleEyeLColor.color = femaleEyeRColor.color;
                    femaleSkinColor.color = femaleSkinActualColor.color;
                    femaleMouseColor.color = femaleMouseActualColor.color;
                }
                else
                {
                    doubleEyesColorPicker.SetActive(false);
                    femaleEyeRColor.color = femaleEyeRActualColor.color;
                    femaleEyeLColor.color = femaleEyeLActualColor.color;
                    femaleSkinColor.color = femaleSkinActualColor.color;
                    femaleMouseColor.color = femaleMouseActualColor.color;
                }


                if (Input.GetKey(KeyCode.LeftAlt) || Input.GetMouseButton(1))
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    femalePlayer.transform.Rotate(0, -Input.GetAxis("Mouse X") * rotateSpeed, 0);
                }
                else
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
            else
            {
                malePanel.SetActive(true);
                femalePanel.SetActive(false);
                femalePlayer.SetActive(false);
                malePlayer.SetActive(true);

                maleEyeColor.color = maleEyeActualColor.color;
                maleSkinColor.color = maleSkinActualColor.color;
                maleMouseColor.color = maleMouseActualColor.color;

                if (Input.GetKey(KeyCode.LeftAlt) || Input.GetMouseButton(1))
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    malePlayer.transform.Rotate(0, -Input.GetAxis("Mouse X") * rotateSpeed, 0);
                }
                else
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }

    }

    //Boutton de remise a zero de toute les slider de la zone choisie
    public void ResetBoutton()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Slider"))
        {
            if (go.activeInHierarchy)
            {
                go.GetComponent<Slider>().value = 0;
            }
        }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("colorPicker"))
        {
            go.GetComponent<CUIColorPicker>().Color = go.GetComponent<CUIColorPicker>().actualColor;
        }
    }

    //Bouton pour Choisir le genre de notre perso
    public void ChooseGender(bool isNotMale)
    {
        isFemale = isNotMale;
        asSelected = true;
        selectGender.SetActive(false);
    }

    //Troll Button
    public void GenderFluid()
    {
        trollText.SetActive(true);
    }

    public void GoToGeneral()
    {
        currentPanel = CurrentPanel.General;
        generalButton.color = Color.green;
        modelsButton.color = Color.white;
        visageButton.color = Color.white;
        colorButton.color = Color.white;
        generalPanel.SetActive(true);
        modelsPanel.SetActive(false);
        visagePanel.SetActive(false);
        colorPanel.SetActive(false);
    }

    public void GoToModels()
    {
        currentPanel = CurrentPanel.Model;
        modelsButton.color = Color.green;
        generalButton.color = Color.white;
        visageButton.color = Color.white;
        colorButton.color = Color.white;
        generalPanel.SetActive(false);
        modelsPanel.SetActive(true);
        visagePanel.SetActive(false);
        colorPanel.SetActive(false);
    }

    public void GoToVisage()
    {
        currentPanel = CurrentPanel.Visage;
        visageButton.color = Color.green;
        generalButton.color = Color.white;
        modelsButton.color = Color.white;
        colorButton.color = Color.white;
        generalPanel.SetActive(false);
        modelsPanel.SetActive(false);
        visagePanel.SetActive(true);
        colorPanel.SetActive(false);
        eyeColorPanel.SetActive(false);
    }

    public void GoToCouleurs()
    {
        currentPanel = CurrentPanel.Colors;
        colorButton.color = Color.green;
        generalButton.color = Color.white;
        modelsButton.color = Color.white;
        visageButton.color = Color.white;
        generalPanel.SetActive(false);
        modelsPanel.SetActive(false);
        visagePanel.SetActive(false);
        colorPanel.SetActive(true);
    }

    public void GotoIndividualEyesColor()
    {
        currentPanel = CurrentPanel.Visage;
        colorButton.color = Color.green;
        generalButton.color = Color.white;
        modelsButton.color = Color.white;
        visageButton.color = Color.white;
        generalPanel.SetActive(false);
        modelsPanel.SetActive(false);
        visagePanel.SetActive(false);
        colorPanel.SetActive(false);
        eyeColorPanel.SetActive(true);
    }

    public void ChangeVisageCameraPosition()
    {

        transform.position = new Vector3(transform.position.x, 15.7f + ((tailleSlider.value / 100) * 4.3f), transform.position.z);
        
    }

    public void Save()
    {
        CharacterCustomData datas = new CharacterCustomData();

        if (isFemale)
        {
            //datas.blendShapes = femalePanel.GetComponent<CharacterCustomization>().blendShapeDataBase;

            datas.eyeLColorR = femaleEyeLColor.color.r;
            datas.eyeLColorG = femaleEyeLColor.color.g;
            datas.eyeLColorB = femaleEyeLColor.color.b;
            datas.eyeLColorA = femaleEyeLColor.color.a;

            datas.eyeRColorR = femaleEyeRColor.color.r;
            datas.eyeRColorG = femaleEyeRColor.color.g;
            datas.eyeRColorB = femaleEyeRColor.color.b;
            datas.eyeRColorA = femaleEyeRColor.color.a;

            datas.skinColorR = femaleSkinColor.color.r;
            datas.skinColorG = femaleSkinColor.color.g;
            datas.skinColorB = femaleSkinColor.color.b;
            datas.skinColorA = femaleSkinColor.color.a;

            datas.mouseColorR = femaleMouseColor.color.r;
            datas.mouseColorG = femaleMouseColor.color.g;
            datas.mouseColorB = femaleMouseColor.color.b;
            datas.mouseColorA = femaleMouseColor.color.a;

            DataManager.Save(datas, "Perso.save");
        }
        else
        {
            //A faire, sauvegarde de la creation d'un Homme
        }
    }

}
