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
            gameMngr.IncrementScore(scoreWorthy);

            transform.parent.GetComponent<MeshRenderer>().material.color = Color.green;
            transform.parent.localScale = new Vector3(1.2f, 1.0f, 1.2f);

            Vector3 inComingRay = other.gameObject.GetComponent<Rigidbody>().velocity;

            Vector3 normal = other.transform.position - transform.position;
            normal.Normalize();

            // Reflect goes here, just like the bumper:
            Vector3 reflectedVel = Vector3.Reflect(inComingRay, normal);

            other.gameObject.GetComponent<Rigidbody>().velocity = reflectedVel * bashToyPower;


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PinBall"))
        {
            Invoke("OnResetBashToy", 0.5f);
        }
    }
    private void OnResetBashToy()
    {
        transform.parent.GetComponent<MeshRenderer>().material.color = Color.yellow;
        transform.parent.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void OnDrawGizmos()
    {
        Vector3 normal = GameObject.FindGameObjectWithTag("PinBall").transform.position - transform.position;
        //print(normal);
        normal.Normalize();

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.TransformPoint(normal));

        Vector3 vel = GameObject.FindGameObjectWithTag("PinBall").GetComponent<Rigidbody>().velocity;

        Vector3 r = Vector3.Reflect(vel, normal);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.TransformPoint(r));
    }
}