using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CutSceneController : MonoBehaviour
{
    #region BackGround Variables
    public Image BackGroundImage;
    public Sprite[] backgrounds;
    #endregion

    #region Variables Audio

    public AudioMixer mainMixer;
    public AudioSource music;
    public AudioSource clickGood1;
    public float volume;
    public bool soundOn;
    public bool musicOn;

    #endregion

    public float timer;

    // Start is called before the first frame update
    void Start()
    {

        // Load data from previous scene.
        Load();

        // Set music and sound initial values.
        if (soundOn == false) clickGood1.volume = 0;
        if (musicOn == false) music.volume = 0;
        mainMixer.SetFloat("MainVolume", volume);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        
        if (timer > 2) BackGroundImage.sprite = backgrounds[1];
        if (timer > 4) BackGroundImage.sprite = backgrounds[2];
        if (timer > 6) SceneManager.LoadScene(2);
    }

    public void Load()
    {
        musicOn = GlobalController.Instance.musicOn;
        soundOn = GlobalController.Instance.soundOn;
        volume = GlobalController.Instance.volume;
    }
}
