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
    private bool inRestPos = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleUserInput();
        UpdateSpring();
    }
    private void Update()
    {
        // Plunger has a tendancy to get resistance from the pinball, putting its y position off from the correct
        // inclination. Freezing the y position doesn't fix this issue since it acts a world point freeze and
        // the board is tilted 10 degrees, so its y postion will be different with any z-value.
        if (transform.localPosition.y != 0.0f)
        {
            Vector3 local = transform.localPosition;
            local.y = 0f;
            transform.localPosition = local;
        }

    }

    private void HandleUserInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Spring forces will fight the plunger from retracting, so we need to turn kinematic off until we release the spring
            GetComponent<Rigidbody>().isKinematic = true;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            // Retract the spring while 'S' using linear interpolation
            // This doesn't go against the rules of modifying the transform instead of the rigidbody:

            transform.localPosition = Vector3.Lerp(transform.localPosition, maximumRetraction, Time.deltaTime);
        }
        else if (inRestPos)
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }


    private void UpdateSpring()
    {
        // Check if the plunger already passes the resting position:
        if (transform.localPosition.z <= restingPosition.z)
        {
        
            inRestPos = (transform.localPosition.z + 0.1f <= restingPosition.z);
        
            // To calculate displacement we need (xf - xi), which really can be calculated with distance
            // Need to convert this vector to world space first:
            float dist = (restingPosition - transform.localPosition).magnitude;
        

            // Spring Force:
            // F = -k * x
            // Where k is the spring constant, and x is the displacement from the resting point of the spring
        
        
            // How do we calculate spring constant?
            // k = m * g / x
        
            springConstant = GetComponent<Rigidbody>().mass * Physics.gravity.y / dist;
        
            // with spring constant, we can calculate the required force:
            force = -springConstant * (restingPosition - transform.localPosition);
            
            // Damping = -b * (vNow - vPrev)
            force -= GetComponent<Rigidbody>().velocity - velPreviousFrame;
            force.y = 0f;
            print(force);

            // Using relative force, since the y-position needs to be the same relative to the board inclination
            GetComponent<Rigidbody>().AddRelativeForce(force, ForceMode.Acceleration);
            velPreviousFrame = GetComponent<Rigidbody>().velocity;

        }
        else
        {
            /// Did the plunger pass the resting position? If so, stop it
           
            // Alternate way of doing the spring top, rather than using a collider to stop the plunger,
            // This table is tilted 15 degrees so I can't freeze its y position to solve that problem! The plunger has to move parrelel to the ground!
        
            // Setup the spring for the next round
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().isKinematic = true;
        }
       
    }

}
