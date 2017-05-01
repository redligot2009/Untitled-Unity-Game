using UnityEngine;
using System.Collections;

public class LifeSpan : MonoBehaviour {

    public float life = 5;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (life > 0)
        {
            life -= Time.deltaTime;
        }
	    if(life <= 0)
        {
            Destroy(gameObject);
        }
	}
}
