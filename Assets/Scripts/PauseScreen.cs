using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
   public static bool isPaused = false;
   public static bool isOptions = false;
   public GameObject pauseMenuUI;
   public string GoToMain;
   public GameObject optionScreen;
   public AudioSource audioResume;
   public AudioSource audioMainMenu;
   public AudioSource audioOptions;
   public AudioSource audioQuit;



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOptions == false)
            {
                if(isPaused)
                    {
                        escresumeGame();
                    }
                    else
                    {
                        pauseGame();
                    }
            }
        }
    }
    public void resumeGame()
    {
        StartCoroutine(waitTimes());
        //pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        //isPaused = false;
    }
    public void escresumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void pauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ReturnToMain()
    {
        //SceneManager.LoadScene(mainMenuScene);
    }
    public void LoadMenu()
    {
         Time.timeScale = 1f;
         Invoke("sendMain",.2f);
         //SceneManager.LoadScene(GoToMain);
         UnityEngine.Debug.Log("is loading to main menu working?");
    }
    public void Quit()
    {
        Time.timeScale = 1f;
        Invoke("testQuit",.2f);
        UnityEngine.Debug.Log("is asadsadsaworking?");
    }
     public void OpenOptions()
    {
        optionScreen.SetActive(true);
        isOptions = true;
    }
      public void CloseOptions()
    {
        optionScreen.SetActive(false);
        isOptions = false;

    }
    public void buttonSound()
    {
        audioResume.Play();
        //DontDestroyOnLoad(audioResume);
    }
    IEnumerator waitTimes()
    {
        yield return new WaitForSecondsRealtime(.2f);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    /*IEnumerator quitTime()
    {
        yield return new WaitForSecondsRealtime(.2f);
        Application.Quit();
        UnityEngine.Debug.Log("is quit working?");

    }*/
    public void testQuit()
    {
        Application.Quit();
    }
    public void sendMain()
    {
        SceneManager.LoadScene(GoToMain);
    }


}
