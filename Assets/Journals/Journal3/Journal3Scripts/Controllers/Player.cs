using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Player : MonoBehaviour
{
    
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    //Vector2 that stores the offset of the bomb from the player.
    [Space(10)]
    public Vector2 bombOffset;

    public float bombTrailSpacing;

    public float cornerDistance;

    public int numberOfTrailBombs;

    [Space(10)]
    public float warpValue;

    public float maxRange;

    public float speed;

    public float maxSpeed;

    public Vector2 currentVelocity;

    public float accelerationTime;

    public float decelerationTime;

    private float acceleration;

    private float deceleration;

    [Space(10)]
    public Vector2 enemyCurrentVelocity;

    public float enemyMaxSpeed;
    public float enemyAccelerationTime;

    private float enemyAcceleration;

    private void Start()
    {
        // Acceleration is set to the maxSpeed divided by the acceleration time, to make the player accelerate correctly
        acceleration = maxSpeed / accelerationTime;

        enemyAcceleration = enemyMaxSpeed / enemyAccelerationTime;

        // Decelleration is set to the maxSpeed divided by the acceleration time, to make the player decellerates correctly
        deceleration = maxSpeed / decelerationTime;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        EnemyMovement();
        DetectAsteroids(maxRange, asteroidTransforms);

        //Checks if the player has pressed the b button
        if (Input.GetKeyDown(KeyCode.B))
        {
            //Calls the SpawnBomb method using the public bombOffset variable.
            SpawnBombAtOffset(bombOffset);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            //Calls the SpawnBombTrail method with a y offset of 1, using bombTrailspacing for space between bombs and numberOfTrailBombs for the number of bombs.
            SpawnBombTrail(bombTrailSpacing, numberOfTrailBombs);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //Calls the SpawnBombOnRandomCorner method to spawn the bomb in a random corner using the cornerDistance variable for distance.
            SpawnBombOnRandomCorner(cornerDistance);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Moves the player to a distance between the player and enemy, based on the warpValue variable.
            WarpPlayer(enemyTransform, warpValue);
        }
    }

    #region Movement
    public void PlayerMovement()
    {
        //Camera bounds are set using the width and height of the screen and converting them from screen to world points
        Vector2 cameraBounds = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        //Check if the A key is being held down
        if (Input.GetKey(KeyCode.A))
        {
            // Only run the code if the player is inside the camera's bounds
            if (transform.position.x > -cameraBounds.x)
            {
                // Change the velocity by the acceleration value.
                currentVelocity += Vector2.left * acceleration * Time.deltaTime;
            } else
            {
                // Velocity is set to 0 if the player leaves the screen bounds
                currentVelocity.x = 0; // code in the else should be running at all times now in it's own seperate if statement
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < cameraBounds.x)
            {
                currentVelocity += Vector2.right * acceleration * Time.deltaTime;
            } else
            {
                currentVelocity.x = 0;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.y < cameraBounds.y)
            {
                currentVelocity += Vector2.up * acceleration * Time.deltaTime; 
            }
            else
            {
                currentVelocity.y = 0;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.y > -cameraBounds.y)
            {
                currentVelocity += Vector2.down * acceleration * Time.deltaTime;
            }
            else
            {
                currentVelocity.y = 0;
            }
        }

        // Checks if the player is not currently holding any of the direction keys
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            // Stores the current magnitude as a float
            float currentMagnitude = currentVelocity.magnitude;
            // Reduces the currentMagnitude by the deceleration multiplied by deltaTime
            currentMagnitude -= deceleration * Time.deltaTime;

            //If the value is inceredibly small, it is set to absolute zero.
            if (currentMagnitude < 0.0005)
            {
                currentMagnitude = 0;
            }
            //Normalizes the players current velocity
            currentVelocity.Normalize();
            //Multiplies it by currentMagnitude to lower the players current velocity
            currentVelocity *= currentMagnitude;
        }

        //if (currentVelocity.magnitude  >= maxSpeed) Initial solution before learning about clamp
        //{
        //    currentVelocity.Normalize();
        //    currentVelocity *= maxSpeed;
        //}

        // Maximum value of the velocity vector is clamped to the maxSpeed of the player
        currentVelocity = Vector2.ClampMagnitude(currentVelocity, maxSpeed);

        // The currentVelocity Vector is made into a vector 3 and then added to the players position
        transform.position += (Vector3)currentVelocity * Time.deltaTime;
    }

    public void WarpPlayer(Transform target, float ratio)
    {
        //Sets the position to a spot between the position of the player and enemy, based on the ratio value.
        transform.position = Vector3.Lerp(transform.position, target.position, ratio);
    }

    #endregion
    //Vector2 directionVector = ((transform.position - enemyTransform.position).normalized - (Vector3)enemyCurrentVelocity.normalized * .95f).normalized;

    public void EnemyMovement()
    {
        // Checks if the enemy is near the player
        if (enemyTransform.position != transform.position){
            // point the enemy towards the player with respect to it's current velocity
            Vector2 directionVector = ((transform.position - enemyTransform.position).normalized - (Vector3)enemyCurrentVelocity.normalized * .95f).normalized; ;
            // adds the acceleration in that direction to the players current velocity
            enemyCurrentVelocity += directionVector * enemyAcceleration * Time.deltaTime;
        }

        // clamps the enemy's current velocity to it's max speed;
        enemyCurrentVelocity = Vector2.ClampMagnitude(enemyCurrentVelocity, enemyMaxSpeed);
        // adds the enemy's current velocity to it's position
        enemyTransform.position += (Vector3)enemyCurrentVelocity * Time.deltaTime;

    }

    //Spawns a bomb at an offset
    public void SpawnBombAtOffset(Vector3 inOffset)
    {
        //Spawns a bomb using the player position + the specified offset
        Instantiate(bombPrefab, transform.position + inOffset, Quaternion.identity);
    }

    //Spawns multiple bombs in a line below the player.
    public void SpawnBombTrail(float inBombSpacing, int inNumberOfBombs)
    {
        //This does a for loop through 
        for (int i = 0; i < inNumberOfBombs; i++)
        {
            // one is added to i because it starts at 0, so the offset would be ignored for the first mine
            SpawnBombAtOffset(Vector3.down * inBombSpacing * (i + 1));
        }
    }

    public void SpawnBombOnRandomCorner(float inDistance)
    {
        //Make a vector that is up and right, normalized to have a value of 1 so it is a direction vector.
        Vector3 corner = (Vector2.up + Vector2.right).normalized;

        // Used this discussion to refresh me on Random.Range https://discussions.unity.com/t/random-int-value/7800.
        //Uses random to have a 50/50 chance to multiple the x component by -1, changing it from right to left.
        if (Random.Range(1, 3) == 1)
        {
            corner.x *= -1;
        }

        //Uses random to have a 50/50 chance to multiple the y component by -1, changing it from up to down.
        if (Random.Range(1, 3) == 1)
        {
            corner.y *= -1;
        }

        //Spawns the bomb using the players position, then the corner vector multiplied by the distance parameter.
        SpawnBombAtOffset(corner * inDistance);
    }


    public void DetectAsteroids(float inMaxRange, List<Transform> inAsteroids)
    {
        //Loops through to run the code for every individual asteroud
        for (int i = 0; i < inAsteroids.Count; i++)
        {
            //Checks if the distance between the player and asteroid is lower than the maxRange parameter.
            if (Vector3.Distance(transform.position, inAsteroids[i].position) <= inMaxRange)
            {
                //Creates a vector as the distance between the player and asteroid, and makes it a direction vector by normalizing it
                Vector3 directionVector = (inAsteroids[i].position - transform.position).normalized;
                //Draws a line using that direction vector multiplied by the max range, added to the transform.position.
                Debug.DrawLine(transform.position, transform.position + directionVector * inMaxRange, Color.green);
            }
        }

    }
}
