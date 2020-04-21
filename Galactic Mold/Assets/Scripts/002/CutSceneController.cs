using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutSceneController : MonoBehaviour
{
    #region BackGround Variables
    public Image BackGroundImage;
    public Sprite[] backgrounds;
    #endregion

    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        
        if (timer > 2) BackGroundImage.sprite = backgrounds[1];
        if (timer > 4) BackGroundImage.sprite = backgrounds[2];
        if (timer > 6) SceneManager.LoadScene(2);
    }
}
