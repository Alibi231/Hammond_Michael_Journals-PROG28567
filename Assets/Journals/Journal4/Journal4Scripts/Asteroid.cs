using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed;
    public float arrivalDistance;
    public float maxFloatDistance;

    public Vector3 destination;

    public Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 destinationDistance = Vector3.zero;
        destinationDistance.x = Random.Range(-1f, 1f);
        destinationDistance.y = Random.Range(-1f, 1f);
        destinationDistance.Normalize();
        destinationDistance *= maxFloatDistance;
        destination = destinationDistance + transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidMovement();
    }

    public void AsteroidMovement()
    {
        // Checks if the asteroid is within arrivalDistance of it's destination
        if ((destination - transform.position).magnitude <= arrivalDistance)
        {
            // Make an empty vector for destination distance
            Vector3 destinationDistance = Vector3.zero;
            //Gives it a random x and y between 1 to -1
            destinationDistance.x = Random.Range(-1f, 1f);
            destinationDistance.y = Random.Range(-1f, 1f);
            // Normalizes the vector to make it a direction Vector
            destinationDistance.Normalize();
            // Multiplies it by maxFloatDistance to give it the correct distance
            destinationDistance *= maxFloatDistance;
            // Adds that vector to the asteroids position to make it's destination relative to it's original position
            destination = destinationDistance + transform.position;
        }
        //Points the asteroid towards its destination
        direction = (destination - transform.position).normalized;
        //Moves asteroid towards it's destination based on its moveSpeed
        transform.position += direction * moveSpeed * Time.deltaTime;


    }
}
