using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_controller : MonoBehaviour {
    public static Game_controller instance = new Game_controller();

	public enum Game_State{Game_Stop,Game_Over,Game_Player,Game_Enemy,Game_Supply, Game_Wait};
	public Game_State state;
	Game_State  preState;
	// Use this for initialization
	void Start () {
		state = Game_State.Game_Player;
		preState = Game_State.Game_Player;
	}
	
	// Update is called once per frame
	void Update () {
        Change_State();
	}
	void Change_State(/* Game_State State */){
		switch(state){
			case Game_State.Game_Stop:
				
				break;
			case Game_State.Game_Over:
				//
				break;
			case Game_State.Game_Player:
                state = Game_State.Game_Wait;
                //Thread.Sleep(50);
                //Player_controller.instance.StartThread();
                StartCoroutine(Player_controller.instance.ChangeState());
				//
			break;
			case Game_State.Game_Enemy:
				//
				break;
			case Game_State.Game_Supply:
				//
				break;
            case Game_State.Game_Wait:
                break;
		}
	}
    
    public void ChangeState(Game_State state)
    {
        Thread.Sleep(100);
        this.state = state;
    }
}
