using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    //--------------------PUBLIC VARIABLES--------------------
    [Header("Reference Objects")]
    public GameObject creditsGO;
    public GameObject titleScreen;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject howToPlayMenu;
    public TMP_Text difficulty_txt;
    public GameObject fadeOutPanel;

    //--------------------PRIVATE VARIABLES--------------------
    difficultyManager _dm;

    //--------------------START--------------------
    void Start()
    {
        //resets the timescale in case it was previously set to 0
        Time.timeScale = 1f;
        //rolls credits
        StartCoroutine(credits());
        _dm = FindObjectOfType<difficultyManager>();
        //fades the credits in
        fadeOutPanel.GetComponent<Animator>().SetTrigger("fadeIn");
    }

    //--------------------UPDATE--------------------
    void Update()
    {
        //updates the difficulty text in the options menu to the current difficulty
        difficulty_txt.text = "Difficulty: " + _dm.difficulty[_dm.difficultyIndex%4][0];
    }

    //--------------------CREDITS--------------------
    IEnumerator credits()
    {
        //sets the credits game object to true
        creditsGO.SetActive(true);
        titleScreen.SetActive(false);
        //credits last for 3 seconds
        yield return new WaitForSeconds(3f);
        //switches to the title screen after 3 seconds
        creditsGO.SetActive(false);
        titleScreen.SetActive(true);
    }

    //--------------------START BUTTON--------------------
    public void startButton(int scene)
    {
        StartCoroutine(startFadeOut(scene));
    }
    //--------------------START BUTTON FADE OUT--------------------
    IEnumerator startFadeOut(int scene)
    {
        //fades out the screen then loads the next scene
        fadeOutPanel.GetComponent<Animator>().SetTrigger("fadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }

    //--------------------OPTIONS BUTTON--------------------
    public void optionsButton()
    {
        //enables the options screen and disables the mainmenu
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    //--------------------HOW TO PLAY BUTTON--------------------
    public void howToPlayButton()
    {
        //enables the how to play menu
        howToPlayMenu.SetActive(true);
    }

    //--------------------OPTIONS: BACK BUTTON--------------------
    public void optionsBackButton()
    {
        //returns to the main menu
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    //--------------------HOW TO PLAY: BACK BUTTON--------------------
    public void howToPlayBackButton()
    {
        //returns to the main menu
        howToPlayMenu.SetActive(false);
    }

    //--------------------DIFFICULTY BUTTON--------------------
    public void difficultyButton()
    {
        //increases the difficulty index
        ++_dm.difficultyIndex;
    }

    //--------------------EXIT BUTTON--------------------
    public void exitButton()
    {

    }
}
