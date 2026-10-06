using UnityEngine;

public class Turret : MonoBehaviour
{
    public float angularSpeed = 90f; // degrees per second
    public Transform enemyPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.forward, angularSpeed * Time.deltaTime);
        Debug.DrawLine(transform.position, transform.position + transform.up * 30, Color.red);
        //Debug.Log(Facing(transform.up, enemyPosition.position));

        LookAt();
    }

    public string Facing(Vector2 turretUp, Vector2 enemyPos)
    {
        Vector2 enemyDirVector = enemyPos.normalized;

        float dot =  Vector2.Dot(turretUp, enemyDirVector);

        if (dot > 0)
        {
            return "In Front";

        }
        else{
            return "Behind";
        }

    }

    public void LookAt()
    {
        Vector2 directionToTarget = (enemyPosition.position - transform.position).normalized;
        float upAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        float directionAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        float deltaAngle = Mathf.DeltaAngle(upAngle, directionAngle);

        if (Vector2.Dot(transform.up, directionToTarget) < 0.99999)
        {
            switch (Mathf.Sign(deltaAngle))
            {
                case 1:
                    transform.Rotate(Vector3.forward, angularSpeed * Time.deltaTime);
                    break;

                case -1:
                    transform.Rotate(Vector3.forward, -angularSpeed * Time.deltaTime);
                    break;
            }
        }
        

        //Debug.Log(deltaAngle);
    }
}
