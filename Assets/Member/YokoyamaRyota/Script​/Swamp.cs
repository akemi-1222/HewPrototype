using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class Swamp : MonoBehaviour
{
    [SerializeField]
    private float sinkSpeed = 0.05f; // ’¾‚Þ‘¬“x
    [SerializeField]
    private float sinkDistance = 2.0f; // ’¾‚Þ‹——£
 
    private int contact = 0;
    float distance = 0.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            contact++;

            if (contact >= 3)
            {
                Sink();
            }
        }
    }

    private void Sink()
    {
        while(distance < sinkDistance)
        {
            float move = distance + sinkSpeed;

            transform.position = new Vector3(transform.position.x, transform.position.y - move, transform.position.z);

            distance += move;
        }

        Destroy(gameObject);
    }
}
