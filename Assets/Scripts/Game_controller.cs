using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_controller : MonoBehaviour {
	
	public enum Game_State{Game_Stop,Game_Over,Game_Player,Game_Enemy,Game_Supply};
	public Game_State State;
	Game_State  preState;
	// Use this for initialization
	void Start () {
		State = Game_State.Game_Player;
		preState = Game_State.Game_Player;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Change_State(Game_State State){
		switch(State){
			case Game_State.Game_Stop:
				
				break;
			case Game_State.Game_Over:
				//
				break;
			case Game_State.Game_Player:
				//
			break;
			case Game_State.Game_Enemy:
				//
				break;
			case Game_State.Game_Supply:
				//
				break;
		}
	}
}
