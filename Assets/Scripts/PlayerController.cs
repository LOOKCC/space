using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static PlayerController instance = new PlayerController();

	public enum PlayerState{begin, nothing, move, attack};
	PlayerState state;
	bool afterMove ;

	public GameObject control_in; // 大的那个
	public GameObject control_out;  // 小的那个
    private float radius ;
    public new Camera camera;//主摄像机
    public GameObject ball; //轨迹用小球
	Vector2 begin_posion;//开始的位置
	Vector2 end_position; //结束的位置  结束指向开始作为力
	Vector3 now_position;  //判断是否出去大圆
	bool can_move = false; 
	bool can_rot = false;
	bool on_land = true;
    //bool can_change  = true;
    private GameObject[] balls = new GameObject[20];

    private Vector3 inPos;
    private Vector3 outPos;
    private bool changePos = false; // 判断是否更新UI
    private int reset = 0;  // 重置那两个东西

	void Start () {
        radius = control_in.transform.position.x;
		afterMove = false;
        state = PlayerState.begin;
		//state = player_state.begin; 
        //测试
        // state = player_state.attack;
        for (int i = 0; i < balls.Length; ++i)
        {
            balls[i] = Instantiate(ball) as GameObject;
        }
        begin_posion = control_out.transform.position;
        inPos = control_in.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        outPos = control_out.transform.position;
        //Debug.Log("The NO" + ++count + "frame");
        if (changePos)
        {
            ChangePosition();
        }
        if (reset > 0)
        {
            reset = 0;
            ResetPos();
        }
    }

    public IEnumerator ChangeState() {
        // 使用协程的话就需要while和yield
        state = PlayerState.begin;
        // 状态为开始状态
        while (state == PlayerState.begin)
        {
            Debug.Log("Into begin1");
            yield return null;
            // 等待选择状态改变，此处先设置为move
            state = PlayerState.move;
        }
        // 进入move状态，move完毕进入begin状态，进入下面的while
        // 状态在move完毕后改为begin，等待选择下一步
        if (state == PlayerState.move)
        {
            var person = People.instance.GetOnePerson(0);
            if (person == null)
            {
                Debug.Log("Null person");
            }
            //yield return StartCoroutine(Move(person));
            new Thread(Move).Start();
            Debug.Log("Into move");
        }

        //while (state == PlayerState.move)
        //{
        //    state = PlayerState.begin;
        //    //Move(People.instance.GetOnePerson(0));
        //    yield return null;
        //}

        if (state == PlayerState.begin)
        {
            Debug.Log("Into begin2");
            // 此处测试进入attack状态，实际应该弹出菜单等待选择
            //state = PlayerState.attack;
            yield return null;
        }
        // stack只要进入一次，此处可以返回武器发射协程
        if (state == PlayerState.attack)
        {
            //Attack(3, People.instance.GetOnePerson(0));
            Debug.Log("Into sttack");
            yield return null;
        }
        // 攻击完或者选择到nothing进入此处，改变GameState，概率0.3进入补给，0.7进入enemy
        if (state == PlayerState.nothing)
        {
            Debug.Log("Into nothing");
            yield return null;
        }
        Debug.Log("Into change");
        // 此处使用概率选择下一步去哪里？可以返回协程
        if (Random.value < 0.3f)
        {
            Debug.Log("Into supply");
            //yield return StartCoroutine("supply");
        }
        // 此处测试进入player查看是否可以多次启动协程，实际应进入enemy状态
        //GameController.instance.SetGameState(GameController.GameState.Game_Player);
        Debug.Log("End");
        // 本次协程结束
        yield break;
    }
    GameObject hero = People.instance.GetOnePerson(0);

    public /*IEnumerator*/ void Move(){
        Debug.Log("Into Move");
		Rigidbody2D ri = hero.GetComponent<Rigidbody2D> ();
        Vector2 temp_force = Vector2.zero;
        
        while (!Input.GetMouseButtonDown(0) || !(Vector2.Distance(Input.mousePosition, inPos) < radius / 2))
        {
            Thread.Sleep(200);
            //yield return null;
        }
        //在固定区域点下
        Debug.Log("Get the mouse down;");
        can_move = true;
        //Debug.Log("Get position" + Input.mousePosition))
        //Debug.Log("Get in position" + control_in.transform.position);
        //if (Input.GetMouseButtonDown(0) && Vector2.Distance(Input.mousePosition, inPos) < 25.0f)
        //{
        //    Debug.Log("get the mouse");
        //    can_move = true;
        //    yield return null;
        //}
        //推拽 分两种，鼠标在圆内和在圆外
        Debug.Log("Start move");
        while (Input.GetMouseButton (0) && can_move) {
			//在圆内
            if (Vector2.Distance (inPos, Input.mousePosition) <radius/2) {
				end_position = Input.mousePosition;
                //changePos = true;
                //control_out.transform.position = end_position;
                //SetOut(end_position);
				//在圆外
			} else {
				float sin = (Input.mousePosition.y - inPos.y) / Vector2.Distance (inPos, Input.mousePosition);
				float cos = (Input.mousePosition.x - inPos.x) / Vector2.Distance (inPos, Input.mousePosition);
                end_position.x = inPos.x +radius/2 * cos;
                end_position.y = inPos.y +radius/2 * sin;
                //control_out.transform.position = end_position;
                //SetOut(end_position);
                //SetOut(end_position);
                changePos = true;
			}
            //显示轨迹
            CreateBall(hero);
            Thread.Sleep(33);
            //yield return null;
            changePos = false;
		}
		//松开鼠标 获取力的大小
        if (Input.GetMouseButtonUp (0)&&can_move) {
            // 修改状态为begin
            state = PlayerState.begin;
			can_move = false;
			//Debug.Log ("here");
			can_rot = true;
			on_land = false;
			//control_out.transform.position = control_in.transform.position;
            //ResetPos();
            temp_force = inPos;
			temp_force = temp_force - end_position;
		}
		ri.AddForce (temp_force, ForceMode2D.Impulse);
        Debug.Log("Out Move");
        reset = 1;// 给出重置信号
        //yield break;
	}
    
    private void ChangePosition()
    {
        control_out.transform.position = end_position;
    }

    private void ResetPos()
    {
        control_out.transform.position = control_in.transform.position;
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
