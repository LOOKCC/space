using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tine_bomb_explosion : MonoBehaviour {

	public GameObject time_explosion;
	
	private GameObject[] objs;

	private HPController[]  healths;
	private float[] distance ;

	public GameObject game_control;
	private Game_controller controller;
	private Game_controller.Game_State prestate;
	private bool can_bomb ;

	// Use this for initialization
	void Start () {
		objs = new GameObject[10];
		healths = new HPController[10];
		distance = new float[10];
		controller = game_control.GetComponent<Game_controller> ();
		prestate = controller.State;
		clear ();
		can_bomb = true;

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
		if ((prestate == Game_controller.Game_State.Game_Enemy && controller.State == Game_controller.Game_State.Game_Player)||(prestate == Game_controller.Game_State.Game_Player && controller.State == Game_controller.Game_State.Game_Enemy)) {
            if (Input.GetKeyDown(KeyCode.A) && can_bomb == true) {
				exam_distance ();
				GameObject explosion = Instantiate (time_explosion, this.transform.position, Quaternion.identity);
				Destroy (this);
				Invoke ("dehealth", 1f);
				clear ();
			}
		}
	}
	void bomb(){
		exam_distance ();
		GameObject explosion = Instantiate (time_explosion, this.transform.position, Quaternion.identity);
		Destroy (this);
		Invoke ("dehealth", 1f);
		clear ();
		can_bomb = false;
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
