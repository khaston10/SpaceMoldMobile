using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CutSceneContoller005 : MonoBehaviour
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

    #region Variables MoldChar
    public bool back1IsShowing;
    private bool back2IsShowing;
    private bool back3IsShowing;
    public GameObject mortonPrefab;
    public GameObject explosionPrefab;
    private GameObject temp;
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

        if (timer > 2 && back1IsShowing)
        {
            BackGroundImage.sprite = backgrounds[1];
            back1IsShowing = false;
            back2IsShowing = true;
            Destroy(mortonPrefab);
            temp = Instantiate(explosionPrefab);
            var tempVector = new Vector3(4f, -3.5f, 0f);
            temp.transform.position = tempVector;
        }

        else if (timer > 5 && back2IsShowing) SceneManager.LoadScene(4);
    }

    public void Load()
    {
        musicOn = GlobalController.Instance.musicOn;
        soundOn = GlobalController.Instance.soundOn;
        volume = GlobalController.Instance.volume;
    }

    public void ClickSkip()
    {
        SceneManager.LoadScene(4);
    }
}
