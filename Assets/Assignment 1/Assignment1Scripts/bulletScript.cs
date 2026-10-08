using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 5f;

    Vector3 cameraBounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.up = direction;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // Found this method of keeping the player in the viewPort from stackOverflow;
        if (Camera.main.WorldToViewportPoint(transform.position).x > 1 || Camera.main.WorldToViewportPoint(transform.position).x < 0)
        {
            // If the bullet hits the sides of the screen, reverse the direction of the bullet.
            direction.x *= -1;
            // Updates the bullets rotation to reflect it's new direction.
            transform.up = direction;
        }

        if (Camera.main.WorldToViewportPoint(transform.position).y > 1 || Camera.main.WorldToViewportPoint(transform.position).y < 0)
        {
            // If the bullet hits the top or bottom of the screen, reverse the direction of the bullet.
            direction.y *= -1;
            // Updates the bullets rotation to reflect it's new direction.
            transform.up = direction;
        }
    }
}
