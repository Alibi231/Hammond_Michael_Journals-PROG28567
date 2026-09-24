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

        if ((destination - transform.position).magnitude <= arrivalDistance)
        {
            Vector3 destinationDistance = Vector3.zero;
            destinationDistance.x = Random.Range(-1f, 1f);
            destinationDistance.y = Random.Range(-1f, 1f);
            destinationDistance.Normalize();
            destinationDistance *= maxFloatDistance;
            destination = destinationDistance + transform.position;
        }

        direction = (destination - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;


    }
}
