using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    [SerializeField] private Transform ground;

    // プレイヤーの拡大率1あたり、Groundを何メートル下げるか
    [SerializeField] private float groundDropPerScale = 1f;

    public int g_currentLevel;
    public int g_pastLevel;
    public int g_eatPoint;

    void Awake()
    {
        g_currentLevel = 1;
        g_pastLevel = g_currentLevel;
        g_eatPoint = 0;
    }

    public void AddEatPoint()
    {
        g_eatPoint++;

        // Each level requires 1, 3, 5, ... points respectively.
        if (g_eatPoint >= g_currentLevel * 2 - 1)
        {
            g_currentLevel++;
            g_eatPoint = 0;
            Grow();
        }
    }

    // Scale the player when its level changes.
    public void Grow()
    {
        float previousScale = transform.localScale.y;

        transform.localScale *= 1.2f;

        float scaleIncrease = transform.localScale.y - previousScale;

        if (ground != null)
        {
            ground.position += Vector3.down
                * scaleIncrease
                * groundDropPerScale;
        }

        g_pastLevel = g_currentLevel;
    }
}
