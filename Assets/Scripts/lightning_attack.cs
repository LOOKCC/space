using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightning_attack : MonoBehaviour {

	
	private GameObject[] objs;

	private HPController[]  healths;

	// Use this for initialization
	void Start () {
		objs = new GameObject[10];
		healths = new HPController[10];

        objs[0] = GameObject.Find("player1");
        objs[1] = GameObject.Find("player2");
        objs[2] = GameObject.Find("player3");
        objs[3] = GameObject.Find("player4");
        objs[4] = GameObject.Find("player5");
        objs[5] = GameObject.Find("enemy1");
        objs[6] = GameObject.Find("enemy2");
        objs[7] = GameObject.Find("enemy3");
        objs[8] = GameObject.Find("enemy4");
        objs[9] = GameObject.Find("enemy5");

        for(int i = 0; i < 10; i++)
            healths[i] = objs[i].GetComponent<HPController>();
		Invoke ("dehealth", 1.0f);
		Destroy (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void dehealth(){
		for( int i = 0; i < 10; i++){
			if(objs[i].transform.position.x < this.transform.position.x + 0.8f && objs[i].transform.position.x >this.transform.position.x - 0.8f)
				healths [i].health -= 50;
		}
	 }
}
