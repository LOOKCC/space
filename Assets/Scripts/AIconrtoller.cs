﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIconrtoller : MonoBehaviour {

    private  Game_controller controller;
    private People people;
    private weapon weapons;
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
        weapons = GameObject.FindGameObjectWithTag("GameController").GetComponent<weapon>();
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
       // if(weapons_canuse[0] > 0 && canshu >= value[0]/total_value && canshu < (value[0]+weapon_value_now[0])/total_value){
            weapons.SmallBomb.SetActive(true);
            //weapons.SmallBomb.transform.position = me.transform.position;
        time += Time.deltaTime*5;
        //Debug.Log(time);

        if(me.transform.position.x < aim.transform.position.x){
            pos.x = me.transform.position.x + time;
            Vector3 temp = find_ways(me, aim);
            pos.y = temp.x * pos.x * pos.x + temp.y * pos.x + temp.z;
            weapons.SmallBomb.transform.position = pos;
            //if(weapons.SmallBomb.transform.position.x > aim.transform.position.x 
        }

        if(me.transform.position.x > aim.transform.position.x){
            pos.x = me.transform.position.x - time;
            Vector3 temp = find_ways(me, aim);
            pos.y = temp.x * pos.x * pos.x + temp.y * pos.x + temp.z;
            weapons.SmallBomb.transform.position = pos;
        }
           // controller.State = Game_controller.Game_State.Game_Bagin;
            return ;
       // }
        /*
        if(weapons_canuse[1] > 0&& canshu >= value[1]/total_value && canshu < (value[2]+weapon_value_now[1])/total_value){
            // use this weapon
            return ;
        }
        if(weapons_canuse[2] > 0 && canshu >= value[2]/total_value && canshu < (value[2]+weapon_value_now[2])/total_value){
            // use this weapon
            return ;
        }
        if(weapons_canuse[3] > 0&& canshu >= value[3]/total_value && canshu < (value[3]+weapon_value_now[3])/total_value) {
            // use this weapon
            return ;
        }
        if(weapons_canuse[4] > 0&& canshu >= value[4]/total_value && canshu < (value[4]+weapon_value_now[4])/total_value){
            // use this weapon
            return ;
        }
        if(weapons_canuse[5] > 0&& canshu >= value[5]/total_value && canshu <= (value[5]+weapon_value_now[5])/total_value){
            // use this weapon
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
            if(weapons_canuse[6]!=0 /*&& pos_above_zero()*/){
                //海啸
                return ;
            }
            if (weapons_canuse[5] != 0) {
                //闪电 
                return;
            }
            if (weapons_canuse [6] != 0) {
                //油桶 3
                return;

            }
            if (weapons_canuse [2] != 0) {
                //箱子 4
                return;
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
}
     
