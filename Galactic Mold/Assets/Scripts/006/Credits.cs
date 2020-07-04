using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class Credits : MonoBehaviour
{
    public Text creditsText;
    public GameObject explosionPrefab;
    public AudioSource explosionSound;
    private GameObject tempExp;
    private bool isRunning = false;
    private string[] credits = new string[] { "A DEAD PROPELLER GAME", "Voice Acting \n- Brett Kehoe", "Music \n- August Hansen", 
        "Game Design and Programming \n- Kevin Haston" };
    public int creditTracker = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) StartCoroutine(UpdateCredits());
    }

    IEnumerator UpdateCredits()
    {
        isRunning = true;

        // If there are more credits, we will update them,
        if (creditTracker < credits.Length)
        {
            // Play explosion
            tempExp = Instantiate(explosionPrefab);
            tempExp.transform.position = Vector3.zero;
            explosionSound.Play();
            creditsText.text = credits[creditTracker];
        }

        // ...otherwise we will load the start screen.
        else SceneManager.LoadScene(7);

        // Wait 3 seconds and then exit the routine.
        yield return new WaitForSeconds(3);

        creditTracker += 1;
        Destroy(tempExp);
        isRunning = false;
    }
}
