using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    #region Variables Stats

    public bool musicOn;
    public bool soundOn;
    public float volume;
    public int day;
    public int difficulty; // 0 - Easy, 1 - Medium, 2 - Hard

    public bool[] moldsUnlocked = new bool[4];
    public bool[] debrisUnlocked = new bool[11];
    public bool[] compoundDebrisUnlocked = new bool[11];
    // 0-10 Debris, 11-21 Compound Debris, 22-25 Mold;
    public bool[] allDebrisUnlocked = new bool[26];
    public bool[] componentsUnlocked = new bool[6];
    public bool resetRecipeBook;

    #endregion

    #region Variables Audio
    public AudioSource mainMusic;
    public AudioSource click1Good;
    public AudioMixer mainMixer;
    public Slider volumeSlider;
    #endregion

    #region Variables
    public GameObject StartPanel;
    public GameObject SettingsPanel;
    public Dropdown difficultyDropdown;
    public GameObject StatsPanel;
    #endregion
    void Start()
    {
        Load();
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

    #endregion

    #region Functions - Buttons

    public void ClickStart()
    {
        Save();
        SceneManager.LoadScene(1);
    }

    public void ClickSettings()
    {
        StartPanel.SetActive(false);
        SettingsPanel.SetActive(true);

    }

    public void ClickCloseSettings()
    {
        SettingsPanel.SetActive(false);
        StartPanel.SetActive(true);
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

    public void ChangeDifficulty()
    {
        Debug.Log(difficultyDropdown.value);
        difficulty = difficultyDropdown.value;
    }

    public void ClickExitGame()
    {
        Application.Quit();
    }

    public void ClickStatsPanel()
    {
        StatsPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }

    public void ClickCloseStatsPanel()
    {
        StartPanel.SetActive(true);
        StatsPanel.SetActive(false);
    }

    public void ClickResetRecipeBook()
    {
        resetRecipeBook = true;
        GlobalController.Instance.resetRecipeBook = resetRecipeBook;
        StartPanel.SetActive(true);
        StatsPanel.SetActive(false);
    }

    #endregion

    
}
  