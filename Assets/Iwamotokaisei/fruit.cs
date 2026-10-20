using UnityEngine;

public class fruits : MonoBehaviour
{
    [SerializeField] private GameObject player;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        //Destroy(collision.gameObject);
        if(other.gameObject==player||
            other.transform.IsChildOf(player.transform))
       {
            Destroy(this.gameObject);
        }
        

        Debug.Log("ぶつかる相手:" + other.gameObject.name);
    }
    //public class PlayerMove : MonoBehaviour
    // {

    //    // Update is called once per frame
    //    void Update()
    //    {
    //        //キー入力を取得
    //        float x = Input.GetAxisRaw("Horizontal");
    //        float y= Input.GetAxisRaw("Vertical");

    //        //移動方向を作る
    //        Vector3 move =new Vector3(x,0f,y).normalized;

    //        //プレイヤーを移動
    //        transform.position += move*moveSpeed*Time.deltaTime;
    //    }
    //}
   // public class ItemPickup :MonoBehaviour
    //{
        //アイテムに触れた瞬間に実行する
        
    //}
}
