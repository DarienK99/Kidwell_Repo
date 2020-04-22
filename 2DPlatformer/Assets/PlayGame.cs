using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public static PlayGame instance;

    public GameObject controls;
    public GameObject exitCheck;
    public GameObject Menu;
    public GameObject background;

    void Awake()
    {
        instance = this;
    }

    public void Play()
    {
        Debug.Log("Loading Scene");
        background.SetActive(false);
        SceneManager.LoadScene("scene1");
    }

    public void Controls()
    {
        Debug.Log("Load Controls");
        Menu.SetActive(false);
        controls.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("Exiting Application");
        Application.Quit();
    }

    public void doublecheckExit()
    {
        Debug.Log("Asking if you want to exit fo' real");
        Menu.SetActive(false);
        exitCheck.SetActive(true);
    }

    public void ReturnToMenu()
    {
        Debug.Log("Returning to menu");
        exitCheck.SetActive(false);
        controls.SetActive(false);
        Menu.SetActive(true);
    }

}
