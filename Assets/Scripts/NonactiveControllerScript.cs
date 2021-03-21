using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonactiveControllerScript : MonoBehaviour
{
    
    // Custom class I made that controls all the common variables needed in different classes
    // examples include score & lives.
    [SerializeField]
    private GameManager gameMngr;

    // Amount of points this game object is worth
    [SerializeField]
    private int scoreWorthy;

    private void OnCollisionEnter(Collision collision)
    {
        // Note: This is not an game object that deflects velocity, look in the BumperController.cs script to find out how 
        // I'm calculating reflected velocity.
        if (gameMngr != null)
        {
            gameMngr.IncrementScore(scoreWorthy);
        }
    }

}
