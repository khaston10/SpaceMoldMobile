using UnityEngine;
using UnityEngine.UI;

public class GlobalController : MonoBehaviour
{
    public static GlobalController Instance;

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


    void Awake()
    {

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
