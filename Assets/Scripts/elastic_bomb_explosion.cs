﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elastic_bomb_explosion : MonoBehaviour {

	public GameObject move_bomb;
    public GameObject move_explosion;
	private GameObject[] objs;

	private HPController[]  healths;
	private float[] distance ;
	private Rigidbody2D ri;
    private bool can_explosion = false;


	// Use this for initialization
	void Start () {
		ri = GetComponent<Rigidbody2D> ();
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

	}

	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "barrier" && can_explosion == true){
            exam_distance();
            GameObject explosion = Instantiate (move_explosion, this.transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f);
            //   Debug.Log("coll");
            move_bomb.SetActive(false);
            Invoke ("dehealt", 1f);
            clear ();
        }
    }
	void  dehealth(){
		for (int i = 0; i < 10; i++) {
			if (healths [i].isdeath == false && distance[i] < 5) 
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
