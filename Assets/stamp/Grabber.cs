using UnityEngine;

public class Grabber : MonoBehaviour
{

    public LayerMask mask;

    private GameObject selected;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            selected = null;
        }

        //Camera.main.farClipPlane
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Cast();

            if (hit.collider != null)
            {
                selected = hit.collider.gameObject;
            }
        }

        if (selected != null)
        {
            //Debug.Log(Input.mousePosition);
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selected.transform.position = new Vector2(v.x, v.y);
        }

    }

    private RaycastHit2D Cast()
    {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0, mask);

        Debug.Log(hit.collider.gameObject.tag);

        return hit;
    }
}
