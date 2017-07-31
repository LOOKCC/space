using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_controller : MonoBehaviour {
	
	public enum Game_State{Game_Bagin,Game_Level, Game_Stop,Game_Over,Game_Player,Game_Enemy,Game_Supply};
	public Game_State State;
	public  Game_State  preState;

    private Player_controller player; 
    private AIConrtoller AI;
    private Supply supply;

    public float SupplyProbability = 0.3f;  
        
	// Use this for initialization
	void Start () {
        State = Game_State.Game_Enemy  ;
		preState = Game_State.Game_Player;
        player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Player_controller>();
        AI = GameObject.FindGameObjectWithTag("AIController").GetComponent<AIConrtoller>();
        supply = GameObject.FindGameObjectWithTag("GameSupply").GetComponent<Supply>();
	}
	
	// Update is called once per frame
	void Update () {
        Change_State();
        //Debug.Log(State);
	}
	void Change_State(){
		switch(State){
            case Game_State.Game_Bagin:
                break;
            case Game_State.Game_Level:
                break;
			case Game_State.Game_Stop:
				break;
			case Game_State.Game_Over:
				break;
            case Game_State.Game_Player:
                int temp =  player.ChoosePerson();
                preState = Game_State.Game_Player;
                player.changestate();
			    break;
            case Game_State.Game_Enemy:
                preState = Game_State.Game_Enemy;
                AI.change_state();
				break;
			case Game_State.Game_Supply:
                //supply.XXXXX
				break;
		}
	}
    public void InSupply (){
        if(Random.value < 0.7){
            if(preState  == Game_State.Game_Enemy)
                State = Game_State.Game_Player;
            else
                State = Game_State.Game_Enemy;
        }else
            State = Game_State.Game_Supply;
    }
    public void  OutSupply(){
        if (preState == Game_State.Game_Enemy)
        {
            State = Game_State.Game_Player;
        }
        else
        {
            State = Game_State.Game_Enemy;
        }
    }
}