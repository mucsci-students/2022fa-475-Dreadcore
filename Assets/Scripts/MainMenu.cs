using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class MainMenu : MonoBehaviour
{
    public string enterGame;
    public GameObject optionScreen;
    public GameObject creditsScreen;
    public AudioSource audio;
    public AudioSource audioOptions;
    public AudioSource audioQuit;
    // Start is called before the first frame update
    void Start()
    {
        //Want to start next screen
        //Options button to open options
        //Quit to close the game
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(waitTime());

    }
    public void OpenCredits()
    {
        creditsScreen.SetActive(true);
    }
    public void closeCredits()
    {
        creditsScreen.SetActive(false);
    }
    public void OpenOptions()
    {
        optionScreen.SetActive(true);
    }
    public void CloseOptions()
    {
        optionScreen.SetActive(false);
    }
    public void QuitGame()
    {
        StartCoroutine(quitTime());
    }
    public void buttonSound()
    {
        audio.Play();
        //DontDestroyOnLoad(audio);
    }
    IEnumerator waitTime()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(enterGame);
    }
    IEnumerator quitTime()
    {
        yield return new WaitForSeconds(.2f);
        Application.Quit();

    }
}

