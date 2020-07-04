using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeController : MonoBehaviour
{
    #region Variables Panels and Text
    public Text informationText;
    public GameObject[] recipePanels; // 0 - Mold, 1 - Debris, ... 
    #endregion

    #region Variables Stats
    public bool[] moldsUnlocked = new bool[4];
    public bool[] debrisUnlocked = new bool[11];
    public bool[] compoundDebrisUnlocked = new bool[11];
    public bool[] componentsUnlocked = new bool[6];
    // 0-10 Debris, 11-21 Compound Debris, 22-25 Mold;
    [SerializeField]
    public bool[] recipeBookUnlocked = new bool[26];
    public int difficulty; // 0 - Easy, 1 - Medium, 2 - Hard
    public bool resetRecipeBook;
    #endregion

    #region Variables Buttons & Images
    public Button[] moldButtons;
    public Button[] debrisButtons;
    public Button[] compositeButtons;
    public Button[] componentButtons;
    public Button[] allDebrisButtons;
    public Image[] allUnkownImages;
    public Image[] lockImages;
    public Image ingredientsImage1;
    public Image ingredientsImage2;
    public Sprite blackSprite;
    #endregion

    #region Variables Dictionary Of Materials
    Dictionary<string, string> Materials = new Dictionary<string, string>();
    public Sprite[] MaterialIcons;

    public string[] materialArrayForSearching = new string [26] { "COSMIC DUST","RADIATION", "STRANGE GOO", "ICE", "EMPTY ROCKET ENGINE", "BATTERY", "ANTENNA", "LENS",
        "SOLAR PANEL", "SATELLITE DISH", "PROPELLER", "RADIATED DUST", "ACID", "FROZEN GOO", "LASER", "MOUNTABLE SPIKE", "MOUNTABLE DISH", "MOUNTABLE LENS", "GLASSES", "RECHARGEABLE BATTERY",
        "ACTIVE ROCKET ENGINE", "MOUNTABLE PROPELLER", "SPACE MOLD", "HEAT RESISTANT MOLD", "ELECTRIC MOLD", "POISON MOLD" };

public string[,] materialsCombinationArray = new string[27, 27] {  
        { "NOT A VALID ENTERY",      "COSMIC DUST",   "RADIATION",    "STRANGE GOO",     "ICE",                 "EMPTY ROCKET ENGINE", "BATTERY",                 "ANTENNA",            "LENS",             "SOLAR PANEL",         "SATELLITE DISH", "PROPELLER",          "RADIATED DUST",          "ACID",                          "FROZEN GOO",               "LASER",           "MOUNTABLE SPIKE",    "MOUNTABLE DISH", "MOUNTABLE LENS", "GLASSES",              "RECHARGEABLE BATTERY", "ACTIVE ROCKET ENGINE", "MOUNTABLE PROPELLER", "SPACE MOLD",          "HEAT RESISTANT MOLD", "ELECTRIC MOLD",     "POISON MOLD" },
        { "COSMIC DUST",             "NA",            "RADIATED DUST","NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "NA",                "NA"},
        { "RADIATION",               "RADIATED DUST", "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "NA",                "NA"},
        { "STRANGE GOO",             "NA",            "NA",           "NA",              "FROZEN GOO",          "NA",                  "NA",                      "MOUNTABLE SPIKE",    "MOUNTABLE LENS",   "NA",                  "MOUNTABLE DISH", "NA",                 "ACID",                   "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "NA" ,               "NA"},
        { "ICE",                     "NA",            "NA",           "FROZEN GOO",      "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "NA" ,               "NA"},
        { "EMPTY ROCKET ENGINE",     "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "ACTIVE ROCKET ENGINE",   "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "NA" ,               "NA"},
        { "BATTERY",                 "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "RECHARGEABLE BATTERY","NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "NA" ,               "NA"},
        { "ANTENNA",                 "NA",            "NA",           "MOUNTABLE SPIKE", "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "NA" ,               "NA"},
        { "LENS",                    "NA",            "NA",           "MOUNTABLE LENS",  "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "LASER",                "NA",                   "NA",                  "NA",                  "NA",                  "NA" ,               "NA"},
        { "SOLAR PANEL",             "NA",            "NA",           "NA",              "NA",                  "NA",                  "RECHARGEABLE BATTERY",    "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "NA" ,               "NA"},
        { "SATELLITE DISH",          "NA",            "NA",           "MOUNTABLE DISH",  "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "NA" ,               "NA"},
        { "PROPELLER",               "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "MOUNTABLE PROPELLER","NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "NA" ,               "NA"},
        { "RADIATED DUST",           "NA",            "NA",           "ACID",            "NA",                  "ACTIVE ROCKET ENGINE","NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "NA" ,               "NA"},
        { "ACID",                    "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "POISON MOLD",         "NA",                  "NA" ,               "NA"},
        { "FROZEN GOO",              "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "HEAT RESISTANT MOLD", "NA",                  "NA" ,               "NA"},
        { "LASER",                   "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "ATTACK SYSTEM" ,    "NA"},
        { "MOUNTABLE SPIKE",         "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "MOUNTABLE PROPELLER","NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "NA" ,               "DEFENSE SYSTEM"},
        { "MOUNTABLE DISH",          "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "HEAT SHIELD",         "NA" ,               "NA"},
        { "MOUNTABLE LENS",          "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "GLASSES",        "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "NA" ,               "NA"},
        { "GLASSES",                 "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "COOL FACTOR",         "NA",                  "NA" ,               "NA"},
        { "RECHARGEABLE BATTERY",    "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "LASER",            "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "ELECTRIC MOLD",       "NA",                  "NA" ,               "NA"},
        { "ACTIVE ROCKET ENGINE",    "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "MOBILITY PACKAGE" , "NA"},
        { "MOUNTABLE PROPELLER",     "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "CLIMATE CONTROLLER",  "NA" ,               "NA"},
        { "SPACE MOLD",              "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "POISON MOLD",                   "HEAT RESISTANT MOLD",      "NA",              "NA",                 "NA",             "NA",             "COOL FACTOR",          "ELECTRIC MOLD",        "NA",                   "NA",                  "NA",                  "NA",                  "NA",                "NA" },
        { "HEAT RESISTANT MOLD",     "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "NA",                 "HEAT SHIELD",    "NA",             "NA",                   "NA",                   "NA",                   "CLIMATE CONTROLLER",  "NA",                  "NA",                  "NA" ,               "NA"},
        { "ELECTRIC MOLD",           "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "ATTACK SYSTEM",   "NA",                 "NA",             "NA",             "NA",                   "NA",                   "MOBILITY PACKAGE",     "NA",                  "NA",                  "NA",                  "NA" ,               "NA"},
        { "POISON MOLD",             "NA",            "NA",           "NA",              "NA",                  "NA",                  "NA",                      "NA",                 "NA",               "NA",                  "NA",             "NA",                 "NA",                     "NA",                            "NA",                       "NA",              "DEFENSE SYSTEM",     "NA",             "NA",             "NA",                   "NA",                   "NA",                   "NA",                  "NA",                  "NA",                  "NA" ,               "NA"} };


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Load();
        LoadDictionary();
        ClickPanel(0);

        // Check to see if the user selected to reset the recipe book,
        if (resetRecipeBook)
        {
            Debug.Log("Recipe Book Needs to be reset");
            ResetRecipeBook();
            SetButtonsActiveOnStart();
        }

        // ... otherwise, load Recipe Book Prefs from players previous play throughs.
        else
        {
            LoadRecipeBookPrefs();
            SetButtonsActiveOnStart();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load()
    {
        moldsUnlocked = GlobalController.Instance.moldsUnlocked;
        debrisUnlocked = GlobalController.Instance.debrisUnlocked;
        compoundDebrisUnlocked = GlobalController.Instance.compoundDebrisUnlocked;
        recipeBookUnlocked = GlobalController.Instance.allDebrisUnlocked;
        componentsUnlocked = GlobalController.Instance.componentsUnlocked;
        resetRecipeBook = GlobalController.Instance.resetRecipeBook;
    }

    public void SaveRecipeBookPrefs()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadRecipeBookPrefs()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        recipeBookUnlocked = data.allDebrisUnlocked;
    }

    public void ResetRecipeBook()
    {
        // Clear recipe book,
        for (int i = 0; i < recipeBookUnlocked.Length; i++)
        {
            recipeBookUnlocked[i] = false;
        }

        // ... then load in Mold, Radiation, and Cosmic Dust.
        recipeBookUnlocked[0] = true;
        recipeBookUnlocked[1] = true;
        recipeBookUnlocked[22] = true;

        SaveRecipeBookPrefs();

    }

    public void SetButtonsActiveOnStart()
    {
        for (int i = 0; i < recipeBookUnlocked.Length; i++)
        {
            if (recipeBookUnlocked[i]) 
            {
                allDebrisButtons[i].gameObject.SetActive(true);
                allUnkownImages[i].gameObject.SetActive(false);

                // Check to see if the item should display to the user it is locked.
                // This would happen in the case that the item was previously unlocked and added to the recipe book.
                if (!GameObject.Find("MainController").GetComponent<MainContoller004>().allDebrisUnlocked[i]) lockImages[i].gameObject.SetActive(true);
                else lockImages[i].gameObject.SetActive(false);

            }
            
        }

        for (int i = 0; i < componentsUnlocked.Length; i++)
        {
            if (componentsUnlocked[i]) componentButtons[i].gameObject.SetActive(true);
        }
    }

    public void ClickPanel(int panelNumber)
    {
        for (int i = 0; i < recipePanels.Length; i++)
        {
            if (i == panelNumber) recipePanels[i].SetActive(true);
            else recipePanels[i].SetActive(false);
        }

        // Clear the information on the screen.
        UpdateInformation("");
    }

    public void UpdateInformation(string info)
    {
        informationText.text = info;
    }

    public void LoadDictionary()
    {
        Materials.Add("COSMIC DUST", "The dust that makes up everything…");
        Materials.Add("RADIATION", "Good ole friendly radiation!");
        Materials.Add("STRANGE GOO", "It is strange and it is gooey.");
        Materials.Add("ICE", "It is cold.");
        Materials.Add("EMPTY ROCKET ENGINE", "An empty rocket engine is an expensive paper weight.");
        Materials.Add("BATTERY", "Hey, it doesn’t have any charge!");
        Materials.Add("ANTENNA", "Is it FM or AM?");
        Materials.Add("LENS", "Probably from the Hubble.");
        Materials.Add("SOLAR PANEL", "Convertes light into electricity! Cool");
        Materials.Add("SATELLITE DISH", "A good example of a paraboloid!");
        Materials.Add("PROPELLER", "What is a propeller doing out here?");
        Materials.Add("RADIATED DUST", "Cool! It is glowing dirt.");
        Materials.Add("ACID", "Don’t get it on your face!");
        Materials.Add("FROZEN GOO", "The strange Goo has been frozen, cool…");
        Materials.Add("LASER", "Light amplification by stimulated emission of radiation");
        Materials.Add("MOUNTABLE SPIKE", "Ouch, the sharp end hurts!");
        Materials.Add("MOUNTABLE DISH", "Maybe we can pick up MTV");
        Materials.Add("MOUNTABLE LENS", "Great, goo on a lens…");
        Materials.Add("GLASSES", "They maybe the wrong prescription but they sure are trending.");
        Materials.Add("RECHARGEABLE BATTERY PACK", "Handy for powering home appliances.");
        Materials.Add("ACTIVE ROCKET ENGINE", "It is a rocket engine. Who doesn’t want a rocket engine?");
        Materials.Add("MOUNTABLE PROPELLER", "Hey, it can spin!");
        Materials.Add("SPACE MOLD", "Standard Space Mold, it is gooey, gummy and smells like space.");
        Materials.Add("HEAT RESISTANT MOLD", "It is cold and slimy to the touch.");
        Materials.Add("ELECTRIC MOLD", "It is ALIVE!");
        Materials.Add("POISON MOLD", "Don’t eat it, it is poisonous.");
        Materials.Add("HEAT SHIELD", "Very handy for entering an atmosphere.");
        Materials.Add("DEFENSE SYSTEM", "Everyone should have a personal defense system!");
        Materials.Add("ATTACK SYSTEM", "The best offense is a good ATTACK SYSTEM");
        Materials.Add("COOL FACTOR", "When entering a new environment, it is important to make a good impression.");
        Materials.Add("MOBILITY PACKAGE", "Nothing fancy, but it should get you from A to B.");
        Materials.Add("CLIMATE CONTROLLER", "Keeps you cold in the summer and warm in the winter.");
    }

    public void ClickRecipeButton(string button)
    {
        UpdateInformation(button + ": " + Materials[button]);

        // Update the ingredients images so the player can refresh on what it take to make the item.

        // Set up temp variables to store the column and row when I find the item in the material matrix.
        int[] indicesRC = {0, 0 };

        // Then loop throught the matrix until we find the item. Save the col and row variables each 
        //time and break the loop when the item is found. No need to look in col or row 0 because we do 
        //not want the item found there.
        indicesRC = FindIngredients(button);

        // Use the stores col and row variables to load the ingredients images.
        if (indicesRC[0] != 0 && indicesRC[1] != 0)
        {
            ingredientsImage1.sprite = GameObject.Find("MainController").GetComponent<MainContoller004>().debrisSprites[indicesRC[0] - 1];
            ingredientsImage2.sprite = GameObject.Find("MainController").GetComponent<MainContoller004>().debrisSprites[indicesRC[1] - 1];
        }
        else
        {
            ingredientsImage1.sprite = blackSprite;
            ingredientsImage2.sprite = blackSprite;
        }
        
    }

    public int[] FindIngredients(string button)
    {
        int[] indicesRC = {0, 0};

        for (int i = 1; i < 27; i++)
        {
            for (int j = 1; j < 27; j++)
            {
                if (materialsCombinationArray[i, j] == button)
                {
                    Debug.Log("Match Found");
                    indicesRC[0] = i;
                    indicesRC[1] = j;
                    return indicesRC;
                }
            }
        }

        return indicesRC;
    }

}
