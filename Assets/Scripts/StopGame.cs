using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StopGame : MonoBehaviour {


	public Button continue_game;
	public Button back_to_menu;
	public Button stop_game;
	public Button restart_game;
	private Text continue_text;
	private Text back_text;
	private Text restart_text;
	// Use this for initialization
	void Start () {

		continue_text = continue_game.GetComponentInChildren<Text> ();
		back_text = back_to_menu.GetComponentInChildren<Text> ();
		restart_text = restart_game.GetComponentInChildren<Text>();
		restart_game.enabled = false;
		restart_game.image.enabled = false;
		restart_text.enabled = false;
		continue_game.enabled = false;
		continue_game.image.enabled = false;
		continue_text.enabled = false;
		back_to_menu.enabled = false;
		back_to_menu.image.enabled = false;
		back_text.enabled = false;
		stop_game.onClick.AddListener (stop_menu);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void stop_menu(){
		stop_game.enabled = false;
		continue_game.enabled = true;
		continue_game.image.enabled = true;
		continue_text.enabled = true;
		back_to_menu.enabled = true;
		back_to_menu.image.enabled = true;
		back_text.enabled = true;
		restart_game.enabled = true;
		restart_game.image.enabled = true;
		restart_text.enabled = true;



		if (Time.timeScale == 1) {
			Time.timeScale = 0;
		}
		continue_game.onClick.AddListener (change_scene_to_game);
		back_to_menu.onClick.AddListener (change_scene_to_menu);
		restart_game.onClick.AddListener (restart);
	}
	void restart(){
		if (Time.timeScale == 0)
			Time.timeScale = 1;
		SceneManager.LoadScene ("test_shoot", LoadSceneMode.Single);
	}
	void change_scene_to_menu(){
		if (Time.timeScale == 0)
			Time.timeScale = 1;
		SceneManager.LoadScene ("level", LoadSceneMode.Single);
	}
	void change_scene_to_game(){
		stop_game.enabled = true;
		continue_game.enabled = false;
		continue_game.image.enabled = false;
		continue_text.enabled = false;
		back_to_menu.enabled = false;
		back_to_menu.image.enabled = false;
		back_text.enabled = false;
		restart_game.enabled = false;
		restart_game.image.enabled = false;
		restart_text.enabled = false;

		if (Time.timeScale == 0)
			Time.timeScale = 1;
	}
}
