using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Control : MonoBehaviour {
    
    private bool can_move = false;
    private bool in_sky = false;
    public bool after_move = false;
    public bool for_move = false;
    public bool for_atttack = false;
    public bool can_attack = true;
    public bool can_layeggs = false;


    private  Player_controller player;
    private  AIconrtoller enemy;

    public GameObject control_in; 
    public GameObject control_out;
    private float radious;
    public Camera camera;//主摄像机
    public GameObject ball; //轨迹用小球
    private  Vector2 begin_posion;//开始的位置
    private Vector2 end_position; //结束的位置  结束指向开始作为力
    private Vector3 now_position;  //判断是否出去大圆
    private GameObject[] balls = new GameObject[20];
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Player_controller>();
        radious = control_in.transform.position.x;
        for(int i = 0; i < balls.Length; i++){ 
            balls[i] = Instantiate(ball) as GameObject;
	    }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void addforce_all(GameObject hero){ 
        Rigidbody2D ri = hero.GetComponent<Rigidbody2D> ();
        Vector2 temp_force = Vector2.zero;
        begin_posion = control_out.transform.position;
        //在固定区域点下
        if (Input.GetMouseButtonDown (0) && Vector2.Distance (Input.mousePosition, control_in.transform.position) < radious/2) {
            Debug.Log ("get the mouse");
            can_move = true;
        }
        //推拽 分两种，鼠标在圆内和在圆外
        if (Input.GetMouseButton (0) && can_move) {
            //在圆内
            if (Vector2.Distance (control_in.transform.position, Input.mousePosition) < radious/2) {
                end_position = Input.mousePosition;
                control_out.transform.position = end_position;
                //显示轨迹 
                create_ball (hero);
                //在圆外
            } else {
                float sin = (Input.mousePosition.y - control_in.transform.position.y) / Vector2.Distance (control_in.transform.position, Input.mousePosition);
                float cos = (Input.mousePosition.x - control_in.transform.position.x) / Vector2.Distance (control_in.transform.position, Input.mousePosition);
                end_position.x = control_in.transform.position.x + (radious/2) * cos;
                end_position.y = control_in.transform.position.y + (radious/2 )* sin;
                control_out.transform.position = end_position;
                //显示轨迹
                create_ball (hero);
            }
        }
        //松开鼠标 获取力的大小
        if (Input.GetMouseButtonUp (0)&&can_move ==true ) {
            can_move = false;
            if (for_move){
                after_move = true;
                player.state = Player_controller.player_state.begin;
            }
            if (for_atttack){
                player.state = Player_controller.player_state.nothing;
            }
            control_out.transform.position = control_in.transform.position;
            temp_force = control_in.transform.position;
            temp_force = temp_force - end_position;
            for_move = false;
            for_atttack = false;
        }
        ri.AddForce (temp_force, ForceMode2D.Impulse);
    }
    public void cannon(GameObject person,GameObject Cannon , GameObject CannonBomb){
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
            if (for_atttack){
                player.state = Player_controller.player_state.nothing;
            }
            control_out.transform.position = control_in.transform.position;
            GameObject bomb = Instantiate (CannonBomb, Cannon.transform.position, Quaternion.identity);
            Rigidbody2D ri_bomb = bomb.GetComponent<Rigidbody2D> ();
            ri_bomb.AddForce (new Vector2 (50.0f, 0), ForceMode2D.Impulse);
            for_atttack = false;
        }
    }
    public void movevbomb(GameObject person, GameObject MoveBomb){
        if (in_sky == false)
        {
            Rigidbody2D ri = MoveBomb.GetComponent<Rigidbody2D>();
            Vector2 temp_force = Vector2.zero;
            begin_posion = control_out.transform.position;
            // Debug.Log("force");
            //在固定区域点下
            if (Input.GetMouseButtonDown(0) && Vector2.Distance(Input.mousePosition, control_in.transform.position) < 25.0f)
            {
                Debug.Log("get the mouse");
                can_move = true;
            }
            //推拽 分两种，鼠标在圆内和在圆外
            if (Input.GetMouseButton(0) && can_move)
            {
                //在圆内
                if (Vector2.Distance(control_in.transform.position, Input.mousePosition) < 25.0f)
                {
                    end_position = Input.mousePosition;
                    control_out.transform.position = end_position;
                    create_ball(MoveBomb);
                }
                else
                {
                    float sin = (Input.mousePosition.y - control_in.transform.position.y) / Vector2.Distance(control_in.transform.position, Input.mousePosition);
                    float cos = (Input.mousePosition.x - control_in.transform.position.x) / Vector2.Distance(control_in.transform.position, Input.mousePosition);
                    end_position.x = control_in.transform.position.x + 25.0f * cos;
                    end_position.y = control_in.transform.position.y + 25.0f * sin;
                    control_out.transform.position = end_position;
                    create_ball(MoveBomb);
                }
            }
            if (Input.GetMouseButtonUp(0) && can_move == true)
            {
                can_move = false;
                in_sky = true;
                control_out.transform.position = control_in.transform.position;
                temp_force = control_in.transform.position;
                temp_force = temp_force - end_position;
            }
            ri.AddForce (temp_force, ForceMode2D.Impulse);
        }
        if (in_sky)
        {
            //totaltime += Time.deltaTime;
            if (Input.GetMouseButtonDown (0) && Vector2.Distance (Input.mousePosition, control_in.transform.position) < 25.0f) {
                can_move = true;
            }
            if (Input.GetMouseButton (0) && can_move) {
                end_position = new Vector2 (50.0f, 50.0f);
                end_position.x = clamp (Input.mousePosition.x, 25.0f, 75.0f);
                control_out.transform.position = end_position;
                Debug.Log(control_out.transform.position);
                MoveBomb.transform.position = new Vector3 (MoveBomb.transform.position.x + (control_out.transform.position.x - 50.0f) / 400, MoveBomb.transform.position.y , 0);
            }
            //松开鼠标 获取力的大小
            if (Input.GetMouseButtonUp (0)&& can_move ) {
                can_move = false;
                if (for_atttack){
                    player.state = Player_controller.player_state.nothing;
                }
                control_out.transform.position = control_in.transform.position;
                for_atttack = false;
            }
        }
    }



    void create_ball(GameObject person){
        Vector2 ball_force;
        ball_force.x = control_out.transform.position.x - control_in.transform.position.x;
        ball_force.y = control_out.transform.position.y - control_in.transform.position.y;
        //ball_ri.AddForce (-ball_force * 20, ForceMode2D.Impulse);
        //for(int i = 0 ;i < 20; i++){
        //    GameObject ball_temp = Instantiate (ball, fx(-ball_force*0.7f,0.05f*i,hero), Quaternion.identity) as GameObject;
        //    Destroy (ball_temp, 0.05f);
        //}
        for(int i = 0; i < balls.Length; i++)
            balls[i].transform.position = (Vector2)fx(-ball_force * 0.7f, 0.05f * i, person);
        Thread.Sleep(20);
        for (int i = 0; i < balls.Length; i++)
            balls[i].SetActive(false);
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
    float clamp(float x,float min,float max){
        if(x > max)
            return max;
        else if(x < min)
            return min;
        else
            return x;
    }
}
