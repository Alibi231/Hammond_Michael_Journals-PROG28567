using UnityEngine;

public class LinecastScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position + transform.up * 20f);

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
        } 
    }
}
