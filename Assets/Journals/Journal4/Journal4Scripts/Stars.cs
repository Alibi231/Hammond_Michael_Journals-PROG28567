using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stars : MonoBehaviour
{
    public List<Transform> starTransforms;
    public float drawingTime;
    private float currentTime = 0;
    
    public Color color = Color.white;

    public int currentStar = 0;
    //private Vector3 currentPosition; This ended up being irrelevent for my coding
    private Vector3 startPosition;
    private Vector3 endPosition;

    void Start()
    {
        //Sets the start position to the first star and endPosition to the second
        startPosition = starTransforms[currentStar].position;
        endPosition = starTransforms[currentStar + 1].position;
    }

    // Update is called once per frame
    void Update()
    {
        DrawConstellation();
    }

    public void DrawConstellation()
    {
        // Adds delta time to the currentTime
        currentTime += Time.deltaTime;

        // Draws a linef from the startingPosition to the its current progress towards the endingPosition
        Debug.DrawLine(startPosition, Vector3.Lerp(startPosition, endPosition, currentTime / drawingTime), color, 99999999f);

        //Runs if the currentTime is greater than the DrawingTime
        if (currentTime > drawingTime)
        {
            // Resets current time and moves to the next star
            currentTime = 0;
            currentStar += 1;
            //Checks if the currentStar is the last star 
            if (currentStar == starTransforms.Count - 1)
            {
                // If it is, then current star is reset and a new color for the Debug.line is chosen
                currentStar = 0;
                color = new Color(Random.Range(.1f, 1f), Random.Range(.1f, 1f), Random.Range(.1f, 1f));
            }
            //Sets the starting position to the currentStar, and the ending position to the next star
            startPosition = starTransforms[currentStar].position;
            endPosition = starTransforms[currentStar + 1].position;
        }
    }
}
