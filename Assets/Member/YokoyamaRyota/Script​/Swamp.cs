using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.LowLevelPhysics2D.PhysicsShape;

public class Swamp : MonoBehaviour
{
    [SerializeField]
    private float sinkSpeed = 1.0f; // ’¾‚Þ‘¬“x
    [SerializeField]
    private float sinkTime = 2.0f; // ’¾‚ÞŽžŠÔ

    private int contact = 0;
    private float timeElapsed = 0.0f;
    private bool sink = false;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (sink)
        {
            timeElapsed += Time.deltaTime;
            float ratio = 1.0f - (timeElapsed / sinkTime);
            float move = sinkSpeed * ratio * Time.deltaTime;

            transform.position = new Vector3(
                transform.position.x, 
                transform.position.y - move, 
                transform.position.z
                );

            if (timeElapsed >= sinkTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            contact++;

            if (contact >= 3)
            {
                sink = true;
                rb.isKinematic = true;
            }
        }
    }
}
