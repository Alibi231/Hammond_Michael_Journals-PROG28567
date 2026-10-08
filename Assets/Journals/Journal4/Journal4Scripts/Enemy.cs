using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public float fireTimer;
    public float currentTime;
    public GameObject bullet;

    public Transform playerTransform;

    public Player playerScript;

    private void Start()
    {
        currentTime = fireTimer;

    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = fireTimer;
            // set currentBullet to an instantiated bullet at the players position.
            GameObject currentBullet = Instantiate(bullet, transform.position, Quaternion.identity);

            // Find the distance between the player and the enemy.
            Vector2 distanceToPlayer = (playerTransform.position - transform.position);

            // Find the time it will take for the bullet to get to the player.
            float timeToPlayer = distanceToPlayer.magnitude / currentBullet.GetComponent<bulletScript>().speed;

            // Add the players currentVelocity * the time it will take for the bullet to get to the player to the players position;
            Vector2 playerPos = playerTransform.position + (Vector3)playerTransform.GetComponent<Player>().currentVelocity * timeToPlayer;

            // Set the direction to the normalized vector between the calculated player position and the enemy
            Vector2 dir = (playerPos - (Vector2)transform.position).normalized;



            // Set the bullets direction to the direction vector.
            currentBullet.GetComponent<bulletScript>().direction = dir;

            // Destroy the bullet after 5 seconds to prevent it from staying around forever off screen.
            Destroy(currentBullet, 5);
        }

    }

}
