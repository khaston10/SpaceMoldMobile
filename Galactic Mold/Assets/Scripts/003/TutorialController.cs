using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class TutorialController : MonoBehaviour
{
    #region Variables Stats

    public bool musicOn;
    public bool soundOn;
    public float volume;
    public bool playerHasStartedMovesForDay;
    public int day;
    public int difficulty; // 0 - Easy, 1 - Medium, 2 - Hard
    private int wavePointsRequired;

    public bool[] moldsUnlocked = new bool[4];
    public bool[] debrisUnlocked = new bool[11];
    public bool[] compoundDebrisUnlocked = new bool[11];
    // 0-10 Debris, 11-21 Compound Debris, 22-25 Mold;
    public bool[] allDebrisUnlocked = new bool[26];
    public bool[] componentsUnlocked = new bool[6];


    #endregion

    #region Variables Audio
    public AudioSource mainMusic;
    public AudioSource click1Good;
    public AudioSource create;
    public AudioSource combine;
    public AudioSource noMatch;
    public AudioMixer mainMixer;
    public Slider volumeSlider;
    public Toggle musicToggle;
    public Toggle soundToggle;
    public AudioSource[] TutClip;
    #endregion

    #region Variables Panels - text
    public GameObject startOfDayPanel;
    public Image startOfDayIconImage;
    public Text startOfDayIconText;
    public Text startOfDayTitleText;
    public GameObject buttonPanel;
    public GameObject SettingsPanel;
    public GameObject menuPanel;
    public GameObject informationPanel;
    public GameObject recipePanel;
    public GameObject newItemPanel;
    public Image newItemImage;
    public Text newItemText;
    public Text dayText;
    public Text wavePointsRequiredText;
    public Button endOfDayButton;
    #endregion

    #region Variables Button Panel Buttons

    public int firstSelection;
    public int secondSelection;
    public Sprite blank;
    public Image[] buttonPanelImages;
    public Sprite[] debrisSprites;
    public Sprite spaceMoldIcon;
    public Sprite[] componentsSprites;
    public Image buttonSelector;
    #endregion

    #region Variables
    public GameObject Exp1;
    public GameObject Exp2;
    public GameObject Exp3;
    private GameObject tempExp;
    private GameObject tempExp2;
    private bool showEndOfDayButton;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        Load();

        LoadDifficulty();

        // Set text objects.
        UpdateTextObjects();

        // At start we need to load random availiable debris
        LoadButtonImagesAtStartOfTutorial();

        // Set Volume Slider to initial volume and toggles.
        volumeSlider.value = volume;
        mainMixer.SetFloat("MainVolume", volume);
        SetTogglesOnStart();

        PlayTutorialClip(0);

        showEndOfDayButton = true;

        // Create temp explosion and hide it.
        tempExp = Instantiate(Exp1);
        tempExp2 = Instantiate(Exp1);
        tempExp.transform.position = Vector3.one * 300;
        tempExp2.transform.position = Vector3.one * 300;

        // For tutorial we need to unlock goo and ice so it does not show up in the new debris slot.
        allDebrisUnlocked[2] = true;
        allDebrisUnlocked[3] = true;

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Functions - Save And Load Data

    public void Load()
    {
        musicOn = GlobalController.Instance.musicOn;
        soundOn = GlobalController.Instance.soundOn;
        volume = GlobalController.Instance.volume;
        day = GlobalController.Instance.day;
        moldsUnlocked = GlobalController.Instance.moldsUnlocked;
        debrisUnlocked = GlobalController.Instance.debrisUnlocked;
        compoundDebrisUnlocked = GlobalController.Instance.compoundDebrisUnlocked;
        allDebrisUnlocked = GlobalController.Instance.allDebrisUnlocked;
        componentsUnlocked = GlobalController.Instance.componentsUnlocked;
        difficulty = GlobalController.Instance.difficulty;
    }

    public void Save()
    {
        GlobalController.Instance.musicOn = musicOn;
        GlobalController.Instance.soundOn = soundOn;
        GlobalController.Instance.volume = volume;
        GlobalController.Instance.day = day;
        GlobalController.Instance.moldsUnlocked = moldsUnlocked;
        GlobalController.Instance.debrisUnlocked = debrisUnlocked;
        GlobalController.Instance.compoundDebrisUnlocked = compoundDebrisUnlocked;
        GlobalController.Instance.allDebrisUnlocked = allDebrisUnlocked;
        GlobalController.Instance.componentsUnlocked = componentsUnlocked;
        GlobalController.Instance.difficulty = difficulty;
    }

    public void LoadDifficulty()
    {
        if (difficulty == 0)
        {
            day = 2;
            wavePointsRequired = 10;
        }

        else if (difficulty == 1)
        {
            day = 2;
            wavePointsRequired = 10;
        }

        else if (difficulty == 2)
        {
            day = 2;
            wavePointsRequired = 10;
        }

        else Debug.Log("Difficulty Loaded Incorrectly");
    }

    #endregion

    #region Functions - Settings
    public void ClickSettings()
    {
        SettingsPanel.SetActive(true);
    }

    public void ClickCloseSettings()
    {
        SettingsPanel.SetActive(false);
    }

    public void ClickExitTutorial()
    {
        ResetUnlockedItems();
        SceneManager.LoadScene(4);
    }

    public void SetTogglesOnStart()
    {
        if (musicOn == false) musicToggle.isOn = false;
        if (soundOn == false) soundToggle.isOn = false;
    }

    public void ToggleMusic()
    {
        if (musicToggle.isOn == false)
        {
            musicOn = false;
            mainMusic.volume = 0f;
        }
        else
        {
            musicOn = true;
            mainMusic.volume = 1f;
        }
    }

    public void ToggleSound()
    {
        if (soundToggle.isOn == false)
        {
            soundOn = false;
            click1Good.volume = 0f;
        }

        else
        {
            soundOn = true;
            click1Good.volume = 1f;
        }
    }

    public void SliderVolume()
    {
        volume = volumeSlider.value;
        mainMixer.SetFloat("MainVolume", volume);
    }

    #endregion

    #region Functions - Various

    public void UpdateTextObjects()
    {
        dayText.text = day.ToString();
        wavePointsRequiredText.text = wavePointsRequired.ToString();
    }

    public void ClickRecipeBook(bool turnOn)
    {
        if (turnOn) recipePanel.SetActive(true);
        else recipePanel.SetActive(false);
    }

    public void ClickStartOfDay()
    {
        // Hide start of day panel and show button panel.
        startOfDayPanel.SetActive(false);
        buttonPanel.SetActive(true);
        informationPanel.SetActive(true);
        menuPanel.SetActive(true);
        recipePanel.GetComponent<RecipeController>().SetButtonsActiveOnStart();
        LoadButtonImagesAtStartOfDay();


    }

    public bool UnlockDebrisAtStartOfDay()
    {
        // Check to see if there is a new ingredent to unlock. 
        for (int i = 0; i < 11; i++)
        {
            if (allDebrisUnlocked[i] == false)
            {
                Debug.Log("We have unlocked item.");
                allDebrisUnlocked[i] = true;
                startOfDayIconImage.sprite = debrisSprites[i];
                startOfDayIconText.text = recipePanel.GetComponent<RecipeController>().materialArrayForSearching[i];
                return true;
            }
        }

        startOfDayTitleText.text = "No New Debris";
        startOfDayIconImage.sprite = blank;
        startOfDayIconText.text = "";
        return false;
    }

    public void ClickEndOfDay(int tutorialClip)
    {
        // Check to see if game play is up. O days to impact.
        if (day == 1)
        {
            Save();
            SceneManager.LoadScene(3);
        }

        // Set End of day button inactive.
        endOfDayButton.gameObject.SetActive(false);

        // Set Day and close/open panels.
        day -= 1;
        UpdateTextObjects();
        UnlockDebrisAtStartOfDay();
        buttonPanel.SetActive(false);
        startOfDayPanel.SetActive(true);
        informationPanel.SetActive(false);
        menuPanel.SetActive(false);

        // Reset Wave Points Required.
        if (difficulty == 0) wavePointsRequired = 10;
        else if (difficulty == 1) wavePointsRequired = 15;
        else if (difficulty == 2) wavePointsRequired = 20;
        UpdateTextObjects();

        // Toggle Player's ability to use shuffle.
        playerHasStartedMovesForDay = false;

        // Play the correct audio clip.
        PlayTutorialClip(tutorialClip);

        // Because we only want this button to show once in the tutorial we are putting a flag on it.
        showEndOfDayButton = false;

    }

    public void CloseNewItemPanel()
    {
        newItemPanel.SetActive(false);
    }

    public void PlayTutorialClip(int clipNumber)
    {
        TutClip[clipNumber].Play();
    }

    #endregion

    #region Functions Button Panel

    public void PushSelectButton(int button)
    {
        // Check to see if first selection has been assigned.
        if (firstSelection == 55)
        {
            firstSelection = button;
            //Debug.Log("First Selection Set To: " + firstSelection.ToString());

            // Move button selector image ontop of the first selection.
            buttonSelector.gameObject.transform.position = GetButtonImageOnGamePanel(button).gameObject.transform.position;
        }
        else if (secondSelection == 55)
        {
            secondSelection = button;
            //Debug.Log("Second Selection Set To: " + secondSelection.ToString());

            // Check to see if the 2 buttons are next to eachother, and that the 2nd selection is not blank. If not, reset buttons.
            if (
                (Mathf.Abs(firstSelection - secondSelection) == 1 || Mathf.Abs(firstSelection - secondSelection) == 10
                || Mathf.Abs(firstSelection - secondSelection) == 11 || Mathf.Abs(firstSelection - secondSelection) == 9)
                & GetButtonImageOnGamePanel(secondSelection).sprite.name != "UIMask")
            {
                Debug.Log(GetCombinationResult(GetButtonImageOnGamePanel(firstSelection).sprite.name, GetButtonImageOnGamePanel(secondSelection).sprite.name));

                // Check to see if combination is two of the same image, if yes, the player gets wave points.
                if (GetButtonImageOnGamePanel(firstSelection).sprite.name == GetButtonImageOnGamePanel(secondSelection).sprite.name)
                {
                    AwardPlayerWavePoints();
                }

                // Place the image of the returned sprite on the second selection.
                if (GetCombinationResult(GetButtonImageOnGamePanel(firstSelection).sprite.name, GetButtonImageOnGamePanel(secondSelection).sprite.name) == "NA")
                {
                    // Play the explosion effect.
                    if (GetButtonImageOnGamePanel(secondSelection).sprite.name == GetButtonImageOnGamePanel(firstSelection).sprite.name)
                    {
                        PlayExplosion(GetButtonImageOnGamePanel(secondSelection).transform.position, GetButtonImageOnGamePanel(firstSelection).transform.position, 3);
                        Debug.Log(GetButtonImageOnGamePanel(secondSelection).sprite.name);
                        Debug.Log(GetButtonImageOnGamePanel(firstSelection).sprite.name);
                    }
                    else
                    {
                        noMatch.Play();
                    }

                    GetButtonImageOnGamePanel(secondSelection).sprite = blank;
                }

                else if (GetCombinationResult(GetButtonImageOnGamePanel(firstSelection).sprite.name, GetButtonImageOnGamePanel(secondSelection).sprite.name) == "HEAT SHIELD")
                {
                    NewComponentUnlocked(0, "Very handy for entering an atmosphere.");
                    GetButtonImageOnGamePanel(secondSelection).sprite = blank;
                    AwardPlayerWavePoints();
                    // Play the explosion effect.
                    PlayExplosion(GetButtonImageOnGamePanel(secondSelection).transform.position, GetButtonImageOnGamePanel(firstSelection).transform.position, 1);
                }

                else if (GetCombinationResult(GetButtonImageOnGamePanel(firstSelection).sprite.name, GetButtonImageOnGamePanel(secondSelection).sprite.name) == "DEFENSE SYSTEM")
                {
                    NewComponentUnlocked(1, "Everyone should have a personal defense system!");
                    GetButtonImageOnGamePanel(secondSelection).sprite = blank;
                    AwardPlayerWavePoints();
                    // Play the explosion effect.
                    PlayExplosion(GetButtonImageOnGamePanel(secondSelection).transform.position, GetButtonImageOnGamePanel(firstSelection).transform.position, 1);
                }

                else if (GetCombinationResult(GetButtonImageOnGamePanel(firstSelection).sprite.name, GetButtonImageOnGamePanel(secondSelection).sprite.name) == "ATTACK SYSTEM")
                {
                    NewComponentUnlocked(2, "The best offense is a good ATTACK SYSTEM");
                    GetButtonImageOnGamePanel(secondSelection).sprite = blank;
                    AwardPlayerWavePoints();
                    // Play the explosion effect.
                    PlayExplosion(GetButtonImageOnGamePanel(secondSelection).transform.position, GetButtonImageOnGamePanel(firstSelection).transform.position, 1);
                }

                else if (GetCombinationResult(GetButtonImageOnGamePanel(firstSelection).sprite.name, GetButtonImageOnGamePanel(secondSelection).sprite.name) == "COOL FACTOR")
                {
                    NewComponentUnlocked(3, "When entering a new environment, it is important to make a good impression.");
                    GetButtonImageOnGamePanel(secondSelection).sprite = blank;
                    AwardPlayerWavePoints();
                    // Play the explosion effect.
                    PlayExplosion(GetButtonImageOnGamePanel(secondSelection).transform.position, GetButtonImageOnGamePanel(firstSelection).transform.position, 1);
                }

                else if (GetCombinationResult(GetButtonImageOnGamePanel(firstSelection).sprite.name, GetButtonImageOnGamePanel(secondSelection).sprite.name) == "MOBILITY PACKAGE")
                {
                    NewComponentUnlocked(4, "Nothing fancy, but it should get you from A to B.");
                    GetButtonImageOnGamePanel(secondSelection).sprite = blank;
                    AwardPlayerWavePoints();
                    // Play the explosion effect.
                    PlayExplosion(GetButtonImageOnGamePanel(secondSelection).transform.position, GetButtonImageOnGamePanel(firstSelection).transform.position, 1);
                }

                else if (GetCombinationResult(GetButtonImageOnGamePanel(firstSelection).sprite.name, GetButtonImageOnGamePanel(secondSelection).sprite.name) == "CLIMATE CONTROLLER")
                {
                    NewComponentUnlocked(5, "Keeps you cold in the summer and warm in the winter.");
                    GetButtonImageOnGamePanel(secondSelection).sprite = blank;
                    AwardPlayerWavePoints();
                    // Play the explosion effect.
                    PlayExplosion(GetButtonImageOnGamePanel(secondSelection).transform.position, GetButtonImageOnGamePanel(firstSelection).transform.position, 1);
                }

                else
                {
                    int temp = System.Array.IndexOf(recipePanel.GetComponent<RecipeController>().materialArrayForSearching, GetCombinationResult(GetButtonImageOnGamePanel(firstSelection).sprite.name, GetButtonImageOnGamePanel(secondSelection).sprite.name));
                    GetButtonImageOnGamePanel(secondSelection).sprite = debrisSprites[temp];

                    AwardPlayerWavePoints();

                    // Play the explosion effect.
                    PlayExplosion(GetButtonImageOnGamePanel(secondSelection).transform.position, GetButtonImageOnGamePanel(firstSelection).transform.position, 1);

                    // Check to see if item is new!
                    if (allDebrisUnlocked[temp] != true)
                    {
                        //NewItemUnlocked(temp);

                        if (wavePointsRequired > 0)
                        {
                            wavePointsRequired -= 5;
                            UpdateTextObjects();

                            if (wavePointsRequired <= 0 & endOfDayButton)
                            {
                                endOfDayButton.gameObject.SetActive(true);
                            }
                        }
                    }

                }



                // Replace the image for first selection as black.
                GetButtonImageOnGamePanel(firstSelection).sprite = blank;

                // Disable players ability to shuffle after first move.
                if (playerHasStartedMovesForDay == false) playerHasStartedMovesForDay = true;

                // Reset selections.
                firstSelection = 55;
                secondSelection = 55;

                // Move the button selector image off screen.
                buttonSelector.gameObject.transform.position = Vector3.one * 1000f;

            }

            else
            {
                //Debug.Log("Dummy! Buttons are not next to eachother!");
            }

            // Reset buttons.
            firstSelection = 55;
            secondSelection = 55;

            // Move the button selector image off screen.
            buttonSelector.gameObject.transform.position = Vector3.one * 1000f;
        }

        else
        {
            Debug.Log("No More Space");
        }
    }

    public void AwardPlayerWavePoints()
    {
        if (wavePointsRequired > 0)
        {
            wavePointsRequired -= 5;
            UpdateTextObjects();

            if (wavePointsRequired <= 0 & endOfDayButton & showEndOfDayButton)
            {
                endOfDayButton.gameObject.SetActive(true);
            }
        }
    }

    public Image GetButtonImageOnGamePanel(int buttonCode)
    {
        // Because of the strange numbering system for the buttons.
        // 00, 01, 02, 03
        // 10, 11, 12, 13
        // 20, 21, 22, 23
        // 30, 31, 32, 33
        // We will deal with this in 4 seperate cases.

        // Get last digit.
        int lastDigit = buttonCode % 10;

        if (buttonCode < 10) return buttonPanelImages[lastDigit];
        else if (buttonCode < 20) return buttonPanelImages[lastDigit + 4];
        else if (buttonCode < 30) return buttonPanelImages[lastDigit + 8];
        else return buttonPanelImages[lastDigit + 12];
    }

    public void LoadButtonImagesAtStartOfDay()
    {
        // 1. Grab a random debris type or mold.
        // 2. Check to see if it is unlocked.
        // 3. Load it into the next button.
        int buttonsLeftToAssign = 16;

        while (buttonsLeftToAssign > 0)
        {

            int tempSprite = Random.Range(0, 26);
            if (allDebrisUnlocked[tempSprite])
            {
                buttonPanelImages[buttonsLeftToAssign - 1].sprite = debrisSprites[tempSprite];
                buttonsLeftToAssign -= 1;
            }
        }

        // 4. pick four buttons to replace the images with space mold.
        for (int i = 0; i < 4; i++)
        {
            int randButtonImage = Random.Range(0, 15);
            buttonPanelImages[randButtonImage].sprite = spaceMoldIcon;
        }

    }

    public void LoadButtonImagesAtStartOfTutorial()
    {

        for (int i = 0; i < 16; i++)
        {
            buttonPanelImages[i].sprite = blank;
        }

        buttonPanelImages[3].sprite = spaceMoldIcon;
        buttonPanelImages[7].sprite = spaceMoldIcon;
        buttonPanelImages[5].sprite = debrisSprites[2];
        buttonPanelImages[8].sprite = debrisSprites[3];
        buttonPanelImages[14].sprite = debrisSprites[7];
        buttonPanelImages[15].sprite = debrisSprites[8];

    }

    public void ClickShuffle()
    {
        // Need to check if player has ability to shuffle.
        if (playerHasStartedMovesForDay)
        {
            Debug.Log("Unable to Shuffle!");
        }

        else
        {
            LoadButtonImagesAtStartOfDay();
            click1Good.Play();
        }

    }

    public string GetCombinationResult(string material1, string material2)
    {
        int tempIndex1 = System.Array.IndexOf(recipePanel.GetComponent<RecipeController>().materialArrayForSearching, material1);
        int tempIndex2 = System.Array.IndexOf(recipePanel.GetComponent<RecipeController>().materialArrayForSearching, material2);
        return recipePanel.GetComponent<RecipeController>().materialsCombinationArray[tempIndex1 + 1, tempIndex2 + 1];
    }

    public void NewItemUnlocked(int index)
    {
        Debug.Log("New Item Unlocked!");
        allDebrisUnlocked[index] = true;
        newItemImage.sprite = debrisSprites[index];
        newItemText.text = recipePanel.GetComponent<RecipeController>().materialArrayForSearching[index];
        newItemPanel.SetActive(true);

        // Update the recipe book.
        recipePanel.GetComponent<RecipeController>().SetButtonsActiveOnStart();

    }

    public void NewComponentUnlocked(int index, string description)
    {
        componentsUnlocked[index] = true;
        newItemImage.sprite = componentsSprites[index];
        newItemText.text = description;
        newItemPanel.SetActive(true);

        // Update the recipe book.
        recipePanel.GetComponent<RecipeController>().SetButtonsActiveOnStart();

    }

    public void PlayExplosion(Vector3 loc1, Vector3 loc2, int expType)
    {
        Destroy(tempExp);

        if (expType == 1)
        {
            tempExp = Instantiate(Exp3);
            create.Play();
        }

        else
        {
            tempExp = Instantiate(Exp1);
            tempExp2 = Instantiate(Exp1);
            combine.Play();
        }

        tempExp.transform.position = loc1;
        tempExp2.transform.position = loc2;

    }

    public void ResetUnlockedItems()
    {
        // Use this function to reset unlocked items. This can be needed when the player starts the game over,
        // or when the player exits the tutorial.
        for (int i = 3; i < allDebrisUnlocked.Length; i++)
        {
            allDebrisUnlocked[i] = false;
        }

        allDebrisUnlocked[22] = true;

        for (int i = 0; i < componentsUnlocked.Length; i++)
        {
            componentsUnlocked[i] = false;
        }

    }

    #endregion

}
