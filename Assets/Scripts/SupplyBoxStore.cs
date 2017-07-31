using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyBoxStore : MonoBehaviour {

    public GameObject supply1;
    public GameObject supply2;
    public GameObject supply3;
    public GameObject supply4;

    private Supply supply;
    public int SupplyThings = 0; 
	// Use this for initialization
	void Start () {
        supply = GameObject.FindGameObjectWithTag("GameSupply").GetComponent<Supply>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "bg")
        {
            supply.SupplySet.Add(gameObject.transform.position);
        }
        if(collision.gameObject.tag == "player")
        {
            supply.SupplySet.Remove(gameObject.transform.position);
        }
    }

}
