using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class big_bomb_explosion : MonoBehaviour {

    public GameObject bigbomb;
	public GameObject big_explosion;

    //private GameObject explosion;

	private HPController[]  healths;
	private float[] distance ;
    bool can_explosion = false;

	// Use this for initialization
	void Start () {
		//clear ();
        healths = People.instance.GetHPs();
        //explosion = Instantiate (big_explosion, this.transform.position, Quaternion.identity);
        //explosion.SetActive(false);
	}

	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "barrier" && can_explosion){
            //exam_distance();
            // 获取距离
            People.instance.GetDistances(this.transform);
            GameObject explosion = Instantiate (big_explosion, this.transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f);
            //Debug.Log("coll");
            bigbomb.SetActive(false);
            Invoke("disappear", 1.0f);
            // Invoke ("disappear" ,1.0f);
            //Invoke ("dehealt", 2f);
            // 休眠2s
            Thread.Sleep(2000);
            // 减血
            People.instance.DecraseHealth(4.0f, 1);
			//clear ();
		}
	}
    /*
    void disappear(){
        bigbomb.SetActive(false);
        exam_distance();
        explosion.SetActive(true);
        Destroy(explosion,1f);
    } 
*/
    void OnCollisionExit2D(Collision2D coll){
        if(coll.gameObject.tag == "barrier" )
            can_explosion = true;
    }
}
	
