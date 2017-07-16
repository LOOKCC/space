using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour {

	public enum player_state{begin, nothing, move, attack};
	player_state state;
	public Game_controller controller;
	bool after_move ;

	public GameObject control_in; 
	public GameObject control_out;
	public Camera camera;//主摄像机
	public GameObject ball; //轨迹用小球
	Vector2 begin_posion;//开始的位置
	Vector2 end_position; //结束的位置  结束指向开始作为力
	Vector3 now_position;  //判断是否出去大圆
	bool can_move = false; 
	bool can_rot = false;
	bool on_land = true;
    bool can_change  = true;

	
	private GameObject[] objs;


	void Start () {
		after_move = false;
		//state = player_state.begin; 
        //测试
        state = player_state.attack;


        objs = new GameObject[10];

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
	}
	
	// Update is called once per frame
	void Update () {
        

		if (controller.State == Game_controller.Game_State.Game_Player) {
			changestate (state);
		}
	}
	void changestate(player_state state){
		switch(state){
            case (player_state.begin):
            
			/*
			if(after_move == false){
				if(receive nothing){
					state = player_state.nothing;
				}
				elseif(receve){
					state = player_state.move;
				}else(  attack){
					state = player_state.attack;
				}
			}
			if (after_move == true){
				if(receive nothing){
					state = player_state.nothing;
				}else(  attack){
					state = player_state.attack;
				}
			}
               */         
			break;

            case (player_state.move):
                move(objs[0]);
			    after_move = true;
			    state = player_state.begin;
			    break;
            case (player_state.attack):
                //Debug.Log("case");
                attack(2, objs[0]);
                state = player_state.nothing;
               // Debug.Log(state);
			    break;
            case (player_state.nothing):
               // Debug.Log("hh");
			    controller.State = Game_controller.Game_State.Game_Enemy;
			    state = player_state.begin;
			    after_move = false;
			    break;
		}
	}

	void move(GameObject hero){	
			Rigidbody2D ri = hero.GetComponent<Rigidbody2D> ();
			Vector2 temp_force = Vector2.zero;
			begin_posion = control_out.transform.position;
			//在固定区域点下
			if (Input.GetMouseButtonDown (0) && Vector2.Distance (Input.mousePosition, control_in.transform.position) < 25.0f) {
				Debug.Log ("get the mouse");
				can_move = true;
			}
			//推拽 分两种，鼠标在圆内和在圆外
			if (Input.GetMouseButton (0) && can_move) {
				//在圆内
				if (Vector2.Distance (control_in.transform.position, Input.mousePosition) < 25.0f) {
					end_position = Input.mousePosition;
					control_out.transform.position = end_position;
					//显示轨迹 
					create_ball (hero);
					//在圆外
				} else {
					float sin = (Input.mousePosition.y - control_in.transform.position.y) / Vector2.Distance (control_in.transform.position, Input.mousePosition);
					float cos = (Input.mousePosition.x - control_in.transform.position.x) / Vector2.Distance (control_in.transform.position, Input.mousePosition);
					end_position.x = control_in.transform.position.x + 25.0f * cos;
					end_position.y = control_in.transform.position.y + 25.0f * sin;
					control_out.transform.position = end_position;
					//显示轨迹
					create_ball (hero);
				}
			}
			//松开鼠标 获取力的大小
            if (Input.GetMouseButtonUp (0)&&can_move ==true ) {
				can_move = false;
				//Debug.Log ("here");
				can_rot = true;
				on_land = false;
				control_out.transform.position = control_in.transform.position;
				temp_force = control_in.transform.position;
				temp_force = temp_force - end_position;
			}
			ri.AddForce (temp_force, ForceMode2D.Impulse);
	}
		//显示轨迹（略暴力，后期可优化）
		void create_ball(GameObject hero){
			Vector2 ball_force;
			ball_force.x = control_out.transform.position.x - control_in.transform.position.x;
			ball_force.y = control_out.transform.position.y - control_in.transform.position.y;
			//ball_ri.AddForce (-ball_force * 20, ForceMode2D.Impulse);
			for(int i = 0 ;i < 20; i++){
				GameObject ball_temp = Instantiate (ball, fx(-ball_force*0.7f,0.05f*i,hero), Quaternion.identity) as GameObject;
				Destroy (ball_temp, 0.05f);
			}
		}
		//计算平抛运动的轨迹 y=vt+1/2at^2
		Vector2 fx(Vector2 speed, float time ,GameObject hero){
			Vector2 ret = new Vector2 (0, 0);
			if (speed.x > 0)
				ret.x = hero.transform.position.x + speed.x * time - 0.5f * 0.3f * time * time;
			else if (speed.x < 0)
				ret.x = hero.transform.position.x + speed.x * time + 0.5f * 0.3f * time * time;
			else
				ret.x = hero.transform.position.x;
			ret.y = hero.transform.position.y + speed.y * time - 0.5f * 14f * time * time;
			return ret;
		}
		Vector3 ScreenToWord(Vector3 screen){
			Vector3 view = camera.ScreenToViewportPoint (screen);
			Vector3 word = camera.ViewportToWorldPoint (view);
			return word;
		}

    void attack(int NO, GameObject person){
        switch (NO)
        {
            case (0):
//                Debug.Log("here");
                SendMessage("small_bomb", person);
                break;
            case (1):
                SendMessage("big_bomb", person);
                break;
            case (2):
                SendMessage("move_bomb", person);
                break;
            case (3):
                SendMessage("time_bomb", person);
                break;
            case(4):
                SendMessage("lightning");
                break;
            case(5):
                SendMessage("tsunami");
                break;
            case(6):
                SendMessage("box");
                break;
            case(7):
                SendMessage("drum");
                break;
            case(8):
                SendMessage("bird_egg");
                break;
            case(9):
                SendMessage("cannon", person);
                break;
        }
    }


}
