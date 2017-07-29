using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour {

	public enum player_state{begin, nothing, move, attack};
	public  player_state state;
	private  Game_controller controller;
    private People people;
    private Weapon weapons;
    private Control control;
    private GameObject[] peoples = new GameObject[10];
    //for test
    public int a = 1;

	void Start () {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_controller>();
        people = GameObject.FindGameObjectWithTag("GameController").GetComponent<People>();
        weapons = GameObject.FindGameObjectWithTag("GameController").GetComponent<Weapon>();
        control = GameObject.FindGameObjectWithTag("GameController").GetComponent<Control>();
        peoples = people.GetPeople();
		state = player_state.begin; 
	}
	
	void Update () {
	}

    public void changestate(){
		switch(state){
            case (player_state.begin):
            if(control.after_move == false){
				if(a == 0){
					state = player_state.nothing;
				}
				else if(a == 1){
					state = player_state.move;
				}else if( a == 2){
					state = player_state.attack;
				}
			}
            if (control.after_move == true){
				if( a == 0){
					state = player_state.nothing;
                }else if( a== 2){
					state = player_state.attack;
				}
			}
			break;
            case (player_state.move):
                control.for_move = true;
                control.addforce_all(peoples[0]);
			    break;
            case (player_state.attack):
                control.for_atttack = true;
                attack(0, peoples[0]);
			    break;
            case (player_state.nothing):
                Debug.Log("here");
                float x = Random.value;
                if (x > 0.5)
                    controller.State = Game_controller.Game_State.Game_Enemy;
                else
                    controller.State = Game_controller.Game_State.Game_Supply;
                control.can_attack = true;
			    state = player_state.begin;
                control.after_move = false;
			    break;
		}
	}
        

    void attack(int NO, GameObject person){
        switch (NO)
        {
            case (0):
                weapons.small_bomb(person);
                break;
            case (1):
                weapons.big_bomb(person);
                break;
            case (2):
                weapons.move_bomb(person);
                break;
            case (3):
                weapons.time_bomb(person);
                break;
            case(4):
                weapons.lightning();
                break;
            case(5):
                weapons.tsunami();
                break;
            case(6):
                weapons.box();
                break;
            case(7):
                weapons.drum();
                break;
            case(8):
                weapons.bird_egg();
                break;
            case(9):
                weapons.cannon(person);
                break;
        }
    }
}
