using UnityEngine;

public class OrbitStrength : MonoBehaviour
{
    public Transform playerTransform;
    public float strength;
    public float maxRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // only run the code if the player is in the planets range.
        if (Vector3.Magnitude(playerTransform.position - transform.position) <= maxRange)
        {
            // Find the original magnitude of the distance between the player and the planet for later use.
            float originalMagnitude = Vector3.Magnitude(playerTransform.position - transform.position);

            // Getting the direction Vector from the player to the planet.
            Vector2 directionToPlanet = (playerTransform.position - transform.position).normalized;

            // Find the angle from the direction vector to the planet. Add 90 degrees to make it perpendicular.
            float angle = (Mathf.Atan2(directionToPlanet.y, directionToPlanet.x) * Mathf.Rad2Deg) + 90f;

            // Create a direction Vector based on the created perpendicular angle.
            Vector2 perpendicularDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            // Find the relative strength of the orbit based on the distance from the planet.
            float relativeStrength = strength * (maxRange - originalMagnitude) / maxRange;

            // move the player by that perpendicular direction based on the relative strength.
            playerTransform.position += (Vector3)perpendicularDirection * relativeStrength * Time.deltaTime;

            // Get the direction to the planet after moving the player.
            Vector2 newDirection = (playerTransform.position - transform.position).normalized;

            // Keep the player the same distance from the planet, while still moving them.
            playerTransform.position = transform.position + (Vector3)newDirection * originalMagnitude;
        }
    }
}
