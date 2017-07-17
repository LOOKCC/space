using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class big_bomb_explosion : MonoBehaviour {

    public GameObject bigbomb;
	public GameObject big_explosion;

    bool can_explosion = false;

	void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "barrier" && can_explosion){
            //exam_distance();
            // 获取距离
            People.instance.GetDistances(this.transform);
            // 生成爆炸效果
            GameObject explosion = Instantiate (big_explosion, this.transform.position, Quaternion.identity);
            // 0.5s后销毁
            Destroy(explosion, 0.5f);
            //Debug.Log("coll");
            bigbomb.SetActive(false);
            // disappear函数干什么的？
            Invoke("disappear", 1.0f);
            // Invoke ("disappear" ,1.0f);
            //Invoke ("dehealt", 2f);
            // 休眠2s
            Thread.Sleep(2000);
            // 减血
            People.instance.DecreaseHealth(4.0f, 1);
			//clear ();
		}
	}
    /*
    // 看似是恢复状态的
    void disappear(){
        bigbomb.SetActive(false);
        exam_distance();
        explosion.SetActive(true);
        Destroy(explosion,1f);
    } 
*/
    void OnCollisionExit2D(Collision2D coll){
        // 碰到barrier才爆炸
        if(coll.gameObject.tag == "barrier" )
            can_explosion = true;
    }
}
	
