using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPerson : MonoBehaviour {
    // 在你的脚本中启动这个协程就会返回一个你选择的人物的ID(int)
    public  IEnumerator<int> SelectOnePerson()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouse = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mouse);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // 返回ID
                    yield return hit.transform.GetComponent<ID>().id;
                    break;
                }
            }
        }
        // 默认返回0
        yield return 0;
    }
}
