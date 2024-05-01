using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public GameObject creditsGO;
    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(credits());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator credits()
    {
        creditsGO.SetActive(true);
        titleScreen.SetActive(false);
        yield return new WaitForSeconds(5f);
        creditsGO.SetActive(false);
        titleScreen.SetActive(true);
    }

    public void startButton(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void optionsButton()
    {

    }

    public void exitButton()
    {

    }
}
