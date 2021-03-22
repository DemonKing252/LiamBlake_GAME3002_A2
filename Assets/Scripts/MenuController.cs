using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnStart()
    {
        Utilities.scenesChanged++;
        SceneManager.LoadScene("Game");
    }
    public void OnHowToPlay()
    {
        Utilities.scenesChanged++;
        SceneManager.LoadScene("HowToPlay");
    }
    public void OnQuit()
    {
        // Check if the unity editor is open, quitting the application is done differently within editor/build

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        
    }
}
