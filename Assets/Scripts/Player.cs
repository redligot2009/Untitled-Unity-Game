using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Controller2D myController;
    BoxCollider2D myCollider;
    public GameObject bulletPrefab;
    public Vector2 maxDistanceShoot = new Vector2(0.25f, 2f);

	// Use this for initialization
	void Start () {
        myController = GetComponent<Controller2D>();
        myCollider = GetComponent<BoxCollider2D>();
	}


    float timer = 0;
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            myController.velocity.x -= 1 * Time.deltaTime;
        }
        if (myController.velocity.x < 0 && myController.myCollisions.left)
        {
            myController.velocity.x = 0;
        }
        if (Input.GetKey(KeyCode.D))
        {
            myController.velocity.x += 1 * Time.deltaTime;
        }
        if (myController.velocity.x > 0 && myController.myCollisions.right)
        {
            myController.velocity.x = 0;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            myController.velocity.x = Mathf.Lerp(myController.velocity.x, 0, Time.deltaTime * 8f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            myController.velocity.y -= 1 * Time.deltaTime;
        }
        if (myController.velocity.y < 0 && myController.myCollisions.down)
        {
            myController.velocity.y = 0;
        }
        if (Input.GetKey(KeyCode.W))
        {
            myController.velocity.y += 1 * Time.deltaTime;
        }
        if (myController.velocity.y > 0 && myController.myCollisions.up)
        {
            myController.velocity.y = 0;
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            myController.velocity.y = Mathf.Lerp(myController.velocity.y, 0, Time.deltaTime * 8f);
        }

        int mouseDirX = 0, mouseDirY = 0;
        float angle = 0;

        if(Input.GetKey(KeyCode.UpArrow))
        {
            mouseDirY = 1;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            mouseDirY = -1;
        }
        else
        {
            mouseDirY = 0;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            mouseDirX = -1;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            mouseDirX = 1;
        }
        else
        {
            mouseDirX = 0;
        }
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0 && (mouseDirX != 0 || mouseDirY != 0))
        {
            //Debug.Log("YO");
            GameObject clone = Instantiate(bulletPrefab);
            if (clone != null)
            {
                LinearTravel mover = clone.GetComponent<LinearTravel>();
                Vector3 mouseDir = new Vector3(mouseDirX, mouseDirY).normalized;
                clone.transform.position = transform.position;
                mover.velocity = mover.moveSpeed;
                angle = Vector3.Angle(Vector3.zero, mouseDir);
                mover.transform.eulerAngles = Quaternion.Euler(0, 0, 45) * mover.transform.eulerAngles;
            }
            timer = 0.25f;
        }
    }
}
