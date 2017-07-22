using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour {
    public struct  rectangle{
        public float x1;
        public float x2;
        public float y1;
        public float y2;
        public rectangle(float _x1, float _x2, float _y1, float _y2)
        {
            x1 = _x1;
            x2 = _x2;
            y1 = _y1;
            y2 = _y2;
        }
    };
    public int rec_number = 3;
    public GameObject ball;
    public rectangle[] barrier;
    public float picture_length;
    public Vector2 vertex1;
    public Vector2 vertex2;
    // Use this for initialization
    void Start () {
        barrier = new rectangle[rec_number];
        barrier[0] = new rectangle(1, 3, -2, -1);
        barrier[1] = new rectangle(-6, -4, -1, 0);
        barrier[2] = new rectangle(-2, -0, 0, 1);
        draw_ball();
    }

    // Update is called once per frame
    void Update () {

    }
    void draw_ball(){
        Vector3 line = exam (vertex1, vertex2); 
        Debug.Log (line);
        float x = vertex1.x;
        while(x < vertex2.x){
            Vector3 temp = new Vector3 (x, 0, 0);
            temp.y = line.x * x * x + line.y * x + line.z; 
            GameObject bollobj = Instantiate (ball, temp, Quaternion.identity);
            x += 0.1f;
        }
    }
    Vector3 parabola(Vector2 V1,Vector2 V2,float a){
        Vector3 ret = new Vector3 (a, 0, 0);
        ret.y = (V2.y - V1.y) / (V2.x - V1.x) - a * (V1.x + V2.x);
        ret.z = (V1.y * V2.x - V2.y * V1.x)/(V2.x-V1.x) +  a * V1.x * V2.x;
        return ret;
    }
    bool exam_linex (Vector3 par, float x,float y1,float y2){
        float y = par.x * x * x + par.y * x + par.z;
        if (y <= y1 - picture_length || y >= y2 + picture_length)
            return true;
        else
            return false;
    }
    bool exam_liney(Vector3 par,float y,float x1,float x2){
        if (par.y * par.y - 4 * par.x * (par.z - y + picture_length) <= 0)
            return true;
        else{
            float   yx1 = (-par.y - Mathf.Sqrt (par.y * par.y - 4 * par.x * (par.z - y + picture_length))) / (2 * par.x);
            float   yx2 = (-par.y + Mathf.Sqrt (par.y * par.y - 4 * par.x * (par.z - y + picture_length))) / (2 * par.x);
            if ((yx1 > x1 && yx1 < x2)|| (yx2 > x1 && yx2 < x2))
                return true;
            else
                return false; 
        }
    }
    bool exam_rec(Vector3 par,rectangle rec){
        if (exam_linex (par, rec.x1, rec.y1, rec.y2) && exam_linex (par, rec.x2, rec.y1, rec.y2) && exam_liney (par, rec.y1, rec.x1, rec.x2) && exam_liney (par, rec.y2, rec.x1, rec.x2))
            return true;
        else
            return false;
    }
    public Vector3 exam(Vector2 V1,Vector2 V2){
        float a = -0.1f;
        while (a > -5.0f)
        {
            //Debug.Log (a);
            for (int i = 0; i < rec_number; i++)
            { 
                //Debug.Log(a);
                if (exam_rec(parabola(V1, V2, a), barrier[i]))
                {
                    float x = V2.x;
                    //Debug.Log(" jjd");

                    while(x < V1.x){
                        Vector3 temp = new Vector3 (x, 0, 0);
                        temp.y = parabola(V1, V2, a).x * x * x + parabola(V1, V2, a).y * x + parabola(V1, V2, a).z; 
                        GameObject bollobj = Instantiate (ball, temp, Quaternion.identity);
                        x += 0.1f;
                    }

                    return parabola(V1, V2, a); 
                }
            }
            a -= 0.1f;
        }
        return new Vector3(0, 0, 0);
    }
}
