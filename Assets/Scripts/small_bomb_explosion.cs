using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class small_bomb_explosion : MonoBehaviour {
    public GameObject smallbomb;
	public GameObject small_explosion;
    bool can_explosion = false;
    bool after_shoot = false;
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "barrier" && can_explosion == true){
            //exam_distance();
            People.instance.GetDistances(this.transform);
			GameObject explosion = Instantiate (small_explosion, this.transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f);
         //   Debug.Log("coll");
            smallbomb.SetActive(false);
            //Invoke ("dehealt", 1f);
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
