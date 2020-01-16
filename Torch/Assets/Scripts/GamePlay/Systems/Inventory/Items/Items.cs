using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Items
{
    public string name;
    public int id;
    public enum Type
    {
        Armor,
        Weapon,
        Consomable
    };
    public Type type;

    public enum ConsomableType
    {
        healPotion,
        manaPotion,
        food,
        speedScript //plagier sur Aion xD
    }
    public ConsomableType consomableType;
    public int value;
    public int maxAmount = 1;

    public Sprite image;
    public GameObject gameObject;
}
