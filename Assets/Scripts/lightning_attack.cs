using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lightning_attack : MonoBehaviour {	
	private GameObject[] objs;
	private HPController[]  healths;

	// Use this for initialization
	void Start () {
        healths = People.instance.GetHPs();
        objs = People.instance.GetPeople();
		Invoke ("dehealth", 1.0f);
		Destroy (this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void dehealth(){
		//for( int i = 0; i < 10; i++){
			//if(objs[i].transform.position.x < this.transform.position.x + 0.8f && objs[i].transform.position.x >this.transform.position.x - 0.8f)
			//	healths [i].health -= 50;
		//}
        for (int i = 0; i < objs.Length; ++i)
        {
            // 判断是否击中
            if (Mathf.Abs( objs[i].transform.position.x - this.transform.position.x) < 0.8f)
            {
                healths[i].health -= 50;
                if (healths[i].health <= 0)
                {
                    objs[i].GetComponentInChildren<Slider>().value = 0;
                    healths[i].isDeath = true;
                }
                else
                {
                    objs[i].GetComponentInChildren<Slider>().value = healths[i].health / healths[i].maxHealth;
                }
            }
        }
	 }
}
