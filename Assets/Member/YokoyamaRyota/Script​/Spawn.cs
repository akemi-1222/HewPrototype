using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] public GameObject SpawnObject;
    [SerializeField, Min(0.1f)] private float spawnInterval = 5f;
    [SerializeField, Min(0f)] private float spawnedLifetime = 30f;
    [SerializeField] private Transform passThroughRoot;

    private float timeCount;

    private void Start()
    {
        if (passThroughRoot == null)
            return;

        // Keep the ramps solid for food, but let player bodies pass through.
        Collider[] machineColliders = passThroughRoot.GetComponentsInChildren<Collider>(true);
        foreach (PlayerSize player in FindObjectsByType<PlayerSize>(FindObjectsSortMode.None))
        {
            foreach (Collider body in player.GetComponentsInChildren<Collider>(true))
            {
                foreach (Collider machine in machineColliders)
                    Physics.IgnoreCollision(body, machine);
            }
        }
    }

    private void Update()
    {
        if (SpawnObject == null)
            return;

        timeCount += Time.deltaTime;
        if (timeCount < Mathf.Max(0.1f, spawnInterval))
            return;

        timeCount = 0f;
        GameObject spawned = Instantiate(SpawnObject, transform.position, Quaternion.identity);
        // Remove food that misses the collection box instead of accumulating forever.
        if (spawnedLifetime > 0f)
            Destroy(spawned, spawnedLifetime);
    }
}
