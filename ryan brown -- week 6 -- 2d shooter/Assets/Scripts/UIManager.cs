using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    gameManager _gm;
    public TMP_Text score_txt;
    public GameObject[] hearts;
    public Slider volCtrlSlider;
    public GameObject backgroundMusic;
    public GameObject pauseScreen;
    public GameObject gameoverScreen;
    public Image flashbang;
    public bool gameoverTriggered;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        volCtrlSlider.value = PlayerPrefs.GetFloat("background_music_vol");
        _gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        score_txt.text = "Score: " + _gm.score;

        if(_gm.lives < 3)
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

        
        if(PlayerPrefs.GetFloat("background_music_vol") != volCtrlSlider.value)
        {
            PlayerPrefs.SetFloat("background_music_vol", volCtrlSlider.value);
        }
        backgroundMusic.GetComponent<AudioSource>().volume = volCtrlSlider.value;


        if (_gm.gamePaused)
        {
            gamePause();
        }
        else
        {
            gameUnPause();
        }

        if (_gm.gameOver && !gameoverTriggered)
        {
            gameoverTriggered = true;
            StartCoroutine(gameOverSequence());
        }
    }

    private void gamePause()
    {
        backgroundMusic.GetComponent<AudioSource>().Pause();
        pauseScreen.SetActive(true);
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
        }
    }
    
    private void gameUnPause()
    {
        backgroundMusic.GetComponent<AudioSource>().UnPause();
        pauseScreen.SetActive(false);
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    public void buttonGamePause()
    {
        
        _gm.gamePaused = true;
    }

    public void buttonGameUnPause()
    {
        
        _gm.gamePaused = false;
    }

    IEnumerator gameOverSequence()
    {
        backgroundMusic.GetComponent<AudioSource>().Stop();
        gameoverScreen.SetActive(true);

        float elapsed = 0f;
        float duration = 3f;
        
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            flashbang.color = Color.Lerp(Color.white, Color.clear, t);

            Time.timeScale = Mathf.Clamp01(1f - t);
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }

    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
