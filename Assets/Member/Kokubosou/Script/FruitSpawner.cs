using UnityEngine;
using System.Collections;
public class FruitSpawner : MonoBehaviour
{
    // Inspectorから復活させるフルーツのPrefabを設定する
    [SerializeField] private GameObject fruitPrefab;

    // Inspectorからフルーツの復活時間を設定する
    [SerializeField] private float respawnTime;
    // 現在スポナーが管理しているフルーツ
    private GameObject currentFruit;

    // 復活処理中かどうかを管理する 
    // trueの場合、重複して復活処理を開始しない
    private bool isRespawning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ゲーム開始時にフルーツを生成する
        SpawnFruit();
    }


    // Update is called once per frame
    void Update()
    {
        // 管理しているフルーツが消えていて、 
        // まだ復活処理が始まっていない場合
        if (currentFruit == null && !isRespawning)
        {
            // フルーツの復活処理を開始する
            StartCoroutine(RespawnFruit());
        }
    }

    // フルーツを生成するメソッド
    private void SpawnFruit()
    {
        // InspectorにPrefabが設定されているか確認する
        if (fruitPrefab == null)
        {
            Debug.LogError("Fruit Prefabが設定されていません！");
            return;
        }
        // スポナーの位置と向きにフルーツを生成する 
        // 生成したフルーツをcurrentFruitに保存する
        currentFruit = Instantiate(fruitPrefab, transform.position, transform.rotation);
    }

    // フルーツを復活させるコルーチン
    private IEnumerator RespawnFruit()
    {
        // 復活処理中であることを記録する
        isRespawning = true;

        // Inspectorで設定した秒数だけ待機する
        yield return new WaitForSeconds(respawnTime);

        // フルーツを再生成する
        SpawnFruit();

        // 復活処理が終了したことを記録する
        isRespawning = false;
    }

}
