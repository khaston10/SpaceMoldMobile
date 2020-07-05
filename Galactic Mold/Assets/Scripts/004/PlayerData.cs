using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public bool[] allDebrisUnlocked;
    public bool[] componentsUnlocked;

    public PlayerData(RecipeController recipeController)
    {
        allDebrisUnlocked = new bool[26];
        componentsUnlocked = new bool[6];

        allDebrisUnlocked = recipeController.recipeBookUnlocked;
        componentsUnlocked = recipeController.recipeBookComponentsUnlocked;
    }

}
