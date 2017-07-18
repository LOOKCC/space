using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CannonAttack : MonoBehaviour {
	public GameObject cannon_bomb_explosion;
    public GameObject cannon_bomb;
    bool can_explosion = false;

    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "barrier" && can_explosion == true){
            // 获取距离，保存在了people那里
            People.instance.GetDistances(this.transform);
            GameObject explosion = Instantiate (cannon_bomb_explosion, this.transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f);
            //   Debug.Log("coll");
            cannon_bomb.SetActive(false);
            //Invoke ("dehealt", 1f);
            // 休眠1s再调用减血
            Thread.Sleep(1000);
            People.instance.DecreaseHealth(2f, 1f);
            //clear ();
        }
    }
    void OnCollisionExit2D(Collision2D coll){
        if(coll.gameObject.tag == "barrier" )
            can_explosion = true;
    }
}
