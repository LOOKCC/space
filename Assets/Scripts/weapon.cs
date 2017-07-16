using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//武器库(因为没有图还没有做测试，应该会有很多bug，有图后会测试和优化）
public class weapon : MonoBehaviour {

	// Use this for initialization
	public GameObject control_in; 
	public GameObject control_out;
	public Camera camera;//主摄像机
	public GameObject ball; //轨迹用小球
	Vector2 begin_posion;//开始的位置
	Vector2 end_position; //结束的位置  结束指向开始作为力
	Vector3 now_position;  //判断是否出去大圆
	bool can_move = false; 
    bool can_attack = true;
	//bool can_rot = false;
	//bool on_land = true;

    public Image mini_map;
    float screen_width;
    float screen_height;
    float map_height;
    float map_width;

	public GameObject Box;

	public GameObject Drum;

	public GameObject Bird;//轰炸机
	public GameObject Egg;//轰炸机上的炸弹
	private float length;
	private float bird_speed;
	private bool can_layeggs = false ;

	public GameObject SmallBomb;
	public GameObject BigBomb;
	public GameObject TimeBomb;
	public GameObject ElasticBomb;
	public GameObject Cannon;
	public GameObject Lightning;
	public GameObject Tsunami;
	public GameObject CannonBomb;

   


	void Start () {
        screen_width = Screen.width;
        screen_height = Screen.height;
        map_width = mini_map.transform.position.x * 2;
        map_height = (screen_height - mini_map.transform.position.y) * 2;
		length = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
		end_position = control_in.transform.position;
        SmallBomb.SetActive(false);
        BigBomb.SetActive(false);
        Cannon.SetActive(false);
        Box.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	void force(GameObject hero){
		Rigidbody2D ri = hero.GetComponent<Rigidbody2D> ();
		Vector2 temp_force = Vector2.zero;
		begin_posion = control_out.transform.position;
       // Debug.Log("force");
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
        if (Input.GetMouseButtonUp (0)&& can_move == true ) {
			can_move = false;
            can_attack = true;
			//Debug.Log ("here");
			//can_rot = true;
			//on_land = false;
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



	void box(){
        if (Input.GetMouseButtonDown (0) && !(Input.mousePosition.x >= 0 && Input.mousePosition.x <= map_width && Input.mousePosition.y <= screen_height && Input.mousePosition.y >= screen_height - map_height)) {
            Debug.Log("box");
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            mousePosition.y += 1;
            mousePosition.z = 1;
            Box.transform.rotation = Quaternion.identity; 
            Box.SetActive(true);
			Box.transform.position = mousePosition;
		}
	}
	void drum(){
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			GameObject drum_obj = Instantiate (Drum) as GameObject;
			drum_obj.transform.position = mousePosition;
		}
	}
	void bird_egg(){
		GameObject bird_obj = Instantiate(Bird) as GameObject;
		bird_obj.SetActive (false);
		if (Input.GetMouseButtonDown(0) && can_layeggs == false)
		{ 
			Vector2 mouseTransform = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			bird_obj.transform.position = new Vector2(-length, mouseTransform.y);
			bird_obj.GetComponent<Rigidbody2D>().velocity = new Vector2(bird_speed, 0);
			bird_obj.SetActive (true);
			can_layeggs = true;
		}
		if (Input.GetMouseButtonDown (0) && can_layeggs == true) {
			GameObject egg_obj = Instantiate (Egg) as GameObject;
			egg_obj.transform.position = bird_obj.transform.position;
			egg_obj.GetComponent<Rigidbody2D> ().velocity = bird_obj.GetComponent<Rigidbody2D> ().velocity;
		}
		if (can_layeggs == true && bird_obj.transform.position.x > length + 2)
			Destroy (bird_obj);
	}
	float clamp(float x,float min,float max){
		if(x > max)
			return max;
		else if(x < min)
			return min;
		else
			return x;
	}
    void cannon(GameObject person){
        if(can_attack == true){
            Cannon.SetActive(true);
            Cannon.transform.position = person.transform.position;
        }
        can_attack = false;
        //GameObject cannon_obj = Instantiate(Cannon, person.transform.position, Quaternion.identity);
        //Rigidbody2D ri = cannon_obj.GetComponent<Rigidbody2D> ();
		if (Input.GetMouseButtonDown (0) && Vector2.Distance (Input.mousePosition, control_in.transform.position) < 25.0f) {
			can_move = true;
		}
		if (Input.GetMouseButton (0) && can_move) {
			end_position = new Vector2 (50.0f, 0.0f);
			end_position.y = clamp (Input.mousePosition.y, 25.0f, 75.0f);
			control_out.transform.position = end_position;
            Cannon.transform.position = new Vector3 (person
                .transform.position.x, person.transform.position.y + (control_out.transform.position.y - 50.0f) / 10, 0);
		}
		//松开鼠标 获取力的大小
        if (Input.GetMouseButtonUp (0)&& can_move ) {
			can_move = false;
            //float temp = control_out.transform.position.y - 50.0f;
			control_out.transform.position = control_in.transform.position;
			GameObject bomb = Instantiate (CannonBomb, Cannon.transform.position, Quaternion.identity);
			Rigidbody2D ri_bomb = bomb.GetComponent<Rigidbody2D> ();
			ri_bomb.AddForce (new Vector2 (50.0f, 0), ForceMode2D.Impulse);
		}
	}
    void small_bomb(GameObject person){
        if(can_attack == true){
            SmallBomb.SetActive(true);
            SmallBomb.transform.position = person.transform.position;
        }
        can_attack = false;
		force (SmallBomb);
       // count = true;
		//碰撞 在对应物体中写 撞到player或者 bg 就销毁自己 生成爆炸的物体， 并播放爆炸动画。
       // Debug.Log(can_attack);
	}
    void big_bomb(GameObject person){
        if(can_attack == true){
            BigBomb.SetActive(true);
            BigBomb.transform.position = person.transform.position;
        }
        can_attack = false;
        force (BigBomb);
        //force (bigbomb_obj);
		//同上 爆炸威力更大，弹性略大
	}
    void elastic_bomb(GameObject person){
        GameObject elasticbomb_obj = Instantiate(ElasticBomb, person.transform.position, Quaternion.identity);
        force (elasticbomb_obj);
		//同上 弹性更大
	}
    void time_bomb(GameObject person){
        GameObject timebomb_obj = Instantiate(TimeBomb, person.transform.position, Quaternion.identity);
		force (timebomb_obj);

	}
	void lightning(){
		Vector3 lig_pos = new Vector3(0,Screen.height,1);
		if (Input.GetMouseButtonDown (0)) {
			lig_pos.x = Input.mousePosition.x;
            Debug.Log(ScreenToWord(lig_pos));
			GameObject Lightning_obj = Instantiate (Lightning,ScreenToWord(lig_pos) , Quaternion.identity) as GameObject;
			Destroy (Lightning_obj, 2.0f);
		}
 	}
	void tsunami(){
		Vector3 speed = new Vector3(1,0, 0);
        if (Input.GetMouseButtonDown(0))
        {
            GameObject Tsunami_obj = Instantiate(Tsunami, new Vector3(-17, -5, 0), Quaternion.identity);
            Rigidbody2D Tsunami_ri = Tsunami_obj.GetComponent<Rigidbody2D>();
            Tsunami_ri.velocity = new Vector2(15, 0);
            if (Tsunami_obj.transform.position.x > 17)
                Destroy(Tsunami_obj);
        }
	}

}
