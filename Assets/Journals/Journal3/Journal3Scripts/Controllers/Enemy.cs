using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform playerTransform;
    public float maxSpeed;
    public float accelerationTime;
    private float acceleration;

    public Vector2 currentVelocity;

    void Start()
    {
        acceleration = maxSpeed / accelerationTime;
    }

    void Update()
    {
        // Checks that the enemy isn't on top of the player
        if (transform.position != playerTransform.position) {
            /* Subtracts the current velocity from the direction vector to make it point towards the play with its velocity in mind. currentVelocity is normalized to prevent this
                from freezing the enemy if the currentVelocity is equal to the direction towards the player
            */
            Vector3 directionVector = ((playerTransform.position - transform.position).normalized - (Vector3)currentVelocity.normalized * .95f).normalized;
            //adds the direction vector multiplied by accelerationa and deltatime to the current velocity
            currentVelocity += (Vector2)directionVector * acceleration * Time.deltaTime;
        }

        //makes sure current velocity max's out at the 
        currentVelocity = Vector2.ClampMagnitude(currentVelocity, maxSpeed);
        //add the currentVelocity to the position
        transform.position += (Vector3)currentVelocity * Time.deltaTime;

    }

}
