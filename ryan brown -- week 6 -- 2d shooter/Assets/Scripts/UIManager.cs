using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    gameManager _gm;
    public TMP_Text score_txt;
    public GameObject[] hearts;
    public Slider volCtrlSlider;
    public GameObject backgroundMusic;


    // Start is called before the first frame update
    void Start()
    {
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

        backgroundMusic.GetComponent<AudioSource>().volume = volCtrlSlider.value;
    }
}
