using UnityEngine;

public class DeleteBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerSize>() != null)
            return;

        fruit food = other.GetComponentInParent<fruit>();
        if (food != null)
            Destroy(food.gameObject);
    }
}
