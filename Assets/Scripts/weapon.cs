using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//武器库(因为没有图还没有做测试，应该会有很多bug，有图后会测试和优化）
public class weapon : MonoBehaviour {

	// Use this for initialization 
    public Image mini_map;
    float screen_width;
    float screen_height;
    float map_height;
    float map_width;

	public GameObject Box;
	public GameObject Drum;
	public GameObject Bird;//轰炸机
	public GameObject Egg;//轰炸机上的炸弹
    private float bird_speed = 2.0f;
	public GameObject SmallBomb;
	public GameObject BigBomb;
	public GameObject TimeBomb;
	public GameObject MoveBomb;
	public GameObject Cannon;
	public GameObject Lightning;
	public GameObject Tsunami;
	public GameObject CannonBomb;

    private Control control;
    private Player_controller player;
    public Camera camera;

    private float totaltime = 0.0f;


	void Start () {
        control = GameObject.FindGameObjectWithTag("GameController").GetComponent<Control>();
        player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Player_controller>();
        screen_width = Screen.width;
        screen_height = Screen.height;
        map_width = mini_map.transform.position.x * 2;
        map_height = (screen_height - mini_map.transform.position.y) * 2;
        SmallBomb.SetActive(false);
        BigBomb.SetActive(false);
        Cannon.SetActive(false);
        Bird.SetActive (false);
        MoveBomb.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void box(){
        if (control.can_attack == true)
        {
            if (Input.GetMouseButtonDown(0) && !(Input.mousePosition.x >= 0 && Input.mousePosition.x <= map_width && Input.mousePosition.y <= screen_height && Input.mousePosition.y >= screen_height - map_height))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.y += 1;
                mousePosition.z = 1;
                GameObject box_obj = Instantiate(Box, mousePosition, Quaternion.identity);
                control.can_attack = false;

                if (control.for_atttack)
                    player.state = Player_controller.player_state.nothing;
                control.for_atttack = false;
            }
        }

	}
    public void drum(){
        if (control.can_attack == true)
        {
            if (Input.GetMouseButtonDown(0) && !(Input.mousePosition.x >= 0 && Input.mousePosition.x <= map_width && Input.mousePosition.y <= screen_height && Input.mousePosition.y >= screen_height - map_height))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.y += 1;
                mousePosition.z = 1;
                GameObject drum_obj = Instantiate(Drum, mousePosition, Quaternion.identity);
                control.can_attack = false;

                if (control.for_atttack)
                    player.state = Player_controller.player_state.nothing;
                control.for_atttack = false;
            }
        }
       
	}
    public void bird_egg(){
		if (Input.GetMouseButtonDown(0) && control.can_layeggs == false)
		{ 
			Vector2 mouseTransform = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Bird.transform.position = new Vector3(-17.0f, mouseTransform.y,1);
            Bird.SetActive (true);
			control.can_layeggs = true;
		}
        if(control.can_layeggs){
            Rigidbody2D ri = Bird.GetComponent<Rigidbody2D>();
            ri.velocity = new Vector2(4, 0);
        }
		if (Input.GetMouseButtonDown (0) && control.can_layeggs == true) {
			GameObject egg_obj = Instantiate (Egg) as GameObject;
            egg_obj.transform.position = Bird.transform.position;
            egg_obj.GetComponent<Rigidbody2D> ().velocity = (Bird.GetComponent<Rigidbody2D> ().velocity) /2;
		}
        if (control.can_layeggs == true && Bird.transform.position.x > 17.0f)
        {
            Bird.SetActive(false);
            control.can_layeggs = false;
            if (control.for_atttack)
                player.state = Player_controller.player_state.nothing;
            control.for_atttack = false;
        }
	}
    public void cannon(GameObject person){
        if(control.can_attack == true){
            Cannon.SetActive(true);
            Cannon.transform.position = person.transform.position;
            control.cannon(person, Cannon, CannonBomb);
            if (control.for_atttack)
                player.state = Player_controller.player_state.nothing;
            control.for_atttack = false;
        }
        control.can_attack = false;
	}
    public void small_bomb(GameObject person){
        if(control.can_attack == true){
            SmallBomb.SetActive(true);
            SmallBomb.transform.position = person.transform.position;
        }
        control.can_attack = false;
        control.addforce_all(SmallBomb);
            
	}
    public void big_bomb(GameObject person){
        if(control.can_attack == true){
            BigBomb.SetActive(true);
            BigBomb.transform.position = person.transform.position;
            control.addforce_all(BigBomb);
            if (control.for_atttack)
                player.state = Player_controller.player_state.nothing;
            control.for_atttack = false;
        }
        control.can_attack = false;
	}
    public void move_bomb(GameObject person){
        if(control.can_attack == true){
            MoveBomb.SetActive(true);
            MoveBomb.transform.position = person.transform.position;
            control.movevbomb(person, MoveBomb);
            if (control.for_atttack)
                player.state = Player_controller.player_state.nothing;
            control.for_atttack = false;
        }
        control.can_attack = false;      
       
	}
    public void time_bomb(GameObject person){
        GameObject timebomb_obj = Instantiate(TimeBomb, person.transform.position, Quaternion.identity);
        control.addforce_all(timebomb_obj);
	}
    public void lightning(){
		Vector3 lig_pos = new Vector3(0,Screen.height,1);
        if (Input.GetMouseButtonDown (0)&&control.can_attack == true) {
			lig_pos.x = Input.mousePosition.x;
            Debug.Log(ScreenToWord(lig_pos));
			GameObject Lightning_obj = Instantiate (Lightning,ScreenToWord(lig_pos) , Quaternion.identity) as GameObject;
			Destroy (Lightning_obj, 2.0f);
            if (control.for_atttack)
                player.state = Player_controller.player_state.nothing;
            control.for_atttack = false;
		}
        control.can_attack = false;  
 	}
    public void tsunami(){
		Vector3 speed = new Vector3(1,0, 0);
        if (Input.GetMouseButtonDown(0)&&control.can_attack == true)
        {
            GameObject Tsunami_obj = Instantiate(Tsunami, new Vector3(-17, -5, 0), Quaternion.identity);
            Rigidbody2D Tsunami_ri = Tsunami_obj.GetComponent<Rigidbody2D>();
            Tsunami_ri.velocity = new Vector2(15, 0);
            if (control.for_atttack)
                player.state = Player_controller.player_state.nothing;
            control.for_atttack = false;
        } 
        control.can_attack = false;
	}
    Vector3 ScreenToWord(Vector3 screen){
        Vector3 view = camera.ScreenToViewportPoint (screen);
        Vector3 word = camera.ViewportToWorldPoint (view);
        return word;
    }

}
