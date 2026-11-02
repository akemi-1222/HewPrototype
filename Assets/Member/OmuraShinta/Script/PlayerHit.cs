using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    // 食べ物ごとのレベル別設定
    [System.Serializable]
    public class FoodLevelSetting
    {
        // 食べ物のサイズ
        public FoodHealth.FoodSize foodSize;

        // レベルごとの必要ヒット数
        [SerializeField]
        public int[] requiredHit = new int[10];
    }

    //Inspectorでの設定表
    private FoodLevelSetting[] foodLevelSettings =
    {
        new FoodLevelSetting{ foodSize = FoodHealth.FoodSize.XXS},
        new FoodLevelSetting{ foodSize = FoodHealth.FoodSize.XS},
        new FoodLevelSetting{ foodSize = FoodHealth.FoodSize.S},
        new FoodLevelSetting{ foodSize = FoodHealth.FoodSize.M},
        new FoodLevelSetting{ foodSize = FoodHealth.FoodSize.L},
        new FoodLevelSetting{ foodSize = FoodHealth.FoodSize.XL},
        new FoodLevelSetting{ foodSize = FoodHealth.FoodSize.XXL},
    };

    //プレイヤーのレベル、※現在は手動で設定してます。
    [SerializeField]
    private int playerLevel = 1;

    private void OnCollisionEnter(Collision collision)
    {
        FoodHealth health =
            collision.gameObject.GetComponent<FoodHealth>();

        // FoodHealthを持つオブジェクトならダメージ
        if (health != null)
        {
            health.TakeDamage(playerLevel);
        }
    }

    private int GetRequiredHit(FoodHealth.FoodSize size)
    {
        foreach(FoodLevelSetting setting in foodLevelSettings)
        { 
            if(setting.foodSize == size)
            {
                int levelIndex = Mathf.Clamp(playerLevel, 1, 10) - 1;

                return Mathf.Max(1, setting.requiredHit[levelIndex]);
            }
        }

        return 1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
