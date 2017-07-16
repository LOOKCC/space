using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class big_bomb_explosion : MonoBehaviour {

    public GameObject bigbomb;
	public GameObject big_explosion;
	private GameObject[] objs;

    //private GameObject explosion;

	private HPController[]  healths;
	private float[] distance ;
    bool can_explosion = false;

	// Use this for initialization
	void Start () {
		objs = new GameObject[10];
		healths = new HPController[10];
		distance = new float[10];
		clear ();

        objs[0] = GameObject.Find("player1");
        objs[1] = GameObject.Find("player2");
        objs[2] = GameObject.Find("player3");
        objs[3] = GameObject.Find("player4");
        objs[4] = GameObject.Find("player5");
        objs[5] = GameObject.Find("enemy1");
        objs[6] = GameObject.Find("enemy2");
        objs[7] = GameObject.Find("enemy3");
        objs[8] = GameObject.Find("enemy4");
        objs[9] = GameObject.Find("enemy5");

        for(int i = 0; i < 10; i++)
            healths[i] = objs[i].GetComponent<HPController>();
        //explosion = Instantiate (big_explosion, this.transform.position, Quaternion.identity);
        //explosion.SetActive(false);
	}

	// Update is called once per frame
	void Update () {

	}
	void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "barrier" && can_explosion){

            exam_distance();
            GameObject explosion = Instantiate (big_explosion, this.transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f);
            //Debug.Log("coll");
            bigbomb.SetActive(false);
            Invoke("disappear", 1.0f);
           // Invoke ("disappear" ,1.0f);
			Invoke ("dehealt", 2f);
			clear ();
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

	void  dehealth(){
		for (int i = 0; i < 10; i++) {
			if (healths [i].isdeath == false && distance[i] < 4) 
				healths [i].health -= 1.0f / (distance[i] * distance[i] + 0.1f);
		}
	}
	void exam_distance(){
		for(int i = 0; i < 10; i++) 
			distance[i] = (this.transform.position - objs [i].transform.position).magnitude;
	}
	void clear()
	{
		for (int i = 0; i < 10; i++)
			distance [i] = 0.0f;
	}

}
	
