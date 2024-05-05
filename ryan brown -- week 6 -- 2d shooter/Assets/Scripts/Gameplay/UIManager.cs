using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //--------------------PUBLIC VARIABLES--------------------
    [Header("References")]
    public TMP_Text score_txt;
    public TMP_Text combo_txt;
    public GameObject[] hearts;
    public Slider volCtrlSlider;
    public Slider SFXVolCtrlSlider;
    public GameObject backgroundMusic;
    public GameObject fruitSFX;
    public GameObject bladeSFX;
    public GameObject bombSFX;
    public GameObject pauseScreen;
    public GameObject gameoverScreen;
    public GameObject fadeInPanel;
    public Image flashbang;

    //--------------------PRIVATE VARIABLES--------------------
    gameManager _gm;
    float fruitSFXBaseVol;
    float bladeSFXBaseVol;
    float bombSFXBaseVol;
    bool gameoverTriggered;

    //--------------------START--------------------
    void Start()
    {
        //sets timescale to 1 in case it was on 0
        Time.timeScale = 1f;
        //sets the volume control slider to the value stored in player prefs (last used)
        volCtrlSlider.value = PlayerPrefs.GetFloat("background_music_vol");
        //does the same for the sfx volume control slider
        SFXVolCtrlSlider.value = PlayerPrefs.GetFloat("SFX_vol");

        //references the game manager
        _gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameManager>();

        //accesses audio sources to play oneshots
        fruitSFXBaseVol = fruitSFX.GetComponent<AudioSource>().volume;
        bladeSFXBaseVol = bladeSFX.GetComponent<AudioSource>().volume;
        bombSFXBaseVol = bombSFX.GetComponent<AudioSource>().volume;

        //triggers the fade in
        fadeInPanel.GetComponent<Animator>().SetTrigger("fadeIn");
    }

    //--------------------UPDATE--------------------
    void Update()
    {
        //updates score and combo text every fram
        score_txt.text = "Score: " + _gm.score;
            //combo tracker works by increasing the combo multiplier by one for every 10 food hit, resets on bomb hit
        combo_txt.text = "Combo: " + Mathf.FloorToInt((_gm.comboTracker + 10) / 10) + "x";

        //manages the heart icons on the screen
        if (_gm.lives < 3)
        {
            hearts[2].SetActive(false);
        }
        if(_gm.lives < 2)
        {
            hearts[1].SetActive(false);
        }
        if(_gm.lives < 1)
        {
            hearts[0].SetActive(false);
        }

        //manages the background music and the sfx volumes using the sliders in the pause menu
        if(PlayerPrefs.GetFloat("background_music_vol") != volCtrlSlider.value)
        {
            PlayerPrefs.SetFloat("background_music_vol", volCtrlSlider.value);
        }
        backgroundMusic.GetComponent<AudioSource>().volume = volCtrlSlider.value;

        if (PlayerPrefs.GetFloat("SFX_vol") != SFXVolCtrlSlider.value)
        {
            PlayerPrefs.SetFloat("SFX_vol", SFXVolCtrlSlider.value);
        }
        fruitSFX.GetComponent<AudioSource>().volume = SFXVolCtrlSlider.value * fruitSFXBaseVol;
        bladeSFX.GetComponent<AudioSource>().volume = SFXVolCtrlSlider.value * bladeSFXBaseVol;
        bombSFX.GetComponent<AudioSource>().volume = SFXVolCtrlSlider.value * bombSFXBaseVol;

        //controls the game pausing
        if (_gm.gamePaused)
        {
            gamePause();
        }
        else
        {
            gameUnPause();
        }

        //makes it so gameover is only triggered once
        if (_gm.gameOver && !gameoverTriggered)
        {
            gameoverTriggered = true;
            StartCoroutine(gameOverSequence());
        }
    }


    //--------------------GAME PAUSE--------------------
    private void gamePause()
    {
        backgroundMusic.GetComponent<AudioSource>().Pause();
        pauseScreen.SetActive(true);
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
        }
    }

    //--------------------GAME UNPAUSE--------------------
    private void gameUnPause()
    {
        backgroundMusic.GetComponent<AudioSource>().UnPause();
        pauseScreen.SetActive(false);
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    //--------------------PAUSE BUTTON--------------------
    public void buttonGamePause()
    {
        _gm.gamePaused = true;
    }

    //--------------------UNPAUSE BUTTON--------------------
    public void buttonGameUnPause()
    {
        _gm.gamePaused = false;
    }

    //--------------------GAME OVER SEQUENCE--------------------
    IEnumerator gameOverSequence()
    {
        //stops the background music and activates the gameover screen
        backgroundMusic.GetComponent<AudioSource>().Stop();
        gameoverScreen.SetActive(true);

        float elapsed = 0f;
        float duration = 3f;
        
        //fades out a flashbang when game over triggers and slows down the game time
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            flashbang.color = Color.Lerp(Color.white, Color.clear, t);

            Time.timeScale = Mathf.Clamp01(1f - t);
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }

    //--------------------RESTART--------------------
    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //--------------------QUIT GAME--------------------
    public void quitGame(int scene)
    {
        Time.timeScale = 1f;
        _gm.gamePaused = false;
        StartCoroutine(quitGameFade(scene));
    }

    //--------------------GMAE QUIT FADE--------------------
    IEnumerator quitGameFade(int scene)
    {
        fadeInPanel.GetComponent<Animator>().SetTrigger("fadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(scene);
    }
}
