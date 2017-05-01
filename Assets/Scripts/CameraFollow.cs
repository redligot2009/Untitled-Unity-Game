using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    Vector3 myPosition;
    public float cameraSpeed = 20f;

	// Use this for initialization
	void Start () {
        myPosition = transform.position;
        transform.position = myPosition;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float halfWidth = Camera.main.orthographicSize;
        float halfHeight = Camera.main.orthographicSize*(Screen.width/Screen.height);
        myPosition.x = Mathf.Lerp(myPosition.x, target.transform.position.x, Time.deltaTime * cameraSpeed);
        myPosition.y = Mathf.Lerp(myPosition.y, target.transform.position.y, Time.deltaTime * cameraSpeed);
        transform.position = myPosition;
    }
}
