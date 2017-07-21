using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour {

	public enum player_state{begin, nothing, move, attack};
	public  player_state state;
	private  Game_controller controller;
    private People people;
    private weapon weapons;
    private Control control;
    private GameObject[] peoples = new GameObject[10];

	void Start () {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_controller>();
        people = GameObject.FindGameObjectWithTag("GameController").GetComponent<People>();
        weapons = GameObject.FindGameObjectWithTag("GameController").GetComponent<weapon>();
        control = GameObject.FindGameObjectWithTag("GameController").GetComponent<Control>();
        peoples = people.GetPeople();
		state = player_state.begin; 
	}
    public bool wait = false;
	
	// Update is called once per frame
	void Update () {
		if (controller.State == Game_controller.Game_State.Game_Player) {
            changestate ();
		}
	}
	void changestate(){
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
                control.after_move = true;
			    state = player_state.begin;
			    break;
            case (player_state.attack):
                //Debug.Log("case");
                attack(1, objs[0]);
                state = player_state.nothing;
                Debug.Log(state);
               // wait = true;
			    break;
            case (player_state.nothing):
                Debug.Log("hh");
			   // controller.State = Game_controller.Game_State.Game_Enemy;
			    state = player_state.begin;
                control.after_move = false;
			    break;
		}
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
