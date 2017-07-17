using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class People : MonoBehaviour {
    // 静态变量只会有一个
    public static People instance = new People();
    // 人的数组
    public static GameObject[] peoples;
    // 距离的静态数组
    private static float[] distances = new float[10];
    // 血量数组
    private static HPController[] HPs = new HPController[10];
    // 开始调用的start，获取人物的血条
    private Slider[] hpSliders;
    private void Start()
    {
        int index = 0;
        foreach (var person in peoples)
        {
            HPs[index++] = person.GetComponent<HPController>();
            hpSliders[i] = person.GetComponent<Slider>();
;        }
    }
    // 其他人调用，返回血条数组
    public HPController[] GetHPs()
    {
        return HPs;
    }
    // 其他人或物体调用，参数为调用者传入的transform，计算距离
    public float[] GetDistances (Transform trans)
    {
        int index = 0;
        // 获取距离
        foreach (var player in peoples)
        {
            distances[index++] = (trans.transform.position - player.transform.position).magnitude;
        }
        return distances;
    }
    // 在此函数中完成减血和更新血条
    public void DecraseHealth(float limitDistance, float damage)
    {
        for (int i = 0; i < HPs.Length; i++)
        {
            // 减血
            if (HPs[i].isdeath == false && distances[i] < 4)
            {
                HPs[i].health -= 1.0f / (distances[i] * distances[i] + 0.1f);
                // 更新血条，被打死的使用0而不是负数
                hpSliders[i].value = (HPs[i].health > 0 ? HPs[i].health : 0) / HPs[i].maxhealth;
                // 判断死活
                if (HPs[i].health <= 0)
                {
                    HPs[i].isdeath = true;
                }
            }
        }
    }
}
