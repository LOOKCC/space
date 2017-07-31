using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour {

    public GameObject SupplyPlane;
    public GameObject SupplyBox;
    public HashSet<Vector3> SupplySet = new HashSet<Vector3>();
    Game_controller controller;
    public float Position1 = -1.2f;
    public float Position2 = 3.0f;
    private float error = 0.1f;
    private float Probabiliy = 0.0f;
    private Rigidbody2D SupplyPlaneRigidbody;
	// Use this for initialization
	void Start () {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game_controller>();
        SupplyPlaneRigidbody = gameObject.GetComponent<Rigidbody2D>();
        SupplyPlane.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (controller.State == Game_controller.Game_State.Game_Supply)
        {
            if (gameObject.transform.position.x >= Position1 - error && gameObject.transform.position.x <= Position1 + error && Probabiliy > 0)
            {
                //supply
                GameObject SupplyBox_obj = Instantiate(SupplyBox, gameObject.transform.position, Quaternion.identity) as GameObject;
                SupplyBoxStore SupplyBoxStore_obj = SupplyBox_obj.GetComponent<SupplyBoxStore>();
                SupplyBoxStore_obj.SupplyThings = CreateThings();
                Probabiliy = 0.0f;
            }
            if (gameObject.transform.position.x >= Position2 - error && gameObject.transform.position.x <= Position2 + error && Probabiliy < 0)
            {
                //supply
                GameObject SupplyBox_obj = Instantiate(SupplyBox, gameObject.transform.position, Quaternion.identity) as GameObject;
                SupplyBoxStore SupplyBoxStore_obj = SupplyBox_obj.GetComponent<SupplyBoxStore>();
                SupplyBoxStore_obj.SupplyThings = CreateThings();
                Probabiliy = 0.0f;
            }
            if (gameObject.transform.position.x > 17.0f)
            {
                SupplyPlane.SetActive(false);
                controller.OutSupply();
            }
        }
    }
    public void GiveSupply()
    {
        SupplyPlane.SetActive(true);
        SupplyPlaneRigidbody.velocity = new Vector2(5.0f, 0);
        Probabiliy = Random.value - 0.5f;
    }
    int CreateThings()
    {
         if(Random.value < 0.5f)
        {
            float temp = Random.value;
            return CreateWeapons(temp);
        }
        else
        {
            float temp1 = Random.value;
            float temp2 = Random.value;
            return CreateWeapons(temp1) * 10 + CreateWeapons(temp2);
        }
    }
    /*
    1   small_bomb  3    0.3
    2   big_bomb  1   0.1
    3   box 1    0.1
    4   drum 1   0.1
    5   time_bomb 2   0.2
    6   move_bomb 2   0.2
    */
    int CreateWeapons(float temp)
    {
        if (temp >= 0 && temp < 0.3f)
        {
            return 1;
        }
        else if (temp >= 0.3f && temp < 0.4f)
        {
            return 2;
        }
        else if(temp >= 0.4f && temp < 0.5f)
        {
            return 4;
        }
        else if(temp >= 0.5f && temp < 0.6f)
        {
            return 5;
        }
        else if(temp >= 0.6f && temp < 0.8f)
        {
            return 6;
        }
        else
        {
            return 7;
        }
    }
}
