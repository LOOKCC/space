using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
		controller = game_control.GetComponent<Game_controller> ();
		prestate = controller.State;
		//clear ();
		can_bomb = true;
	}

	// Update is called once per frame
	void Update () {
		if ((prestate == Game_controller.Game_State.Game_Enemy && controller.State == Game_controller.Game_State.Game_Player)||(prestate == Game_controller.Game_State.Game_Player && controller.State == Game_controller.Game_State.Game_Enemy)) {
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
	//void  dehealth(){
	//	for (int i = 0; i < 10; i++) {
	//		if (healths [i].isDeath == false && distance[i] < 4) 
	//			healths [i].health -= 1.0f / (distance[i] * distance[i] + 0.1f);
	//	}
	//}
	//void exam_distance(){
	//	for(int i = 0; i < 10; i++) 
	//		distance[i] = (this.transform.position - objs [i].transform.position).magnitude;
	//}
	//void clear()
	//{
	//	for (int i = 0; i < 10; i++)
	//		distance [i] = 0.0f;
	//}

}
