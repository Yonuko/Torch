using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class CharacterCustomData
{
    
    public Dictionary<string, BlendShape> blendShapes;

    public float eyeRColorR;
    public float eyeRColorG;
    public float eyeRColorB;
    public float eyeRColorA;

    public float eyeLColorR;
    public float eyeLColorG;
    public float eyeLColorB;
    public float eyeLColorA;

    public float skinColorR;
    public float skinColorG;
    public float skinColorB;
    public float skinColorA;

    public float mouseColorR;
    public float mouseColorG;
    public float mouseColorB;
    public float mouseColorA;

    public bool isFemale;

}
