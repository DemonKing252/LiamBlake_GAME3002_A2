using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField]
    AudioSource theme;

    private void Start()
    {
        theme.Play();
    }

    [SerializeField] private Transform respawnPoint;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PinBall"))
        {
            other.transform.position = respawnPoint.position;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;  // Lose all momentum
        }
    }
}
