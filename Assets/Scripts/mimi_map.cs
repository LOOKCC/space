using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//实现小地图，鼠标点击即显示点击部分的内容
public class mimi_map : MonoBehaviour {
    // 主相机
	public Camera maincamera; 
	public Image frame;
	public Image mini_map;
	private  Game_controller controler;
	Vector3 frame_pos;
	Vector3 camera_pos;
	float screen_width;
	float screen_height;
	float map_height;
	float map_width;

	float frame_weight;
	float frame_height;
	// Use this for initialization
	void Start () {
		controler = GameObject.Find ("Game_control").GetComponent<Game_controller> ();
//		Debug.Log (mini_map.transform.position);
		screen_width = Screen.width;
		screen_height = Screen.height;
		map_width = mini_map.transform.position.x * 2;
		map_height = (screen_height - mini_map.transform.position.y) * 2;
		frame_height = map_height * 8.0f / 14.0f;
		frame_weight = map_width / 2.0f;
	}
	// Update is called once per frame
	void Update () {
		if (controler.state == Game_controller.Game_State.Game_Player) {
			change ();
		}
	}
	//超过x边界
	float clampx(float x){
		if (x < mini_map.transform.position.x - map_width / 4.0f)
			return mini_map.transform.position.x - map_width / 4.0f;
		else if (x > mini_map.transform.position.x + map_width / 4.0f)
			return mini_map.transform.position.x + map_width / 4.0f;
		else
			return x;
	}
	//超过y边界
	float clampy(float y){
		if (y < mini_map.transform.position.y - map_height / 2.0f + frame_height / 2.0f)
			return mini_map.transform.position.y - map_height / 2.0f + frame_height / 2.0f;
		else if (y > mini_map.transform.position.y + map_height  / 2.0f - frame_height / 2.0f)
			return mini_map.transform.position.y + map_height  / 2.0f - frame_height / 2.0f;
		else
			return y;
	}
	//对应比例转化
	Vector3 transpos(Vector3 framepos) {
		Vector3 ret;
		ret.x = 28.0f * framepos.x / map_width - 14.0f;
		ret.y = 7.0f - (screen_height - framepos.y) * 14.0f / map_height;
		ret.z = -10.0f;
		return ret;
	}
	//总流程控制
	void change(){
		if (Input.GetMouseButtonDown (0) && Input.mousePosition.x >= 0 && Input.mousePosition.x <= map_width && Input.mousePosition.y <= screen_height && Input.mousePosition.y >= screen_height - map_height) {
			frame_pos.x = clampx (Input.mousePosition.x);
			frame_pos.y = clampy (Input.mousePosition.y);
			frame_pos.z = 0.0f;
			camera_pos = transpos (frame_pos);
			frame.transform.position = frame_pos;
			maincamera.transform.position = camera_pos;
		}
	}
}