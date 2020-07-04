using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public bool[] allDebrisUnlocked;

    public PlayerData(RecipeController recipeController)
    {
        allDebrisUnlocked = new bool[26];

        allDebrisUnlocked = recipeController.recipeBookUnlocked;
    }

}
