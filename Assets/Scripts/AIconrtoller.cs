﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIconrtoller : MonoBehaviour {

    private  Game_controller controller;
    private People people;
    private weapon weapons;
    private GameObject[] peoples = new GameObject[10];


    private float[] weapons_canuse;
    public float[] bag;
    public float affect;
    public float g;
    public Parabola myparabola;
    float x1;
    float x2;
    public  GameObject me;
    public GameObject aim;
    public int[] weapon_value;
    private int [] weapon_value_now;
    private float[] value;
    // Use this for initialization
    void Start () {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_controller>();
        people = GameObject.FindGameObjectWithTag("GameController").GetComponent<People>();
        weapons = GameObject.FindGameObjectWithTag("GameController").GetComponent<weapon>();
        peoples = people.GetPeople();
        weapon_value = new int[6];
        weapon_value[0] = 6;//small
        weapon_value[1] = 10; //big
        weapon_value[2] =  5; //time 
        weapon_value[3] =  4; //move 
        weapon_value[4] =  4; //bird 
        weapon_value[5] =  3; //cannon
        weapon_value_now = new int[6];
        for (int i = 0; i < 6; i++)
            weapon_value_now[i] = 0;
        for (int i = 0; i < 6; i++)
            value[i] = 0.0f;
        //获取左右关系
        if (me.transform.position.x < aim.transform.position.x) {
            x1 = me.transform.position.x;
            x2 = aim.transform.position.x;
        } else {
            x1 = aim.transform.position.x;
            x2 = me.transform.position.x;
        }
        //测试用，初始化背包数组
        bag = new float[10];
        for(int i = 0; i< 10; i++)
            bag[0] = 0.0f;
        //初始化武器数组
        weapons_canuse = new float[10];
        for(int i = 0; i< 10; i++)
            weapons_canuse[0] = 0.0f;
        exam_weapons ();
        //如果可以直接打中
        if(/*有补给&&可以达到*/true){
            //拿到补给
            if(find_ways(me.transform.position,aim.transform.position) != Vector3.zero)
                attack();
            else
                nothing();
        }
        else
        {
            if (find_ways(me.transform.position,aim.transform.position) != Vector3.zero) {
                attack ();
            } 
            //打不中
            else 
            {
                Vector3 way = can_move ();
                //如果可以移动
                if (way != Vector3.zero ) {
                    //计算是否移动、
                    //移动
                    if (Random.value < 0.7f) {
                        //move;
                        //判断是否打得到
                        if (find_ways (me.transform.position,aim.transform.position) != Vector3.zero)
                            attack ();
                        else
                            nothing ();
                    }
                    //不移动
                    else
                        nothing ();
                } 
                //不可以移动
                else
                {
                    nothing ();
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
    Vector3 find_ways(Vector3 me_pos,Vector3 aim_pos){
        return myparabola.exam (me_pos, aim_pos);
    }
    void attack(){ 
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
        if(weapons_canuse[0] > 0){
            // use this weapon
            return ;
        }
        if(weapons_canuse[1] > 0){
            // use this weapon
            return ;
        }
        if(weapons_canuse[2] > 0){
            // use this weapon
            return ;
        }
        if(weapons_canuse[3] > 0){
            // use this weapon
            return ;
        }
        if(weapons_canuse[4] > 0){
            // use this weapon
            return ;
        }
        if(weapons_canuse[5] > 0){
            // use this weapon
            return ; 
        }
    }
    Vector3 can_move(){
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

    void nothing (){
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
    /*
    bool pos_above_zero(){
        for(int i = 0; i < 10; i++){  
            if(object[i].tr.po < 0)
                return false;
        }
        return true;
    }
    */
    Vector2 V3_to_V2(Vector3 par)
    {
        Vector2 ret = new Vector2(0, 0);
        ret.x = Mathf.Sqrt(g / (-2 * par.x));
        ret.y = (-2 * me.transform.position.x - par.y) * ret.x;
        return ret;
    }
}
     
