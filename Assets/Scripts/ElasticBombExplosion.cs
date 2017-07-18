using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ElasticBombExplosion : MonoBehaviour {

	public GameObject move_bomb;
    public GameObject move_explosion;
	private Rigidbody2D ri;
    private bool can_explosion = false;

	// Use this for initialization
	void Start () {
		ri = GetComponent<Rigidbody2D> ();
	}

    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "barrier" && can_explosion == true){
            //exam_distance();
            People.instance.GetDistances(this.transform);
            GameObject explosion = Instantiate (move_explosion, this.transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f);
            //   Debug.Log("coll");
            move_bomb.SetActive(false);
            //Invoke ("dehealt", 1f);
            Thread.Sleep(1000);
            People.instance.DecreaseHealth(5f, 1);
            //clear ();
        }
    }
}
