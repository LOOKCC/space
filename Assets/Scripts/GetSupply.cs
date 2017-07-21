using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 此脚本应挂在supply身上
public class GetSupply : MonoBehaviour {
    int supplyID;   // 物品ID，传给碰撞者？
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    // 人碰撞到supply，将supply销毁，人物自增物品
    // 有个小问题，如果物品未落地就被获取？？
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.StartsWith("player") || collision.tag.StartsWith("enemy"))
        {
            // 可以获取物品的ID进行操作
            // 可以尝试使用collision的脚本为其增加物体
            //collision.gameObject.GetComponent<AddItem>().AdItem(supplyID);
            // TODO
            Destroy(this);
        }
    }
}
