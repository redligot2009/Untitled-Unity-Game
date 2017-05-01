using UnityEngine;
using System.Collections;

public class LinearTravel : MonoBehaviour {

    public Vector3 velocity = Vector3.zero;
    public Vector3 moveSpeed = new Vector3(5f, 5f, 5f);
	
	// Update is called once per frame
	void Update () {
        transform.Translate(velocity * Time.deltaTime);
	}
}
