using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class People : MonoBehaviour {
    public static People instance = new People();   // 实例化一个 

    private static GameObject[] people = new GameObject[10];   // 人的数组
    private static float[] distances = new float[10]; // 距离的静态数组
    private static HPController[] HPs = new HPController[10];  // 血量数组
    private static Slider[] hpSliders = new Slider[10];  // 人物的血条
    // start初始化一些变量
    private void Awake()
    {
        for (int i = 1; i <= 5; i++)
        {
            people[i - 1] = GameObject.Find("player" + i);
        }
        for (int i = 1; i <= 5; i++)
        {
            people[i + 4] = GameObject.Find("enemy" + i);
        }

        foreach (var person in people)
        {
            person.GetComponent<Rigidbody2D>();
        }

        Debug.Log("Find all people");
        int index = 0;
        foreach (var person in people)
        {
            HPs[index] = person.GetComponent<HPController>();
            hpSliders[index] = person.GetComponentInChildren<Slider>();
            index++;
;       }
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
        foreach (var player in people)
        {
            distances[index++] = (trans.transform.position - player.transform.position).magnitude;
        }
        return distances;
    }
    // 在此函数中完成减血和更新血条
    public void DecreaseHealth(float limitDistance, float damage)
    {
        for (int i = 0; i < HPs.Length; i++)
        {
            // 减血
            if (HPs[i].isDeath == false && distances[i] < 4)
            {
                HPs[i].health -= damage / (distances[i] * distances[i] + 0.1f);
                // 更新血条，被打死的使用0而不是负数
                hpSliders[i].value = (HPs[i].health > 0 ? HPs[i].health : 0) / HPs[i].maxHealth;
                // 判断死活
                if (HPs[i].health <= 0)
                {
                    HPs[i].isDeath = true;
                }
            }
        }
    }
    // 获取人物数组
    public GameObject[] GetPeople()
    {
        return people;
    }
    // 按照id寻找人
    public GameObject GetOnePerson(int id)
    {
        if (id <= 9 && id >= 0)
        {
            return people[id];
        }
        else
        {
            return people[0];
        }
    }
}
