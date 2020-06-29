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

    #region Variables MoldChar
    public bool back0IsShowing;
    private bool back1IsShowing;
    private bool back2IsShowing;
    private bool back3IsShowing;
    public Animator anim;
    public GameObject debrisPrefab;
    private GameObject t;

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

        if (timer > 5 && back0IsShowing)
        {
            anim.Play("Morton2");
            back0IsShowing = false;
            back1IsShowing = true;
        }

        else if (timer > 14 && back1IsShowing)
        {
            BackGroundImage.sprite = backgrounds[1];
            back1IsShowing = false;
            back2IsShowing = true;
            anim.Play("Morton3");
            Debug.Log("Unfortunatley");
        }


        else if (timer > 31 && back2IsShowing) 
        {
            BackGroundImage.sprite = backgrounds[2];
            back2IsShowing = false;
            back3IsShowing = true;
            t = Instantiate(debrisPrefab);
            anim.Play("Morton1");
            t.transform.position = Vector3.zero;
        } 

        else if (timer > 45 && back3IsShowing) SceneManager.LoadScene(2);
    }

    public void Load()
    {
        musicOn = GlobalController.Instance.musicOn;
        soundOn = GlobalController.Instance.soundOn;
        volume = GlobalController.Instance.volume;
    }

    public void ClickSkip()
    {
        SceneManager.LoadScene(3);
    }
}
