using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector3 touchPos;
    [HideInInspector] public bool isTouched;
    
    [HideInInspector] public Camera cam;

    void Start()
    {
        cam = CameraInstance.instance.gameObject.GetComponent<Camera>();
    }
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchPos = cam.ScreenToWorldPoint(touch.position);
            touchPos.z = 0;

            TouchObject();

            if (isTouched)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    transform.position = new Vector3(touchPos.x, touchPos.y, -1.40f);

                    if(transform.parent != null)
                    {
                        if (transform.parent.GetComponent<TableSlot>())
                        {
                            transform.parent.GetComponent<TableSlot>().occupied = false;
                            transform.parent.GetComponent<TableSlot>().ingredient = null;
                            transform.parent = null;
                        }
                    }

                }

                if (touch.phase == TouchPhase.Ended)
                {
                    isTouched = false;
                    transform.position = new Vector3(touchPos.x, touchPos.y, -0.75f);
                }
            }
        }
        else
        {
            isTouched = false;
        }
        Debug.Log("isTouched : " + isTouched);
    }

    void TouchObject()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.ScreenToWorldPoint(Input.GetTouch(0).position), Vector3.forward, out hit))
            {
                var obj = hit.collider;

                if (obj != null && obj.gameObject != null && gameObject != null && obj.gameObject == gameObject && obj.gameObject.name == gameObject.name)
                {
                    isTouched = true;
                }
            }
            Debug.Log(hit.collider);
        }
    }
}
