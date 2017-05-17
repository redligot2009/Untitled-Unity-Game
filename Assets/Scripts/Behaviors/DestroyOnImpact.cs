using UnityEngine;
using System.Collections;

public class DestroyOnImpact : MonoBehaviour {

    public LayerMask wall;
    LinearTravel mover;
    BoxCollider2D myCollider;

    void Start()
    {
        mover = GetComponent<LinearTravel>();
        myCollider = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mover.transform.up, myCollider.bounds.extents.x, wall);

        if (hit)
        {
            Kill();
        }
	}

    public virtual void Kill()
    {
        Destroy(gameObject);
    }
}
