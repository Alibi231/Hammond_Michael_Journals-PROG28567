using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class dotOperatorScript : MonoBehaviour
{
    public float redAngle = 0f;
    public float blueAngle = 0f;

    private Vector2 redVector;
    private Vector2 blueVector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(Vector2.zero, redVector, Color.red);
        Debug.DrawLine(Vector2.zero, blueVector, Color.blue);
        redVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad * redAngle), Mathf.Sin(Mathf.Deg2Rad * redAngle));
        blueVector = new Vector2(Mathf.Cos(Mathf.Deg2Rad * blueAngle), Mathf.Sin(Mathf.Deg2Rad * blueAngle));
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("The Dot Product Is " + Vector2.Dot(redVector, blueVector));
        }
    }
}
