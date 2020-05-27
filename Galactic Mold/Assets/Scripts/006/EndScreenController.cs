using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class EndScreenController : MonoBehaviour
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

    #endregion

    #region Variables Audio
    public AudioSource mainMusic;
    public AudioSource click1Good;
    public AudioMixer mainMixer;
    #endregion

    #region Variables - Images, Text, Toggle

    public Image[] componentsImages;
    public Image[] stars;
    public Sprite[] componentSprites;
    public Sprite starSprite;
    public Text percentageText;
    private int percentageInt;
    public Toggle survivedToggle;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Load();

        LoadEndScreenStats();

        // Set music and sound initial values.
        if (soundOn == false) click1Good.volume = 0;
        if (musicOn == false) mainMusic.volume = 0;
        mainMixer.SetFloat("MainVolume", volume);
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

    #endregion

    public void LoadEndScreenStats()
    {
        // Set Did Survive Toggle
        if (componentsUnlocked[0])
        {
            survivedToggle.isOn = true;
        }

        // Set % stats
        for (int i = 0; i < componentsUnlocked.Length; i++)
        {
            if (componentsUnlocked[i]) percentageInt += 10;
        }
        percentageText.text = percentageInt.ToString();

        // Set Component Images
        for (int i = 0; i < componentsUnlocked.Length; i++)
        {
            if (componentsUnlocked[i]) componentsImages[i].sprite = componentSprites[i];
        }

        // Set x/10 stars
        for (int i = 0; i < (percentageInt / 10); i++)
        {
            stars[i].sprite = starSprite;
            Debug.Log(percentageInt / 10);
        }

        Debug.Log((percentageInt % 10) + 1);

    }

    public void ClickExitGame()
    {
        Application.Quit();
    }


}
