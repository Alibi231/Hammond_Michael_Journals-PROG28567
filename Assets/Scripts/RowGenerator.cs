using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class RowGenerator : MonoBehaviour
{
    public TMP_InputField squareNumberInput;
    private int squareCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateRow()
    {
        bool res = int.TryParse(squareNumberInput.text, out squareCount);
        if (res && squareCount > 0)
        {
            squareCount = int.Parse(squareNumberInput.text);
            for (int i = 0; i < squareCount; i++)
            {
                Vector3 position = new Vector3(i * .55f - 8, 0, 0);
                drawSquare(.5f, position, new Color(1, 1, 1), 3f);
            }
        } else
        {
            Debug.Log("Put in a valid positive integer");
        }
    }
    public void drawSquare(float size, Vector3 position, Color color, float time)
    {
        Debug.DrawLine(position + new Vector3(size / 2 * -1, size / 2 * -1), position + new Vector3(size / 2 * -1, size / 2, 0), color, time);
        Debug.DrawLine(position + new Vector3(size / 2 * -1, size / 2), position + new Vector3(size / 2, size / 2, 0), color, time);
        Debug.DrawLine(position + new Vector3(size / 2, size / 2), position + new Vector3(size / 2, size / 2 * -1, 0), color, time);
        Debug.DrawLine(position + new Vector3(size / 2, size / 2 * -1), position + new Vector3(size / 2 * -1, size / 2 * -1, 0), color, time);
    }
}
