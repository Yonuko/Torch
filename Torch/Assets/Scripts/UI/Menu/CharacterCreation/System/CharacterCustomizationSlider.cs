using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class CharacterCustomizationSlider : MonoBehaviour {

    //Pas besoin de suffix
    [Header("/!\\ Ne pas ajouter le suffix du nom du blendShape")]
    public string blendShapeName;
    Slider slider;

	// Use this for initialization
	void Start () {
        blendShapeName = blendShapeName.Trim();
        slider = GetComponent<Slider>();

        //Des que la valeur du slider change, appel une fonction par rapport au nom du blendShape, et donne la valeur du slider

        slider.onValueChanged.AddListener(value => CharacterCustomization.Instance.ChangeBlendShapeValue(blendShapeName, value));
	}
	
}
