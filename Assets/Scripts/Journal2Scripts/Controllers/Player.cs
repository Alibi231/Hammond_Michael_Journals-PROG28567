using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Player : MonoBehaviour
{
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public List<Transform> asteroidTransforms;

    //Vector2 that stores the offset of the bomb from the player.
    public Vector2 bombOffset;

    public float bombTrailSpacing;

    public float cornerDistance;

    public int numberOfTrailBombs;

    public float warpValue;

    public float maxRange;
    
    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.W))
        {
            //Moves the player to a distance between the player and enemy, based on the warpValue variable.
            WarpPlayer(enemyTransform, warpValue);
        }
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
        //Make a vector that is up and right, normalized to have a value of 1.
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

    public void WarpPlayer(Transform target, float ratio)
    {
        //Sets the position to a spot between the position of the player and enemy, based on the ratio value.
        transform.position = Vector3.Lerp(transform.position, target.position, ratio);
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
