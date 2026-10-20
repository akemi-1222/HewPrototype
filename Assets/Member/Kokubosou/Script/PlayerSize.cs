using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

//int g_currentLevel;  //今のレベル
//int g_pastLevel;     //1f前のレベル
//int g_eatPoint;      //食べたもののポイント

public class PlayerSize : MonoBehaviour
{
    public int g_currentLevel;  //今のレベル
    public int g_pastLevel;     //1f前のレベル
    public int g_eatPoint;      //食べたもののポイント

    void Start()
    {
        g_currentLevel = 1;
        g_pastLevel = g_currentLevel;
        g_eatPoint = 0;
    }

    void Update()
    {
        int i;
        i = g_currentLevel;
        if (Keyboard.current != null &&
            Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            g_eatPoint += 1;
        }

        if ( g_eatPoint == i * 2 - 1/*レベルが1でeatPointが1の時はレベルを2に。レベルが2でeatPointが3の時はレベルを3に。レベルが3でeatPointが5になったらレベルを4に...がずっと続く。奇数とiを上手く使うとすっきりしたコードにできるはず。。。*/)
        {
            //レベルをUPさせる処理
            g_currentLevel++;


            //eatPointをリセットさせる処理
            g_eatPoint = 0;

        }
        if(g_pastLevel != g_currentLevel)
            Grow();
    }
    // Playerのサイズ変更
    public void Grow()
    {
        // プレイヤーのサイズを1.2倍に
        transform.localScale *= 1.2f;

        // 現在のレベルの保存
        g_pastLevel = g_currentLevel;
    }
}
