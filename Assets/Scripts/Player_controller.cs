using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player_controller : MonoBehaviour {

	public enum player_state{begin, nothing, move, attack};
	player_state state;
	public Game_controller controller;
	bool after_move ;

	public GameObject control_in; 
	public GameObject control_out;
    public new Camera camera;//主摄像机
    public GameObject ball; //轨迹用小球
	Vector2 begin_posion;//开始的位置
	Vector2 end_position; //结束的位置  结束指向开始作为力
	Vector3 now_position;  //判断是否出去大圆
	bool can_move = false; 
	bool can_rot = false;
	bool on_land = true;
    bool can_change  = true;
    private GameObject[] balls = new GameObject[20];

	
	private GameObject[] objs;


	void Start () {
		after_move = false;
        state = player_state.begin;
		//state = player_state.begin; 
        //测试
        // state = player_state.attack;
        for (int i = 0; i < balls.Length; ++i)
        {
            balls[i] = Instantiate(ball) as GameObject;
        }
        StartCoroutine("ChangeState");
	}
	
	// Update is called once per frame
	//void Update () {
	//    ChangeState (state);
	//}

	IEnumerator ChangeState(/*player_state state*/){
        #region Before
        //switch(state){
        //          case (player_state.begin):

        //	/*
        //	if(after_move == false){
        //		if(receive nothing){
        //			state = player_state.nothing;
        //		}
        //		elseif(receve){
        //			state = player_state.move;
        //		}else(  attack){
        //			state = player_state.attack;
        //		}
        //	}
        //	if (after_move == true){
        //		if(receive nothing){
        //			state = player_state.nothing;
        //		}else(  attack){
        //			state = player_state.attack;
        //		}
        //	}
        //             */         
        //	break;

        //          case (player_state.move):
        //              Move(objs[0]);
        //	    after_move = true;
        //	    state = player_state.begin;
        //	    break;
        //          case (player_state.attack):
        //              //Debug.Log("case");
        //              Attack(2, objs[0]);
        //              state = player_state.nothing;
        //             // Debug.Log(state);
        //	    break;
        //          case (player_state.nothing):
        //             // Debug.Log("hh");
        //	    controller.State = Game_controller.Game_State.Game_Enemy;
        //	    state = player_state.begin;
        //	    after_move = false;
        //	    break;
        //}
        #endregion
        // 使用协程的话就需要while和yield

        while (state == player_state.begin)
        {
            yield return null;
            Thread.Sleep(2000);
            state = player_state.attack;
        }

        while (state == player_state.move)
        {
            Move(People.instance.GetOnePerson(0));
            state = player_state.attack;
            yield return null;
        }

        if (state == player_state.attack)
        {
            Attack(3, People.instance.GetOnePerson(0));
            //state = player_state.nothing;
            yield return null;
        }
        // 不会再进入了
        if (state == player_state.nothing)
        {
            yield return null;
        }
    }

	void Move(GameObject hero){	
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
					CreateBall (hero);
					//在圆外
				} else {
					float sin = (Input.mousePosition.y - control_in.transform.position.y) / Vector2.Distance (control_in.transform.position, Input.mousePosition);
					float cos = (Input.mousePosition.x - control_in.transform.position.x) / Vector2.Distance (control_in.transform.position, Input.mousePosition);
					end_position.x = control_in.transform.position.x + 25.0f * cos;
					end_position.y = control_in.transform.position.y + 25.0f * sin;
					control_out.transform.position = end_position;
					//显示轨迹
					CreateBall (hero);
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
	void CreateBall(GameObject hero){

		Vector2 ball_force;
		ball_force.x = control_out.transform.position.x - control_in.transform.position.x;
		ball_force.y = control_out.transform.position.y - control_in.transform.position.y;
		//ball_ri.AddForce (-ball_force * 20, ForceMode2D.Impulse);
		//for(int i = 0 ;i < 20; i++){
		//	GameObject ball_temp = Instantiate (ball, Fx(-ball_force*0.7f,0.05f*i,hero), Quaternion.identity) as GameObject;
		//	Destroy (ball_temp, 0.05f);
		//}
        // 改用设置位置
        for (int i = 0; i < balls.Length; ++i)
        {
            balls[i].transform.position = (Vector2)Fx(-ball_force * 0.7f, 0.05f * i, hero);
        }
        // 0.03s后隐藏，仍有重影现象
        Thread.Sleep(30);
        for (int i = 0; i < balls.Length; ++i)
        {
            balls[i].SetActive(false);
        }
    }
	//计算平抛运动的轨迹 y=vt+1/2at^2
	Vector2 Fx(Vector2 speed, float time ,GameObject hero){
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
    // 攻击选择
    void Attack(int NO, GameObject person){
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
