using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonactiveControllerScript : MonoBehaviour
{

    // Custom class I made that controls all the common variables needed in different classes
    // examples include score & lives.
    [SerializeField]
    private float power = 3f;

    [SerializeField]
    private GameManager gameMngr;

    // Amount of points this game object is worth
    [SerializeField]
    private int scoreWorthy;

    private Vector3 targetLerp = Vector3.one;

    private void Update()
    {
        transform.parent.localScale = Vector3.Lerp(transform.parent.localScale, targetLerp, Time.deltaTime * 90f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Are we dealing with the pin ball?
        // I mean what else what it be, but it's still good practice
        if (other.gameObject.CompareTag("PinBall"))
        {
            if (gameMngr != null)
            {
                gameMngr.IncrementScore(scoreWorthy);
            }

            transform.parent.GetComponent<MeshRenderer>().material = FindObjectOfType<GameManager>().inactiveBumper_Exp;
            //transform.parent.localScale = new Vector3(1.2f, 1.0f, 1.2f);
            targetLerp = new Vector3(1.2f, 1.0f, 1.2f);

            Vector3 inComingRay = other.gameObject.GetComponent<Rigidbody>().velocity;

            Vector3 normal = other.transform.position - transform.position;
            normal.Normalize();


            Vector3 clampedVel = inComingRay * power;
            clampedVel = Vector3.ClampMagnitude(clampedVel, FindObjectOfType<GameManager>().frontBumpers);

            print("Clamped: " + clampedVel.magnitude);
            // Reflect goes here, just like the bumper:
            Vector3 reflectedVel = Vector3.Reflect(clampedVel, normal);

            other.gameObject.GetComponent<Rigidbody>().velocity = reflectedVel;


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PinBall"))
        {
            transform.parent.GetComponent<MeshRenderer>().material = FindObjectOfType<GameManager>().inactiveBumper_Def;
            targetLerp = Vector3.one;
            //Invoke("OnResetBashToy", 0.2f);
        }
    }

}
