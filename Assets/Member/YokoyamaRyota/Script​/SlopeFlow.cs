using UnityEngine;

public class SlopeFlow : MonoBehaviour
{
    // Direction relative to the ramp, so rotating the machine also rotates its flow.
    [SerializeField] public Vector3 flowDirection = new Vector3(1, 0, 0);
    [SerializeField] public float flowForce = 5.0f;

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody body = collision.rigidbody;
        if (body == null || body.isKinematic ||
            body.GetComponentInParent<PlayerSize>() != null)
            return;

        Vector3 worldDirection = transform.TransformDirection(flowDirection).normalized;
        body.AddForce(worldDirection * flowForce);
    }
}
