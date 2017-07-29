using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIConrtoller : MonoBehaviour {

    private  Game_controller controller;
    private People people;
    private Weapon weapons;
    private GameObject[] peoples = new GameObject[10];

   
    float time = 0.0f;
    Vector3 pos = new Vector3 (0,0,0);

    private float[] weapons_canuse = new float[10]{0,0,0,0,0,0,0,0,0,0};
    private  float[] bag = new float[10]{0,0,0,0,0,0,0,0,0,0};
    public float affect;
    private  float g = 10;
    private float a = 3;
    public Parabola myparabola;
    float x1;
    float x2;
    public int[] weapon_value = new int[6];
    private int [] weapon_value_now = new int[6];
    private float[] value = new float[6];
    Stack<int> shortest = new Stack<int>();
    // Use this for initialization
    void Start () {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_controller>();
        people = GameObject.FindGameObjectWithTag("GameController").GetComponent<People>();
        weapons = GameObject.FindGameObjectWithTag("GameController").GetComponent<Weapon>();
        peoples = people.GetPeople();
        weapon_value[0] = 6;//small
        weapon_value[1] = 10; //big
        weapon_value[2] =  5; //time 
        weapon_value[3] =  4; //move 
        weapon_value[4] =  4; //bird 
        weapon_value[5] =  3; //cannon
        for (int i = 0; i < 6; i++)
            weapon_value_now[i] = 0;
        for (int i = 0; i < 6; i++)
            value[i] = 0.0f;
    }
    void Update(){
        if (weapons.Bird.activeInHierarchy == true)
        {
            if (around(weapons.Bird,peoples[0]) ||around(weapons.Bird,peoples[1]) ||around(weapons.Bird,peoples[2]) ||around(weapons.Bird,peoples[3])||around(weapons.Bird,peoples[4])  )
            {
                GameObject egg_obj = Instantiate(weapons.Egg) as GameObject;
                egg_obj.transform.position = weapons.Bird.transform.position;
            }
            if (weapons.Bird.transform.position.x > 17.0f)
                weapons.Bird.SetActive(false);
        }
    }
    bool around(GameObject a,GameObject b){
        if (a.transform.position.x > b.transform.position.x - 0.08f && a.transform.position.x < b.transform.position.x + 0.08f)
            return true;
        else
            return false;
    }
    public void change_state(){
        int player_and_enemy = find_to_attack();
        behavior_tree(peoples[player_and_enemy / 10], peoples[player_and_enemy % 10]);
    }
    int find_to_attack(){
        float shorts = 100.0f;
        for (int i = 5; i < 10; i++){
            for(int j = 0; j < 5; j++){
                if(Vector3.Distance(peoples[i].transform.position,peoples[j].transform.position) < shorts){
                    shorts = Vector3.Distance(peoples[i].transform.position,peoples[j].transform.position);
                    shortest.Push(i*10 + j);
                }
            }
        }
        return shortest.Pop();
    }
    void behavior_tree(GameObject me, GameObject aim){
        if (me.transform.position.x < aim.transform.position.x) {
            x1 = me.transform.position.x;
            x2 = aim.transform.position.x;
        } else {
            x1 = aim.transform.position.x;
            x2 = me.transform.position.x;
        }
        //如果可以直接打中
        if(/*有补给&&可以达到*/false){
            //拿到补给
            if(find_ways(me,aim) != Vector3.zero)
                attack(me,aim);
            else
                nothing(me,aim);
        }
        else
        {
            if (find_ways(me,aim) != Vector3.zero) {
                attack (me,aim);
            } 
            //打不中
            else 
            {
                Vector3 way = can_move (me,aim);
                //如果可以移动
                if (way != Vector3.zero ) {
                    //计算是否移动、
                    //移动
                    if (Random.value < 0.7f) {
                        //move;
                        //判断是否打得到
                        if (find_ways (me,aim) != Vector3.zero)
                            attack (me,aim);
                        else
                            nothing (me,aim);
                    }
                    //不移动
                    else
                        nothing (me,aim);
                } 
                //不可以移动
                else
                {
                    nothing (me,aim);
                }
            }
        }

    }

    void exam_weapons(){
        for(int i = 0; i < 10; i++){
            if(bag[i] > 0)
                weapons_canuse[i] = 1;
        }
    }
    Vector3 find_ways(GameObject me,GameObject aim){
        return myparabola.exam (me.transform.position, aim.transform.position);
    }
    Vector3 find_ways(Vector3 me_pos, Vector3 aim_pos){
        return myparabola.exam(me_pos, aim_pos);
    }
    void attack(GameObject me,GameObject aim){ 
        //Debug.Log("attack");
        for(int i = 0; i< 10; i++)
            weapons_canuse[i] = 0.0f;
        exam_weapons();
        float environmentx = affect*(Random.value - 0.5f) / 5;
        float canshu = Random.value;
        int total_value = 0;
        for (int i = 0; i < 6; i++) { 
            if (weapons_canuse [i] > 0) {
                total_value += weapon_value [i];
                weapon_value_now [i] = weapon_value [i];
            }
        }
        for (int i = 0; i < 6; i++) {
            float total = 0.0f;
            for (int j = 0; j < i; j++)
                total += weapon_value_now [j];
            value [i] = total;
        }
        /*
        if(weapons_canuse[0] > 0 && canshu >= value[0]/total_value && canshu < (value[0]+weapon_value_now[0])/total_value){
            weapons.SmallBomb.SetActive(true);
            time += Time.deltaTime*5;
            if(me.transform.position.x <= aim.transform.position.x){
                pos.x = me.transform.position.x + time;
                Vector3 temp = find_ways(me, aim);
                pos.y = temp.x * pos.x * pos.x + temp.y * pos.x + temp.z;
                weapons.SmallBomb.transform.position = pos;
                if (weapons.SmallBomb.transform.position.x >= aim.transform.position.x - 0.1f)
                { 
                    controller.State = Game_controller.Game_State.Game_Bagin;
                    time = 0.0f;
                    return;
                }
            }
            if(me.transform.position.x >= aim.transform.position.x){
                pos.x = me.transform.position.x - time;
                Vector3 temp = find_ways(me, aim);
                pos.y = temp.x * pos.x * pos.x + temp.y * pos.x + temp.z;
                weapons.SmallBomb.transform.position = pos;
                if (weapons.SmallBomb.transform.position.x <= aim.transform.position.x )
                { 
                    controller.State = Game_controller.Game_State.Game_Bagin;
                    time = 0.0f;
                    return;
                }
            }
        }

        
        if(weapons_canuse[1] > 0&& canshu >= value[1]/total_value && canshu < (value[2]+weapon_value_now[1])/total_value){
            weapons.BigBomb.SetActive(true);
            time += Time.deltaTime*5;
            if(me.transform.position.x <= aim.transform.position.x){
                pos.x = me.transform.position.x + time;
                Vector3 temp = find_ways(me, aim);
                pos.y = temp.x * pos.x * pos.x + temp.y * pos.x + temp.z;
                weapons.BigBomb.transform.position = pos;
                if (weapons.BigBomb.transform.position.x >= aim.transform.position.x - 0.1f)
                { 
                    controller.State = Game_controller.Game_State.Game_Bagin;
                    time = 0.0f;
                    return;
                }
            }
            if(me.transform.position.x >= aim.transform.position.x){
                pos.x = me.transform.position.x - time;
                Vector3 temp = find_ways(me, aim);
                pos.y = temp.x * pos.x * pos.x + temp.y * pos.x + temp.z;
                weapons.BigBomb.transform.position = pos;
                if (weapons.BigBomb.transform.position.x <= aim.transform.position.x )
                { 
                    controller.State = Game_controller.Game_State.Game_Bagin;
                    time = 0.0f;
                    return;
                }
            }
        }
        if(weapons_canuse[2] > 0 && canshu >= value[2]/total_value && canshu < (value[2]+weapon_value_now[2])/total_value){
            weapons.TimeBomb.SetActive(true);
            time += Time.deltaTime*5;
            if(me.transform.position.x <= aim.transform.position.x){
                pos.x = me.transform.position.x + time;
                Vector3 temp = find_ways(me, aim);
                pos.y = temp.x * pos.x * pos.x + temp.y * pos.x + temp.z;
                weapons.TimeBomb.transform.position = pos;
                if (weapons.TimeBomb.transform.position.x >= aim.transform.position.x - 0.1f)
                { 
                    controller.State = Game_controller.Game_State.Game_Bagin;
                    time = 0.0f;
                    return;
                }
            }
            if(me.transform.position.x >= aim.transform.position.x){
                pos.x = me.transform.position.x - time;
                Vector3 temp = find_ways(me, aim);
                pos.y = temp.x * pos.x * pos.x + temp.y * pos.x + temp.z;
                weapons.TimeBomb.transform.position = pos;
                if (weapons.TimeBomb.transform.position.x <= aim.transform.position.x )
                { 
                    controller.State = Game_controller.Game_State.Game_Bagin;
                    time = 0.0f;
                    return;
                }
            }
        }




        if(weapons_canuse[3] > 0&& canshu >= value[3]/total_value && canshu < (value[3]+weapon_value_now[3])/total_value) {
            weapons.MoveBomb.SetActive(true);
            time += Time.deltaTime*5;
            if(me.transform.position.x <= aim.transform.position.x){
                pos.x = me.transform.position.x + time;
                Vector3 temp = find_ways(me, aim);
                pos.y = temp.x * pos.x * pos.x + temp.y * pos.x + temp.z;
                weapons.MoveBomb.transform.position = pos;
                if (weapons.MoveBomb.transform.position.x >= aim.transform.position.x - 0.1f)
                { 
                    controller.State = Game_controller.Game_State.Game_Bagin;
                    time = 0.0f;
                    return;
                }
            }
            if(me.transform.position.x >= aim.transform.position.x){
                pos.x = me.transform.position.x - time;
                Vector3 temp = find_ways(me, aim);
                pos.y = temp.x * pos.x * pos.x + temp.y * pos.x + temp.z;
                weapons.MoveBomb.transform.position = pos;
                if (weapons.MoveBomb.transform.position.x <= aim.transform.position.x )
                { 
                    controller.State = Game_controller.Game_State.Game_Bagin;
                    time = 0.0f;
                    return;
                }
            }
        }
        */
        if(/*weapons_canuse[4] > 0&& canshu >= value[4]/total_value && canshu < (value[4]+weapon_value_now[4])/total_value*/true){
            float maxy = peoples[0].transform.position.y;
            //float[] temp = new float[5];
            for (int i = 0; i < 5; i++)
            {
              //  temp[i] = peoples[i].transform.position.x;
                if (peoples[i].transform.position.y > maxy)
                    maxy = peoples[i].transform.position.y;
            }
            //List<float> playerx = new List<float>();
            //playerx.AddRange(temp);
            //playerx.Sort();
            weapons.Bird.transform.position = new Vector3(-17.0f,maxy + 2.0f ,1);
            weapons.Bird.SetActive(true);
            Rigidbody2D ri = weapons.Bird.GetComponent<Rigidbody2D>();
            ri.velocity = new Vector2(4, 0);
           

            controller.State = Game_controller.Game_State.Game_Bagin;
            return ;
        }
        /*
        if(weapons_canuse[5] > 0&& canshu >= value[5]/total_value && canshu <= (value[5]+weapon_value_now[5])/total_value*){
            weapons.Cannon.SetActive(true);
            Vector3 pos = new Vector3(0, 0, 0);
            pos.x = me.transform.position.x;
            pos.y = clamp(aim.transform.position.y, me.transform.position.y - 1f, me.transform.position.y + 1f);
            weapons.Cannon.transform.position = pos;
            GameObject bomb = Instantiate (weapons.CannonBomb, weapons.Cannon.transform.position, Quaternion.identity);
            Rigidbody2D ri_bomb = bomb.GetComponent<Rigidbody2D> ();
            if (me.transform.position.x <= aim.transform.position.x)
            {
                ri_bomb.AddForce (new Vector2 (50.0f, 0), ForceMode2D.Impulse);

            }
            if (me.transform.position.x > aim.transform.position.x)
            {
                weapons.Cannon.transform.rotation = Quaternion.Euler(0, 180, 0);
                Debug.Log(weapons.Cannon.transform.rotation.eulerAngles);
                ri_bomb.AddForce (new Vector2 (-50.0f, 0), ForceMode2D.Impulse);
            }
            weapons.Cannon.SetActive(false);
            controller.State = Game_controller.Game_State.Game_Bagin;
            return ; 
        }
        */
    }

    Vector3 can_move(GameObject me ,GameObject aim){
        
        Vector3 pos = new Vector3 (100, 100,0);
        for(int i = 0; i<myparabola.rec_number;i++){
            if( (myparabola.barrier[i].x1 > x1 &&myparabola.barrier[i].x1 < x2) || (myparabola.barrier[i].x2 > x1 && myparabola.barrier[i].x2 < x2)){
                pos.y = myparabola.barrier[i].y2;
                pos.x = (myparabola.barrier [i].x1 + myparabola.barrier [i].x2) / 2;
            }
        }
        if(pos.x != 100 && pos.y != 100){
            return find_ways (me.transform.position, pos); 
        }
        return Vector3.zero;
    }

    void nothing (GameObject me,GameObject aim){
        float canshu = Random.value;
        if (canshu < 0.5f) {
            //放弃
            return ;
        } else {
            if(weapons_canuse[6]!=0){
                if (pos_above_zero() == false)
                {
                    controller.State = Game_controller.Game_State.Game_Bagin;
                    return;
                }
                else
                {
                    GameObject Tsunami_obj = Instantiate(weapons.Tsunami, new Vector3(-17, -5, 0), Quaternion.identity);
                    Rigidbody2D Tsunami_ri = Tsunami_obj.GetComponent<Rigidbody2D>();
                    Tsunami_ri.velocity = new Vector2(15, 0);
                }
            }
            if (weapons_canuse[5] != 0) {
                GameObject Lightning_obj = Instantiate(weapons.Lightning, new Vector3(aim.transform.position.x, aim.transform.position.y + 0.5f, 0), Quaternion.identity);
                Destroy(Lightning_obj, 1.0f);
                controller.State = Game_controller.Game_State.Game_Bagin;
                return ;
            }
            if (weapons_canuse [6] != 0) {
                if (me.transform.position.x <= aim.transform.position.x)
                { 
                    GameObject Lightning_obj = Instantiate(weapons.Drum, new Vector3(aim.transform.position.x - 1f, aim.transform.position.y, 0), Quaternion.identity);
                    controller.State = Game_controller.Game_State.Game_Bagin;
                    return;
                }
                else
                {
                    GameObject Lightning_obj = Instantiate(weapons.Drum, new Vector3(aim.transform.position.x + 1f, aim.transform.position.y, 0), Quaternion.identity);
                    controller.State = Game_controller.Game_State.Game_Bagin;
                    return;
                }
            }
            if (weapons_canuse [2] != 0) {
                    if (me.transform.position.x <= aim.transform.position.x)
                    { 
                        GameObject Lightning_obj = Instantiate(weapons.Box, new Vector3(aim.transform.position.x - 1f, aim.transform.position.y, 0), Quaternion.identity);
                        controller.State = Game_controller.Game_State.Game_Bagin;
                        return;
                    }
                    else
                    {
                        GameObject Lightning_obj = Instantiate(weapons.Box, new Vector3(aim.transform.position.x + 1f, aim.transform.position.y, 0), Quaternion.identity);
                        controller.State = Game_controller.Game_State.Game_Bagin;
                        return;
                    }

                
            }
            //放弃
            return ;
        }
    }

    bool pos_above_zero(){
        for(int i = 5; i < 10; i++){  
            if(peoples[i].transform.position.y < 0.0f)
                return false;
        }
        return true;
    }

    Vector2 V3_to_V2(Vector3 par, GameObject me, GameObject aim)
    {
        Debug.Log(par);
        Vector2 ret = new Vector2(0, 0);
        ret.x = Mathf.Sqrt((a*par.y - a - g)/(2*par.x) + me.transform.position.x*a);
        //ret.x = Mathf.Sqrt(g / (-2 * par.x));
        ret.y = (-2 * me.transform.position.x - par.y) * ret.x;
        if (aim.transform.position.x < me.transform.position.x)
            ret.x = -ret.x;
        if (ret.y < 0)
            ret.y = -ret.y;
        Debug.Log(ret.x);
        Debug.Log(ret.y);
        return ret;
    }
    float clamp(float x, float min,float max){
        if (x < min)
            return min;
        else if (x > max)
            return max;
        else
            return x;
    }
}
     
