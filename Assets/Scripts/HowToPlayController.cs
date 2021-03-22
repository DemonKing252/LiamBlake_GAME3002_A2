using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayController : MonoBehaviour
{
    public void Start()
    {
        if (Utilities.scenesChanged == 0)
        {
            // The player shouldn't be able to load this scene first
            SceneManager.LoadScene("Menu");
        }
    }

    public void OnBackToMenu()
    {
        Utilities.scenesChanged++;
        SceneManager.LoadScene("Menu");
    }
}
