using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStateTriggerController : MonoBehaviour
{
    // Custom class I made that controls all the common variables needed in different classes
    // examples include score & lives.
    [SerializeField]
    private GameManager gameMngr;

    private void OnTriggerEnter(Collider other)
    {
        // Reset ball transform back to plunger and lose a life
        // Note: I'm not destroying and re-instantiating the ball for a reason. Re-using memory is better than 
        // having to reallocate it again. A more viable option for reusing memory that I use often is 
        // "object pooling", but that's not needed since theres only one ball.

        // Verify that its the pinball colliding (important!)
        if (other.gameObject.CompareTag("PinBall"))
        {
            gameMngr.OnBallLost();
        }

    }
}
