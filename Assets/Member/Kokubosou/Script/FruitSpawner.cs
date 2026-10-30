using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject fruitSpawner; // フルーツスポナー

    [SerializeField]
    public float rispawnTime;     // リスポーン時間

    private Vector3 spawnPosition; // フルーツ生成位置

    private GameObject currentFruit; // 現在生成しているフルーツを記録する

    private bool isReapawning = false; // 復活処理を行っているかを記録
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ゲーム開始時にフルーツを生成する
    }


    // Update is called once per frame
    void Update()
    {
        // フルーツを生成する
        Instantiate(
        fruitSpawner,
        transform.position,
        transform.rotation);

        // 生成したフルーツを変数に保存する
        currentFruit = Instantiate(
            fruitSpawner,
            spawnPosition,
             transform.rotation
        );

        if (currentFruit == null)
        {
            // フルーツが存在しなくなったときの処理

        }
        // フルーツが消えていて、フッ化る処理も始まっていない場合
        if(currentFruit == null && isReapawning)
        {

        }
    }



}
