using UnityEngine;
using System.Collections;

public struct Collisions{
    public bool left;
    public bool right;
    public bool up;
    public bool down;
    public Vector2 faceDir;
}

public class Controller2D : MonoBehaviour {

    public LayerMask walls;

    public Vector3 myPosition;
    public Vector3 maxVelocity;
    public Vector3 velocity;
    public Vector3 moveSpeed;
    public Vector3 moveDir;
    public Vector3 touchWall;

    public int rayCountX = 4;
    public int rayCountY = 4;

    public float raySpacingX;
    public float raySpacingY;

    public float skinWidth = 0.05f;

    BoxCollider2D myCollider;

    public Collisions myCollisions;

	// Use this for initialization
	void Start ()
    {
        myCollider = GetComponent<BoxCollider2D>();
        UpdateRaySpacing();
	}
	
    void UpdateRaySpacing()
    {
        raySpacingX = (myCollider.size.x * Mathf.Abs(transform.localScale.x)) / (rayCountX - 1);
        raySpacingY = (myCollider.size.y * Mathf.Abs(transform.localScale.y)) / (rayCountY - 1);
    }

    void resetCollisions()
    {
        myCollisions.left = myCollisions.right = myCollisions.up = myCollisions.down = false;
    }

    void HorizontalCollisions(ref Vector3 deltaMovement)
    {
        float directionX = myCollisions.faceDir.x;
        float rayDistance = Mathf.Abs(deltaMovement.x) + skinWidth;
        for (int i = 0; i < rayCountX; i ++)
        {
            Vector2 rayOrigin = (directionX == 1) ? new Vector2(myCollider.bounds.max.x, myCollider.bounds.max.y) : new Vector2(myCollider.bounds.min.x, myCollider.bounds.max.y);
            rayOrigin -= i * Vector2.up * raySpacingX;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayDistance, 1<<LayerMask.NameToLayer("Walls"));

            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit)
            {
                /*if(hit.distance == 0)
                {
                    continue;
                }*/
                //Debug.Log("YO");
                deltaMovement.x = (hit.distance - skinWidth) * directionX;
                rayDistance = hit.distance;
                myCollisions.left = directionX == -1;
                myCollisions.right = directionX == 1;
            }
        }
    }

    void VerticalCollisions(ref Vector3 deltaMovement)
    {
        float directionY = myCollisions.faceDir.y;
        float rayDistance = Mathf.Abs(deltaMovement.y) + skinWidth;
        for (int i = 0; i < rayCountY; i++)
        {
            Vector2 rayOrigin = (directionY == 1) ? new Vector2(myCollider.bounds.max.x, myCollider.bounds.max.y) : new Vector2(myCollider.bounds.max.x, myCollider.bounds.min.y);
            rayOrigin -= i * Vector2.right * raySpacingY;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayDistance, 1 << LayerMask.NameToLayer("Walls"));

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit)
            {
                /*if(hit.distance == 0)
                {
                    continue;
                }*/
                //Debug.Log("YO");
                deltaMovement.y = (hit.distance - skinWidth) * directionY;
                rayDistance = hit.distance;
                myCollisions.down = directionY == -1;
                myCollisions.up = directionY == 1;
            }
        }
    }

    public void Move(Vector3 deltaMovement)
    {
        UpdateRaySpacing();
        resetCollisions();
        if (deltaMovement.x != 0)
        {
            myCollisions.faceDir.x = Mathf.Sign(deltaMovement.x);
        }
        if (deltaMovement.y != 0)
        {
            myCollisions.faceDir.y = Mathf.Sign(deltaMovement.y);
        }
        HorizontalCollisions(ref deltaMovement);
        VerticalCollisions(ref deltaMovement);
        transform.Translate(deltaMovement);
    }
    
	void Update ()
    {
        Move(velocity);
        moveDir = new Vector3(Mathf.Sign(velocity.x), Mathf.Sign(velocity.y));
        if (Input.GetKey(KeyCode.A))
        {
            velocity.x -= 1 * Time.deltaTime;
        }
        if (velocity.x < 0 && myCollisions.left)
        {
            velocity.x = 0;
        }
        if (Input.GetKey(KeyCode.D))
        {
            velocity.x += 1 * Time.deltaTime;
        }
        if (velocity.x > 0 && myCollisions.right)
        {
            velocity.x = 0;
        }
        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            velocity.x = Mathf.Lerp(velocity.x, 0, Time.deltaTime * 8f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            velocity.y -= 1 * Time.deltaTime;
        }
        if (velocity.y < 0 && myCollisions.down)
        {
            velocity.y = 0;
        }
        if (Input.GetKey(KeyCode.W))
        {
            velocity.y += 1 * Time.deltaTime;
        }
        if (velocity.y > 0 && myCollisions.up)
        {
            velocity.y = 0;
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            velocity.y = Mathf.Lerp(velocity.y, 0, Time.deltaTime * 8f);
        }
        velocity.x = Mathf.Clamp(velocity.x, -maxVelocity.x * Time.deltaTime, maxVelocity.x * Time.deltaTime);
        velocity.y = Mathf.Clamp(velocity.y, -maxVelocity.y * Time.deltaTime, maxVelocity.y * Time.deltaTime);
    }
}
