using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperController : MonoBehaviour
{
    [SerializeField]
    private GameObject leftFlipper;

    [SerializeField]
    private GameObject rightFlipper;


    [SerializeField]
    private float restRotation = 0f;

    [SerializeField]
    private float targetRotation = 0f;

    private JointSpring leftFlipperJoint;
    private JointSpring rightFlipperJoint;

    // Start is called before the first frame update
    void Start()
    {
        if (leftFlipper != null && rightFlipper != null)
        {
            leftFlipperJoint = leftFlipper.GetComponent<HingeJoint>().spring;
            rightFlipperJoint = rightFlipper.GetComponent<HingeJoint>().spring;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleUserInput();
    }

    private void HandleUserInput()
    {
        // Since the hinge joint is anchored to the top left/right corners, we can readjust the target angle.

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftFlipperJoint.targetPosition = targetRotation;
            leftFlipper.GetComponent<HingeJoint>().spring = leftFlipperJoint;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftFlipperJoint.targetPosition = restRotation;
            leftFlipper.GetComponent<HingeJoint>().spring = leftFlipperJoint;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightFlipperJoint.targetPosition = targetRotation;
            rightFlipper.GetComponent<HingeJoint>().spring = rightFlipperJoint;
        }
        else if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightFlipperJoint.targetPosition = restRotation;
            rightFlipper.GetComponent<HingeJoint>().spring = rightFlipperJoint;
        }


    }
}
