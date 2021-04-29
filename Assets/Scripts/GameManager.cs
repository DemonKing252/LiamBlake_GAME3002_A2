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

    [Header("Post Processing Materials")]

    [SerializeField]
    public Material inactiveBumper_Def;

    [SerializeField]
    public Material inactiveBumper_Exp;

    [SerializeField]
    public Material activeBumper_Def;

    [Space(2)]
    [Header("Sound effects")]

    [SerializeField]
    public AudioSource flipperSfx;

    [SerializeField]
    public AudioSource triangleBumperSfx;

    [SerializeField]
    public AudioSource outerBumperSfx;


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

    private GameObject[,] triangleAnimator = new GameObject[2,6];

    [SerializeField]
    private Material triangleMat;

    [SerializeField]
    private Material triangleOffMat;


    private int index = 0;

    IEnumerator _TurnOff(float delay, int triangleIndex)
    {
        yield return new WaitForSeconds(delay);

        triangleAnimator[0, triangleIndex].GetComponent<MeshRenderer>().material = triangleOffMat;
        triangleAnimator[1, triangleIndex].GetComponent<MeshRenderer>().material = triangleOffMat;
    }

    public void _SetStateOn()
    {
        StartCoroutine(_TurnOff(0.25f, index));

        triangleAnimator[0, index].GetComponent<MeshRenderer>().material = triangleMat;
        triangleAnimator[1, index].GetComponent<MeshRenderer>().material = triangleMat;

        index++;
        if (index >= 6)
            index = 0;

    }

    private void Start()
    {

        for(int i = 0; i < 6; i++)
        {
            triangleAnimator[0, i] = GameObject.FindGameObjectsWithTag("TriangleAnimator")[i];
            triangleAnimator[1, i] = GameObject.FindGameObjectsWithTag("TriangleAnimator2")[i];
        }

        InvokeRepeating("_SetStateOn", 0f, 0.25f);

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
