using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class PlayerDatas{

    public int actualSavePoint; //Sauvegarde le poit de sauvegarde actuel

    public float playerLife; //Vie du joueur

    public List<int> items; // Id des items contenu dans l'inventaire
    public List<int> itemsAmount; // Nombre d'items de ce type dans l'inventaire
}
