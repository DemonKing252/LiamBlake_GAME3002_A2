using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Bumper Properties")]
    [SerializeField]
    public float frontBumpers = 20f;

    [SerializeField]
    public float backBumpers = 20f;

    [Space(2)]

    [Header("Post Rendering Materials")]

    [SerializeField]
    public Material inactiveBumper_Def;

    [SerializeField]
    public Material inactiveBumper_Exp;


    [Space(2)]

    [Header("Game Management")]

    [SerializeField]
    private GameObject gameOverCanvas;

    // Spawn point for pin ball if it goes below the flippers (just like how real pinball works):
    [SerializeField]
    private Transform pinballRespawnPoint;

    // Pin ball reference
    [SerializeField]
    private GameObject pinball;

    [SerializeField]
    private Text ballsRemainingUIRef;

    [SerializeField]
    private int balls;

    [SerializeField]
    private Text scoreUIRef;

    [SerializeField]
    private int score;

    [SerializeField]
    private AudioSource m_mainTheme;



    private void Start()
    {
        m_mainTheme.volume = GameSingleton.desiredMaster;
        m_mainTheme.Play();

        Time.timeScale = 1f;
        if (Utilities.scenesChanged == 0)
        {
            // The player shouldn't be able to load this scene first
            SceneManager.LoadScene("Menu");
        }

        // Initialize UI variables, techinally speaking setting the score is not needed because its set 
        // to zero from the UI anyway at the beggining but I still think its good practice.

        ballsRemainingUIRef.text = "x " + balls.ToString();
        scoreUIRef.text = "Score: " + score.ToString();
    }

    public void OnBallLost()
    {
        // Decrement number of balls remaining by 1, and update the UI text:
        balls--;
        ballsRemainingUIRef.text = "x " + balls.ToString();


        // Respawn pinball above the plunger:
        pinball.transform.position = pinballRespawnPoint.position;
        pinball.GetComponent<Rigidbody>().velocity = Vector3.zero;  // Lose all momentum

        // Lose scene here eventually:
        if (balls <= 0)
        {
            // After the player loses all of his pin balls, load the lose UI as an "overlap" over the game scene
            gameOverCanvas.SetActive(true);
            gameOverCanvas.GetComponent<GameOverController>().SetScore(score);
        }
    }

    public void IncrementScore(int addBy)
    {
        score += addBy;
        scoreUIRef.text = "Score: " + score.ToString();
    }
}
