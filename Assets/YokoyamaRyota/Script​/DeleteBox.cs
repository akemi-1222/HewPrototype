using UnityEngine;

public class DeleteBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        // ” ‚É“–‚½‚Á‚½‘Šè‚ğíœ
        Destroy(other.gameObject);
    }
}
