using UnityEngine;

public class TorqueScript : MonoBehaviour
{
    public GameObject obj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // TEST 1. Torque Rotates an object around its center, with a force depending on the inputted float.
        //obj.GetComponent<Rigidbody2D>().AddTorque(-999f);

        /* 
        * TEST 2. Used the overload for force type. No idea what it does other than the fact Force seems Weaker than Impulse.
        * Tried reading a discussion on the differece here https://discussions.unity.com/t/difference-between-forcemode-force-acceleration-impulse-velocitychange/103470/4
        * Did nothing but get me lost, one squares time and one doesn't but why does that difference matter?
        */
        obj.GetComponent<Rigidbody2D>().AddTorque(5f, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
