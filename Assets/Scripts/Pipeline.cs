using UnityEngine;
using UnityEngine.InputSystem;

public class Pipeline : MonoBehaviour
{
    private bool mouseDown = false;
    private float totalMagnitude = 0;
    private float timeInterval = 0.1f;
    private Vector2 initialPos = new (0, 0);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDown = true;
            totalMagnitude = 0;
            timeInterval = 0.1f;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }

        if (mouseDown == true)
        {
            timeInterval -= Time.deltaTime;
            if (timeInterval < 0)
            {
                timeInterval = .1f;
                Vector2 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Debug.DrawLine(initialPos, currentMousePos, Color.white, 999f);
                Vector2 resultVector = currentMousePos - initialPos;
                totalMagnitude += Mathf.Sqrt(Mathf.Pow(resultVector.x, 2) + Mathf.Pow(resultVector.y, 2));
                initialPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

        }
        else
        {
            if (totalMagnitude > 0)
            {
                Debug.Log("Total Pipe Length: " + totalMagnitude);
                totalMagnitude = 0;
                timeInterval = 0.1f;
            }
        }
    }
}
