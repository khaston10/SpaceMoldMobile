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
    #endregion

    #region Variables Buttons
    public Button[] moldButtons;
    public Button[] debrisButtons;
    public Button[] compositeButtons;
    public Button[] componentButtons;
    #endregion

    #region Variables Dictionary Of Materials
    Dictionary<string, string> Materials = new Dictionary<string, string>();
    public Sprite[] MaterialIcons;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Load();
        LoadDictionary();
        SetButtonsActiveOnStart();
        ClickPanel(0);
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
        componentsUnlocked = GlobalController.Instance.componentsUnlocked;
    }

    public void SetButtonsActiveOnStart()
    {
        for (int i = 0; i < moldsUnlocked.Length; i++)
        {
            if (moldsUnlocked[i]) moldButtons[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < debrisUnlocked.Length; i++)
        {
            if (debrisUnlocked[i]) debrisButtons[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < compoundDebrisUnlocked.Length; i++)
        {
            if (compoundDebrisUnlocked[i]) compositeButtons[i].gameObject.SetActive(true);
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
        Materials.Add("RADIATED DUST", "Cool! Its glowing dirt.");
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
    }

}
