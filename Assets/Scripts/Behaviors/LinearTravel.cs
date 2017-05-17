using UnityEngine;
using System.Collections;

public class LinearTravel : MonoBehaviour {

    public float velocity = 0;
    public float moveSpeed = 5f;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(velocity * transform.up * Time.deltaTime);
	}
}
