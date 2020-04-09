using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public static PlayGame instance;

    void Awake()
    {
        instance = this;
    }

    public void Play()
    {
        Debug.Log("Loading Scene");
        SceneManager.LoadScene("SampleScene");
    }

}
