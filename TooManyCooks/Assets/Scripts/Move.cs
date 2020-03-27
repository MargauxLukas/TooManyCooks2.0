﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector3 touchPos;
    [HideInInspector] public bool isTouched;

    public bool cuttingGame = false;
    public bool cookingGame = false;

    public int touchCount = 0;

    [HideInInspector] public Camera cam;

    void Start()
    {
        cam = CameraInstance.instance.gameObject.GetComponent<Camera>();
    }

    void Update()
    {
        if (!cuttingGame && !cookingGame)
        {
            if (Input.touchCount > 0)
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

                        if (transform.parent != null)
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
        else if (cuttingGame)
        {
            //Input.multiTouchEnabled = false;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                touchPos = cam.ScreenToWorldPoint(touch.position);
                touchPos.z = 0;

                if (touch.phase == TouchPhase.Began)
                {
                    if (touchPos.x > -6 && touchPos.x < 2.5f && touchPos.y < 1 && touchPos.y > -5)
                    {
                        Debug.Log(touchCount);
                        touchCount++;

                        Material test = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0];

                        if (touchCount == 1)
                        {
                            test.SetFloat("Vector1_1DD39ACC", 1.4f);
                        }
                        if (touchCount == 2)
                        {
                            test.SetFloat("Vector1_1DD39ACC", 1.2f);
                        }
                        if (touchCount == 3)
                        {
                            test.SetFloat("Vector1_1DD39ACC", 1f);
                        }
                        if (touchCount == 4)
                        {
                            test.SetFloat("Vector1_1DD39ACC", 0.8f);
                        }
                        if (touchCount == 5)
                        {
                            test.SetFloat("Vector1_1DD39ACC", 0.6f);
                        }
                        if (touchCount == 6)
                        {
                            gameObject.GetComponent<Animator>().SetBool("Cut", true);
                            cuttingGame = false;
                        }
                    }
                }
            }
        }
    }

    void TouchObject()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
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
