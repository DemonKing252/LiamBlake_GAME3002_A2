using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BashToyController : MonoBehaviour
{
    // Custom class I made that controls all the common variables needed in different classes
    // examples include score & lives.
    [SerializeField]
    private GameManager gameMngr;

    [SerializeField]
    private int scoreWorthy;

    [SerializeField]
    private float bashToyPower;

    // The bash toy deflects velocity just like the regular bumpers do
    private void OnTriggerEnter(Collider other)
    {
        // Are we dealing with the pin ball?
        // I mean what else what it be, but it's still good practice
        if (other.gameObject.CompareTag("PinBall"))
        {

            FindObjectOfType<GameManager>().outerBumperSfx.Play();
            gameMngr.IncrementScore(scoreWorthy);

            transform.parent.GetComponent<MeshRenderer>().material = FindObjectOfType<GameManager>().inactiveBumper_Exp;

            Vector3 inComingRay = other.gameObject.GetComponent<Rigidbody>().velocity;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

            Vector3 normal = other.transform.position - transform.position;
            normal.Normalize();

            // Reflect goes here, just like the bumper:
            Vector3 reflectedVel = Vector3.Reflect(inComingRay, normal);


            Vector3 clampedVel = reflectedVel * bashToyPower;
            clampedVel = Vector3.ClampMagnitude(clampedVel, FindObjectOfType<GameManager>().frontBumpers);

            other.gameObject.GetComponent<Rigidbody>().velocity = clampedVel;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PinBall"))
        {
            transform.parent.GetComponent<MeshRenderer>().material = FindObjectOfType<GameManager>().activeBumper_Def;     
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 normal = GameObject.FindGameObjectWithTag("PinBall").transform.position - transform.position;
         normal.Normalize();

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.TransformPoint(normal));

        Vector3 vel = GameObject.FindGameObjectWithTag("PinBall").GetComponent<Rigidbody>().velocity;

        Vector3 r = Vector3.Reflect(vel, normal);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.TransformPoint(r));
    }
    
}
