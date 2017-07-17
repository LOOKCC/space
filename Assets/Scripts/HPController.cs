using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPController : MonoBehaviour {
    private Slider hpSlider;    // 血条slider
    public float health = 100;  // 当前值
    public float maxHealth = 100; // 最大值
    public bool isDeath;

    // Use this for initialization
    void Start()
    {
        isDeath = false;
        // 从子物体获取slider
        hpSlider = GetComponentInChildren<Slider>();  
    }
}
