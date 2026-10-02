using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public Transform planetTransform;

    public float radius;

    public float speed;

    public int currentPoint = 0;

    public Vector3 orbitTarget;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = planetTransform.position + new Vector3(radius, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        OrbitalMotion(radius, speed, planetTransform);
    }

    public void OrbitalMotion(float radius, float speed, Transform target)
    {
        // Makes a list of points in the orbit.
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < 100; i++)
        {
            // Sets increment to 360 divided by the number of points to get the angle between each point.
            float increment = 360f / 20;

            // Sets angle to that increment multiplied by the iterator to get the angle of the current point.
            float angle = increment * i;

            // Uses cos and sin to turn that angle into a x and y value.
            float xPos = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float yPos = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            // Takes those x and y values and adds them to the earth position to get the final position of the point in orbit.
            Vector3 currentPosition = new Vector3(xPos, yPos, 0) + target.position;
            points.Add(currentPosition);
        }

        // Sets the orbit target to the current point in the list.
        orbitTarget = points[currentPoint];

        // Sets the direction of movement to the angle between the target and moon.
        Vector3 direction = (orbitTarget - transform.position).normalized;
        
        // Changes position based on direction speed and time.deltatime.
        transform.position += direction * speed * Time.deltaTime;
        
        // If you are close enough to the target, run the code.
        if (Vector3.Magnitude(transform.position - orbitTarget) < 0.05)
        {
            //Change the point by one, or reset it.
            currentPoint++;
            if (currentPoint >= points.Count - 1)
            {
                currentPoint = 0;
            }
        }
        
    }
}
