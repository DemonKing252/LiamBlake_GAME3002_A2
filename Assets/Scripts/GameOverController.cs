using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField]
    private Text scoreUI;

    // Start is called before the first frame update
    void Start()
    {
        // Freeze the game
        Time.timeScale = 0f;
    }
    public void SetScore(int score)
    {
        scoreUI.text = "Your Score: " + score.ToString();
    }

    public void OnBackToMenu()
    {
        Utilities.scenesChanged++;
        SceneManager.LoadScene("Menu");
    }
}
