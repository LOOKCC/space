using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class island_move : MonoBehaviour {

	public Vector3 begin_pos;
	public Vector3 end_pos;
	public Vector3 speed;
	private Rigidbody2D ri;
    
	// Use this for initialization
	void Start () {
		ri = GetComponent<Rigidbody2D> ();
		this.transform.position = begin_pos;
		ri.velocity = speed; 
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.x > end_pos.x)
			ri.velocity = -speed;
		if (this.transform.position.x < begin_pos.x)
			ri.velocity = speed;
	}

}
