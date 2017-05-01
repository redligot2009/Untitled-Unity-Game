using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class AutoResizeBoxCollider : MonoBehaviour
{
    BoxCollider2D boxCol;
    Transform[] allDescendants;

    public void addBoundsToAllChildren()
    {
        if (boxCol == null)
        {
            boxCol = gameObject.GetComponent(typeof(BoxCollider2D)) as BoxCollider2D;
            if (boxCol == null)
            {
                boxCol = gameObject.AddComponent<BoxCollider2D>();
            }
        }
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        //Renderer thisRenderer = transform.GetComponent<Renderer>();
       // bounds.Encapsulate(thisRenderer.bounds);
        boxCol.offset = bounds.center - transform.position;
        boxCol.size = bounds.size;

        allDescendants = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform desc in allDescendants)
        {
            BoxCollider2D childRenderer = desc.GetComponent<BoxCollider2D>();
            if (childRenderer != null)
            {
                bounds.Encapsulate(childRenderer.bounds);
            }
            boxCol.offset = bounds.center - transform.position;
            boxCol.size = bounds.size;
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        addBoundsToAllChildren();
    }
}
