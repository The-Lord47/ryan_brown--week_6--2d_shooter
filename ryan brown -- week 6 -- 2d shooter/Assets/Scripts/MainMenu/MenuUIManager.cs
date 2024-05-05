using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public GameObject creditsGO;
    public GameObject titleScreen;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject howToPlayMenu;
    public TMP_Text difficulty_txt;
    public GameObject fadeOutPanel;
    difficultyManager _dm;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(credits());
        _dm = FindObjectOfType<difficultyManager>();
        fadeOutPanel.GetComponent<Animator>().SetTrigger("fadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        difficulty_txt.text = "Difficulty: " + _dm.difficulty[_dm.difficultyIndex%4][0];
    }


    IEnumerator credits()
    {
        creditsGO.SetActive(true);
        titleScreen.SetActive(false);
        yield return new WaitForSeconds(3f);
        creditsGO.SetActive(false);
        titleScreen.SetActive(true);
    }

    public void startButton(int scene)
    {
        StartCoroutine(startFadeOut(scene));
    }

    IEnumerator startFadeOut(int scene)
    {
        fadeOutPanel.GetComponent<Animator>().SetTrigger("fadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }

    public void optionsButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void howToPlayButton()
    {
        howToPlayMenu.SetActive(true);
    }

    public void optionsBackButton()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void howToPlayBackButton()
    {
        howToPlayMenu.SetActive(false);
    }

    public void difficultyButton()
    {
        ++_dm.difficultyIndex;
    }

    public void exitButton()
    {

    }
}
