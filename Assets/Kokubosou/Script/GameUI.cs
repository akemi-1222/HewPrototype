using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    // Levelのテキスト
    public TMP_Text Text;
    
    // PrayerSizeの参照
    public PlayerSize PlayerSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "Level : " + PlayerSize.g_currentLevel +
                    "\nPoint : " + PlayerSize.g_eatPoint;
    }

}
