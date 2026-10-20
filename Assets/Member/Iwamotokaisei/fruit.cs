using UnityEngine;

public class fruit : MonoBehaviour
{
    [SerializeField] private GameObject player;

    // プレイヤーの体を表すColliderをInspectorで設定する
    [SerializeField] private Collider playerBody;

    private Collider fruitBody;
    private bool consumed;

    private void Awake()
    {
        fruitBody = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        TryEat(other);
    }

    private void OnTriggerStay(Collider other)
    {
        // 触れている最中にプレイヤーが成長した場合も判定する
        TryEat(other);
    }

    private void TryEat(Collider other)
    {
        if (consumed || player == null ||
            playerBody == null || fruitBody == null)
        {
            return;
        }

        if (other.gameObject != player &&
            !other.transform.IsChildOf(player.transform))
        {
            return;
        }

        float playerSize = GetSize(playerBody);
        float fruitSize = GetSize(fruitBody);

        // 同じ大きさ以下なら、まだ食べられない
        if (playerSize <= fruitSize)
            return;

        PlayerSize playerSizeComponent = player.GetComponent<PlayerSize>();
        if (playerSizeComponent == null)
            return;

        consumed = true;
        playerSizeComponent.AddEatPoint();
        Destroy(gameObject);
    }

    private static float GetSize(Collider body)
    {
        Vector3 size = body.bounds.size;

        // 幅・高さ・奥行きのうち、一番長い辺を大きさとして扱う
        return Mathf.Max(size.x, Mathf.Max(size.y, size.z));
    }
}