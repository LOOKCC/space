using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour {

    public GameObject SupplyPlane;
    public GameObject SupplyThings;
    public Vector3 Position1;
    public Vector3 Position2;
    private float Probabiliy = 0.0f;
    private Rigidbody2D SupplyPlaneRigidbody;
	// Use this for initialization
	void Start () {
        SupplyPlaneRigidbody = gameObject.GetComponent<Rigidbody2D>();
        SupplyPlane.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.position.x    && Probabiliy > 0)
        {
            //supply
            //GameObject 
            Probabiliy = 0.0f;
        }
        if (gameObject.transform.position.x && Probabiliy < 0)
        {
            //supply
            //GameObject
            Probabiliy = 0.0f;
        }
        if(gameObject.transform.position.x > 17.0f)
        {
            
        }
    }
    public void GiveSupply()
    {
        SupplyPlane.SetActive(true);
        SupplyPlaneRigidbody.velocity = new Vector2(5.0f, 0);
        Probabiliy = Random.value - 0.5f;
    }
    void CreateThings()
    {

    }
}
