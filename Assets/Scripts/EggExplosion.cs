using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EggExplosion : MonoBehaviour {

    public GameObject egg;
    public GameObject small_explosion;

    // Update is called once per frame
    void Update () {

    }
    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "barrier" ){
            People.instance.GetDistances(this.transform);
            GameObject explosion = Instantiate (small_explosion, this.transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f);
            //   Debug.Log("coll");
            Destroy(egg);
            //Invoke ("dehealt", 1f);
            Thread.Sleep(1000);
            People.instance.DecreaseHealth(2f, 1f);
        }
    }
}
