using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public class PlayerMove : MonoBehaviour
    {
        public float moveSpeed = 5f;

        // Update is called once per frame
        void Update()
        {
            //キー入力を取得
            float x = Input.GetAxisRaw("Horizontal");
            float y= Input.GetAxisRaw("Vertical");

            //移動方向を作る
            Vector3 move =new Vector3(x,0f,y).normalized;

            //プレイヤーを移動
            transform.position += move*moveSpeed*Time.deltaTime;
        }
    }
    public class ItemPickup :MonoBehaviour
    {
        //アイテムに触れた瞬間に実行する
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                Debug.Log("アイテムを取得した!");
                Destroy(gameObject);
            }
        }
    }
}
