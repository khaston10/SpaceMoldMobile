using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainContoller004 : MonoBehaviour
{

    #region Variables Stats

    public bool musicOn;
    public bool soundOn;
    public float volume;

    public bool[] moldsUnlocked = new bool[4];
    public bool[] debrisUnlocked = new bool[11];
    public bool[] compoundDebrisUnlocked = new bool[11];
    public bool[] componentsUnlocked = new bool[6];

    #endregion

    #region Variables Audio
    public AudioSource mainMusic;
    public AudioSource click1Good;
    public AudioMixer mainMixer;
    public Slider volumeSlider;
    #endregion

    #region Variables Panels
    public GameObject startOfDayPanel;
    public GameObject buttonPanel;
    public GameObject SettingsPanel;
    public GameObject informationPanel;
    public GameObject recipePanel;
    #endregion

    #region Variables Button Panel Buttons

    public int firstSelection;
    public int secondSelection;
    public Image[] buttonPanelImages;
    public Sprite[] debrisSprites;
    public Sprite spaceMoldIcon;
    #endregion

    #region Variables

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        Load();

        // At start we need to load random availiable debris
        LoadButtonImagesAtStartOfDay();
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
        moldsUnlocked = GlobalController.Instance.moldsUnlocked;
        debrisUnlocked = GlobalController.Instance.debrisUnlocked;
        compoundDebrisUnlocked = GlobalController.Instance.compoundDebrisUnlocked;
        componentsUnlocked = GlobalController.Instance.componentsUnlocked;
    }

    public void Save()
    {
        GlobalController.Instance.musicOn = musicOn;
        GlobalController.Instance.soundOn = soundOn;
        GlobalController.Instance.moldsUnlocked = moldsUnlocked;
        GlobalController.Instance.debrisUnlocked = debrisUnlocked;
        GlobalController.Instance.compoundDebrisUnlocked = compoundDebrisUnlocked;
        GlobalController.Instance.componentsUnlocked = componentsUnlocked;
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

    public void ToggleMusic()
    {
        if (musicOn)
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
        if (soundOn)
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
        recipePanel.GetComponent<RecipeController>().SetButtonsActiveOnStart();
    }

    #region Functions Button Panel

    public void PushSelectButton(int button)
    {
        // Check to see if first selection has been assigned.
        if (firstSelection == 55) 
        {
            firstSelection = button;
            Debug.Log("First Selection Set To: " + firstSelection.ToString());
        }
        else if (secondSelection == 55)
        {
            secondSelection = button;
            Debug.Log("Second Selection Set To: " + secondSelection.ToString());

            // Check to see if the 2 buttons are next to eachother. If not, reset buttons.
            if (Mathf.Abs(firstSelection - secondSelection) == 1 || Mathf.Abs(firstSelection - secondSelection) == 10)
            {
                Debug.Log("Buttons are next to eachother!");
            }

            else
            {
                Debug.Log("Dummy! Buttons are not next to eachother!");
            }

            // Reset buttons.
            firstSelection = 55;
            secondSelection = 55;
        }

        else
        {
            Debug.Log("No More Space");
        }
    }

    public void LoadButtonImagesAtStartOfDay()
    {
        // 1. Grab a random debris type or mold.
        // 2. Check to see if it is unlocked.
        // 3. Load it into the next button.
        int buttonsLeftToAssign = 16;

        while (buttonsLeftToAssign > 0)
        {

            int tempSprite = Random.Range(0, 11);
            if (debrisUnlocked[tempSprite])
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

    #endregion

    #endregion

}
