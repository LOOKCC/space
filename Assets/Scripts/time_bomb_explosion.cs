using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class time_bomb_explosion : MonoBehaviour {

	public GameObject time_explosion;
	
	//private GameObject[] objs;
	//private HPController[]  healths;
	//private float[] distance ;

	public GameObject game_control;
	private Game_controller controller;
	private Game_controller.Game_State prestate;
	private bool can_bomb ;

	// Use this for initialization
	void Start () {
		controller = game_control.GetComponent<Game_controller> ();
		prestate = controller.state;
		//clear ();
		can_bomb = true;
	}

	// Update is called once per frame
	void Update () {
		if ((prestate == Game_controller.Game_State.Game_Enemy &&
            controller.state == Game_controller.Game_State.Game_Player)||
            (prestate == Game_controller.Game_State.Game_Player && 
            controller.state == Game_controller.Game_State.Game_Enemy)) {
            if (Input.GetKeyDown(KeyCode.A) && can_bomb == true) {
                People.instance.GetDistances(this.transform);
				GameObject explosion = Instantiate (time_explosion, this.transform.position, Quaternion.identity);
				Destroy (this);
                //Invoke ("dehealth", 1f);
                Thread.Sleep(1000);
                People.instance.DecreaseHealth(4, 1);
				//clear ();
			}
		}
	}
	void bomb(){
		//exam_distance ();
		GameObject explosion = Instantiate (time_explosion, this.transform.position, Quaternion.identity);
		Destroy (this);
		Invoke ("dehealth", 1f);
		//clear ();
		can_bomb = false;
	}		
}
