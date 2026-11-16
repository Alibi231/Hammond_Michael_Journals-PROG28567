using UnityEngine;

public class IgnoreCollisionScript : MonoBehaviour
{
    public Vector2 moveDir;
    public GameObject player;
    public GameObject enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>(), true);
        player.GetComponent<Rigidbody2D>().AddForce(moveDir, ForceMode2D.Impulse);
        enemy.GetComponent<Rigidbody2D>().AddForce(-moveDir, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
