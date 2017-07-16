using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
// 测试使用，主要用来测试抛物线
public class shoot_with_mouse : MonoBehaviour {

	// Use this for initialization
	Vector2 begin_posion;//开始的位置
	Vector2 end_position; //结束的位置  结束指向开始作为力
	Vector3 now_position;  //判断是否出去大圆
	public GameObject control_in; //摇杆内部（摇杆背景
	public Camera camera;//主摄像机
	public GameObject control_out; //摇杆外部
	public GameObject hero;  //主角
	public GameObject ball; //轨迹用小球


	bool can_move = false; 
	bool can_rot = false;
	bool on_land = true;
	Rigidbody2D ri; //主角的刚体

	float totaltime = 0.0f; //控制旋转
	float angle = 0; //旋转角度

	void Start () {
		ri = hero.GetComponent<Rigidbody2D> ();
		end_position = control_in.transform.position;
		//Debug.Log (control_out.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (Input.mousePosition);
		//if (force ().x*20 != 0)
			//Debug.Log (force ().x * 20);
		ri.AddForce (force ()*0.7f , ForceMode2D.Impulse);
		angle += Mathf.Abs(force ().x);
		//if(angle != 0)
			//Debug.Log (angle);
		totaltime += Time.fixedDeltaTime * 100;
		if (can_rot) {
			//Debug.Log ("can rot");
			//Debug.Log (angle * 20 * totaltime);
			ri.transform.rotation = Quaternion.Euler (0, 0, angle*2.0f * totaltime);
		}
		/*
		if (can_rot && on_land) {
			ri.angularDrag = 0.2f;
			if ((int)(ri.transform.rotation.eulerAngles.z ) % 360 == 0) {
				ri.transform.rotation = Quaternion.Euler (0, 0, 0);
			}		ri.angularDrag = 0.05f;
		}
		*/
	}
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "bg") {
			can_rot = false;
			ri.transform.rotation = Quaternion.Euler (0, 0, 0);
		}
	}
	//求力的大小和方向
	Vector2 force(){
		begin_posion = control_out.transform.position;
		//在固定区域点下
		if (Input.GetMouseButtonDown (0) && Vector2.Distance (Input.mousePosition, control_in.transform.position) < 25.0f) {
			//Debug.Log ("get the mouse");
			can_move = true;
		}
		//推拽 分两种，鼠标在圆内和在圆外
		if (Input.GetMouseButton (0) && can_move) {
			//在圆内
			if (Vector2.Distance (control_in.transform.position, Input.mousePosition) < 25.0f) {
				end_position = Input.mousePosition;
				control_out.transform.position = end_position;
				//显示轨迹 
				create_ball ();
				//在圆外
			} else {
				float sin = (Input.mousePosition.y - control_in.transform.position.y) / Vector2.Distance (control_in.transform.position, Input.mousePosition);
				float cos = (Input.mousePosition.x - control_in.transform.position.x) / Vector2.Distance (control_in.transform.position, Input.mousePosition);
				end_position.x = control_in.transform.position.x + 25.0f * cos;
				end_position.y = control_in.transform.position.y + 25.0f * sin;
				control_out.transform.position = end_position;
				//显示轨迹
				create_ball ();
			}
		}
		//松开鼠标 获取力的大小
		if (Input.GetMouseButtonUp (0) ) {
			can_move = false;
			//Debug.Log ("here");
			can_rot = true;
			on_land = false;
			control_out.transform.position = control_in.transform.position;
			Vector2 temp_force = control_in.transform.position;
			return temp_force - end_position;
		}
		return Vector2.zero;
	}
	//没用到
	Vector2 ScreenToWord(Vector2 screen){
		Vector2 view = camera.ScreenToViewportPoint (screen);
		Vector2 word = camera.ViewportToWorldPoint (view);
		return word;
	}
	//显示轨迹（略暴力，后期可优化）
	void create_ball(){
		Vector2 ball_force;
		ball_force.x = control_out.transform.position.x - control_in.transform.position.x;
		ball_force.y = control_out.transform.position.y - control_in.transform.position.y;
		//ball_ri.AddForce (-ball_force * 20, ForceMode2D.Impulse);
		for(int i = 0 ;i < 20; i++){
			GameObject ball_temp = Instantiate (ball, fx(-ball_force*0.7f,0.05f*i), Quaternion.identity) as GameObject;
			Destroy (ball_temp, 0.05f);
		}
	}
	//计算平抛运动的轨迹 y=vt+1/2at^2
	Vector2 fx(Vector2 speed, float time){
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
}
