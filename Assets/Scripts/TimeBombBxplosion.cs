using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TimeBombExplosion : MonoBehaviour {

	public GameObject time_explosion;
    //private GameController.GameState prestate;
    private bool can_bomb;

	// Use this for initialization
    // 延时炸弹需要改进，判断太烧脑
	void Start () {
		//prestate = GameController.instance.GetCurrentState();
		can_bomb = true;
	}

	// Update is called once per frame
	void Update () {
		//if ((prestate == GameController.GameState.Game_Enemy &&
  //          GameController.state == GameController.GameState.Game_Player)||
  //          (prestate == GameController.GameState.Game_Player && 
  //          GameController.state == GameController.GameState.Game_Enemy)) {
  //          if (Input.GetKeyDown(KeyCode.A) && can_bomb == true) {
  //              People.instance.GetDistances(this.transform);
		//		GameObject explosion = Instantiate (time_explosion, this.transform.position, Quaternion.identity);
  //              can_bomb = false;
		//		Destroy (this);
  //              Thread.Sleep(1000);
  //              People.instance.DecreaseHealth(4, 1);
		//	}
		//}
	}
	void Bomb(){
		GameObject explosion = Instantiate (time_explosion, this.transform.position, Quaternion.identity);
		Destroy (this);
		Invoke ("dehealth", 1f);
	}		
}
