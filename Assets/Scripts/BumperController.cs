using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperController : MonoBehaviour
{
    [SerializeField]
    private GameManager gameMngr;

    [SerializeField]
    private float bumperPower = 0f;

    [SerializeField]
    private int scoreWorthy;

    private Vector3 faceNormal;
    private Vector3 targetLerp = new Vector3(1f, 2f, 3f);


    // Start is called before the first frame update
    void Start()
    {
        faceNormal = transform.right;
    }

    [System.Obsolete]
    void Update()
    {
        transform.FindChild("Mesh").transform.localScale = Vector3.Lerp(transform.FindChild("Mesh").transform.localScale, targetLerp, Time.deltaTime * 90f);
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        // Read up on this:
        // This gives you a perfect visual representation of how this works
        // https://docs.unity3d.com/ScriptReference/Vector3.Reflect.html


        // Verify that its the pinball colliding (important!)
        if (other.gameObject.CompareTag("PinBall"))
        {
            if (gameMngr != null)
            {
                gameMngr.IncrementScore(scoreWorthy);
            }
            // 20% bigger:
            targetLerp = new Vector3(1f * 1.2f, 2f, 3f * 1.2f);

            transform.FindChild("Mesh").GetComponent<Renderer>().material.color = Color.green;
            // Reflect the balls velocity using the normal vector 

            // Need to rotate the ball velocity vector with respect of the bumpers rotation, otherwise
            // it will reflect it the wrong way (if its rotated 30 degrees on y for example), the vector will 
            // appear to be rotated 30 degrees the opposite way, which will deflect it the wrong way.


            // Incoming velocity
            Vector3 incomingRay = other.gameObject.GetComponent<Rigidbody>().velocity;

            // Reflect the incoming ray with the face normal
            Vector3 reflectedVelocity = Vector3.Reflect(incomingRay, faceNormal);

            // Set the balls velocity to what it was originally, but now its reflected 
            // AND since this is a bumper has a bounce force, we multiply that vector 
            // by the bumper power
            
            other.gameObject.GetComponent<Rigidbody>().velocity = reflectedVelocity * bumperPower;

        }


    }
    [System.Obsolete]
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("PinBall"))
        {
            targetLerp = new Vector3(1f, 2f, 3f);
            transform.FindChild("Mesh").GetComponent<Renderer>().material.color = Color.yellow;
        }
    }
    private void OnDrawGizmos()
    {
        // Visual Cue for normal vector
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.TransformPoint(faceNormal));
    }

}
