using UnityEngine;

public class Spown : MonoBehaviour
{
    public GameObject SpawnObject;

    float TimeCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //タイマー機能
        TimeCount += Time.deltaTime;
        //5秒ごとに生成
        if (TimeCount > 5)
        {
            //設定した場所にEnemyを回転0で出す
            Instantiate(SpawnObject, transform.position, Quaternion.identity);
            TimeCount = 0;//カウントを0に戻す
        }
    }
}
