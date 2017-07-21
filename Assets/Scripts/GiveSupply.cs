using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GiveSupply : MonoBehaviour {
    private GameObject plane; //补给飞机,需要设置他的位置
    private Rigidbody2D rb2d;
    public float speed;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        //plane = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSupply(Vector3 position, GameObject supply)
    {
        rb2d.velocity = new Vector2(speed, 0);  // 假设speed大于0，从左往右飞
        // 当没有达到指定位置，休眠
        while (rb2d.transform.position.x < position.x)
        {
            // 每秒30次检测
            Thread.Sleep(33);
        }
        // 生成supply
        GameObject supplyItem = Instantiate(supply, position, Quaternion.identity) as GameObject;
    }
}
