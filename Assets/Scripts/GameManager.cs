using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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


    private void Start()
    {
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

        // Lose scene here eventually:
    }

    public void IncrementScore(int addBy)
    {
        score += addBy;
        scoreUIRef.text = "Score: " + score.ToString();
    }
}
