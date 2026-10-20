using UnityEngine;

public class DeleteBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // ” ‚É“–‚½‚Á‚½‘Šè‚ğíœ
        Destroy(other.gameObject);
    }
}
