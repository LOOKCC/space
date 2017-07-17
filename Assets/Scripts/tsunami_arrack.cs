using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class tsunami_arrack : MonoBehaviour {


    private GameObject[] objs;
    private HPController[] healths;

    // Use this for initialization
    void Start () {
        healths = People.instance.GetHPs();
        objs = People.instance.GetPeople();
        Thread.Sleep(2000);
        DecreaseHealth();
	}

	void DecreaseHealth(){
        for (int i = 0; i < objs.Length; i++)
        {
            if (!healths[i].isDeath)
            {
                // 判断此位置海啸是否可以攻击到
                if (objs[i].transform.position.y < 0)
                {
                    healths[i].health -= 70;
                }
                // 判断是否
                if (healths[i].health < 0)
                {
                    // 设置为死亡
                    healths[i].isDeath = true;
                    objs[i].GetComponentInChildren<Slider>().value = 0;
                } else
                {
                    // 设置血条
                    objs[i].GetComponentInChildren<Slider>().value = healths[i].health / healths[i].maxHealth;
                }
            }
           
                
        }
    }
}
