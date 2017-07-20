using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour {
    private Slider hpSlider;
    public float health = 100; // 当前值
    public float maxhealth = 100; // 最大值
    public bool isdeath;

    // Use this for initialization
    void Start()
    {
        isdeath = false;
        hpSlider = GetComponentInChildren<Slider>();  // 从子物体获取slider
    }

    // Update is called once per frame
    void Update () {
        // 测试减血效果
        // if (Input.GetMouseButtonDown(0) && health > 0)
        // {
        //     health -= 10;
        //     if (health <= 0)
        //         isdeath = true;
        //     hpSlider.value = health / maxhealth;
        // }
        // 更新血条，其实可以在一个函数中进行，而不是在update里
        hpSlider.value = health / maxhealth;
    }
}
