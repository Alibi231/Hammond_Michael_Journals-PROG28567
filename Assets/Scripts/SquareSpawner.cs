using UnityEngine;
using  UnityEngine.UIElements;

public class SquareSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Vector3 mousePos;
    public float size = 1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        size += Input.mouseScrollDelta.y * .1f;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            drawSquare(size, mousePos, new Color(1, 1, 1), 9999f);
            
        } else
        {
            drawSquare(size, mousePos, new Color(1, 1, 1, .25f), Time.deltaTime);
        }
    }

    public void drawSquare(float size, Vector3 position, Color color, float time)
    {
        Debug.DrawLine(position + new Vector3(size / 2 * -1, size / 2 * -1), position + new Vector3(size / 2 * -1, size / 2), color, time);
        Debug.DrawLine(position + new Vector3(size / 2 * -1, size / 2), position + new Vector3(size / 2, size / 2), color, time);
        Debug.DrawLine(position + new Vector3(size / 2, size / 2), position + new Vector3(size / 2, size / 2 * -1), color, time);
        Debug.DrawLine(position + new Vector3(size / 2, size / 2 * -1), position + new Vector3(size / 2 * -1, size / 2 * -1), color, time);
    }
}
