using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlungerController : MonoBehaviour
{
    [SerializeField]
    private Vector3 restingPosition;

    [SerializeField]
    private Vector3 maximumRetraction;

    // Tilt of the board will matter for calculations
    [SerializeField]
    [Range(-30f, 30f)]
    private float worldTilt = 0f;
    
    private Vector3 force = Vector3.zero;
    private float springConstant = 0f;

    private Vector3 velPreviousFrame = Vector3.zero; 
    private Vector3 worldPointRestPosition;
    private Vector3 worldPointMaximumRetraction;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Since the whole board is tilted 15 degrees, we need to use trig to calculate the plungers ideal y rest position because it won't be the same as
        // its current position

        // given: theta and opposite:

        // tan(theta) = opp/adj
        // adj * tan(theta) = opp

        // opp = adj * tan(theta)

        worldPointRestPosition.y = worldPointRestPosition.z * Mathf.Tan(worldTilt);

        // World point of our rest position:
        worldPointRestPosition = transform.TransformPoint(restingPosition);
        worldPointMaximumRetraction = transform.TransformPoint(maximumRetraction);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateSpring();
        HandleUserInput();

    }
    private void HandleUserInput()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Spring forces will fight the plunger from retracting, so we need to turn kinematic off until we release the spring
            GetComponent<Rigidbody>().isKinematic = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            // Retract the spring while 'S' using linear interpolation
            // 
            transform.position = Vector3.Lerp(transform.position, worldPointMaximumRetraction, Time.deltaTime);
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }


    private void UpdateSpring()
    {
        
        if (transform.position.z <= worldPointRestPosition.z)
        {
            // To calculate displacement we need (xf - xi), which really can be calculated with distance
            // Need to convert this vector to world space first:
            float dist = (worldPointRestPosition - transform.position).magnitude;


            // Spring Force:
            // F = -k * x
            // Where k is the spring constant, and x is the displacement from the resting point of the spring


            // How do we calculate spring constant?
            // k = F / x
            // To calculate force, we need mass * gravity

            springConstant = GetComponent<Rigidbody>().mass * Physics.gravity.y / dist;

            // with spring constant, we can calculate the required force:
            force = -springConstant * (worldPointRestPosition - transform.position);

            // Damping = -b * (vNow - vPrev)
            force -= GetComponent<Rigidbody>().velocity - velPreviousFrame;

            GetComponent<Rigidbody>().AddForce(force, ForceMode.Acceleration);
            velPreviousFrame = GetComponent<Rigidbody>().velocity;
        }
        else
        {
            // Alternate way of doing the spring top, rather than using a collider to stop the plunger,
            // I just zero its velocity, beacause I had issues where the plunger would shift its y position a bit putting it off centre
            // This table is tilted 15 degrees so I can't freeze its y position! The plunger has to move parrelel to the ground!

            // Setup the spring for the next round
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(worldPointRestPosition, 0.5f);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, worldPointRestPosition);

    }
}
