using UnityEngine;

public class SlopeFlow : MonoBehaviour
{
    [SerializeField]
    // —¬‚·•ûŒü
    public Vector3 flowDirection = new Vector3(1, 0, 0);
    [SerializeField]
    // —¬‚ê‚é‹­‚³
    public float flowForce = 5.0f;

    // ƒXƒ[ƒv‚ÉG‚ê‚Ä‚¢‚éŠÔŠŠ‚é
    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;

        if (rb != null)
        {
            rb.AddForce(
                flowDirection.normalized * flowForce
                );
        }
    }
}
