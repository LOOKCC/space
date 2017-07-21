<<<<<<< HEAD
﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController instance = new GameController();

	public enum GameState{Game_Stop,Game_Over,Game_Player,Game_Enemy,Game_Supply, Game_Wait};
    private static GameState state;
	GameState  preState;
	// Use this for initialization
	void Start () {
		state = GameState.Game_Player;
		preState = GameState.Game_Player;
	}
	
	// Update is called once per frame
	void Update () {
        Change_State();
	}
	void Change_State(/* Game_State State */){
		switch(state){
			case GameState.Game_Stop:
				
				break;
			case GameState.Game_Over:
				//
				break;
			case GameState.Game_Player:
                state = GameState.Game_Wait;
                Debug.Log("Into player;");
                //Thread.Sleep(50);
                //Player_controller.instance.StartThread();
                //StartCoroutine(PlayerController.instance.ChangeState());
                StartCoroutine(PlayerController.instance.Move(People.instance.GetOnePerson(0)));
				//
			break;
			case GameState.Game_Enemy:
				//
				break;
			case GameState.Game_Supply:
				//
				break;
            case GameState.Game_Wait:
                //Debug.Log("Haha");


                break;
		}
	}
    
    public void SetGameState(GameState _state)
    {
        Thread.Sleep(1000);
        state = _state;
    }
    // 获取当前状态，由
    public GameState GetCurrentState()
    {
        return state;
    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController instance = new GameController();

	public enum GameState{Game_Stop,Game_Over,Game_Player,Game_Enemy,Game_Supply, Game_Wait};
    private static GameState state;
	GameState  preState;
	// Use this for initialization
	void Start () {
		state = GameState.Game_Player;
		preState = GameState.Game_Player;
	}
	
	// Update is called once per frame
	void Update () {
        Change_State();
	}
	void Change_State(/* Game_State State */){
		switch(state){
			case GameState.Game_Stop:
				
				break;
			case GameState.Game_Over:
				//
				break;
			case GameState.Game_Player:
                state = GameState.Game_Wait;
                Debug.Log("Into player;");
                //Thread.Sleep(50);
                //Player_controller.instance.StartThread();
                StartCoroutine(PlayerController.instance.ChangeState());
                //StartCoroutine(PlayerController.instance.Move(People.instance.GetOnePerson(0)));
                //
                break;
			case GameState.Game_Enemy:
				//
				break;
			case GameState.Game_Supply:
				//
				break;
            case GameState.Game_Wait:
                //Debug.Log("Haha");


                break;
		}
	}
    
    public void SetGameState(GameState _state)
    {
        Thread.Sleep(1000);
        state = _state;
    }
    // 获取当前状态，由
    public GameState GetCurrentState()
    {
        return state;
    }
}
>>>>>>> e2ea1b2211326bdfbc31d43ec35e510ffa395598
