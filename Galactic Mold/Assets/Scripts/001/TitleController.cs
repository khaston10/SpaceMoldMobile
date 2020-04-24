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

    #region Variables
    public GameObject StartPanel;
    public GameObject SettingsPanel;
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
        componentsUnlocked = GlobalController.Instance.componentsUnlocked;

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
        GlobalController.Instance.componentsUnlocked = componentsUnlocked;
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

    #endregion
}
