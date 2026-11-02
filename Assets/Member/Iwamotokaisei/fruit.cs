using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;

public class fruit : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // Optional player body reference for scene food.
    [SerializeField] private Collider playerBody;

    [Header("êHÇ◊ÇÈâπ")]
    [SerializeField] private AudioClip eatSound;
    private AudioSource audioSource; // Å©í«â¡


    private Collider fruitBody;
    private bool consumed;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        foreach (Collider body in GetComponents<Collider>())
        {
            if (!body.enabled)
                continue;

            fruitBody = body;
            if (!body.isTrigger)
                break;
        }
       

        foreach (Collider body in GetComponents<Collider>())
        {
            if (!body.enabled)
                continue;

            fruitBody = body;

            if (!body.isTrigger)
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TryEat(other);
    }

    private void OnTriggerStay(Collider other)
    {
        // Recheck when the player grows while touching the food.
        TryEat(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        TryEat(collision.collider);
    }

    private void OnCollisionStay(Collision collision)
    {
        TryEat(collision.collider);
    }

    private void TryEat(Collider other)
    {
        if (consumed || fruitBody == null)
            return;

        // Spawned prefabs cannot hold references to scene objects.
        PlayerSize playerSizeComponent = other.GetComponentInParent<PlayerSize>();
        if (playerSizeComponent == null)
            return;

        if (player != null && playerSizeComponent.gameObject != player)
            return;

        Collider body = playerBody;
        if (body == null)
            body = playerSizeComponent.GetComponent<Collider>();
        if (body == null)
            body = other;

        if (GetSize(body) <= GetSize(fruitBody))
            return;

        consumed = true;
        // â ï®ÇêHÇ◊ÇÈâπ
        if (eatSound != null)
        {
               
                audioSource.PlayOneShot(eatSound);
           
        }
        else
        {
            UnityEngine.Debug.LogError("Eat SoundÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒÅI");
        }

        playerSizeComponent.AddEatPoint();

        // â ï®Çè¡Ç∑
        Destroy(gameObject,1f);
    }

    private static float GetSize(Collider body)
    {
        Vector3 size = body.bounds.size;

        // Compare the longest side of the collider bounds.
        return Mathf.Max(size.x, Mathf.Max(size.y, size.z));
    }
}
