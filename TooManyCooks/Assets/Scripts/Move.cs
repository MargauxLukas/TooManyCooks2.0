using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{
    private Vector3 touchPos;
    [HideInInspector] public bool isTouched;
    public bool justDropped = false;

    public bool cuttingGame = false;
    public bool cookingGame = false;

    public int touchCount = 0;

    public float startPos;
    public float endPos;
    public float swipeDifference;

    [HideInInspector] public Camera cam;

    public GameObject upBar;
    public GameObject downBar;

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
                        justDropped = true;
                        isTouched = false;
                        transform.position = new Vector3(touchPos.x, touchPos.y, -0.75f);
                    }
                }
            }
            else
            {
                isTouched = false;
            }
            //Debug.Log("isTouched : " + isTouched);
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
                        float time = Camera.main.GetComponent<AudioController>().time;
                        Debug.Log(touchCount);
                        touchCount++;

                        Material test = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0];

                        if (touchCount == 1)
                        {
                            test.SetFloat("Vector1_1DD39ACC", 1.4f);
                        }
                        else if (touchCount == 2)
                        {
                            test.SetFloat("Vector1_1DD39ACC", 1.2f);
                        }
                        else if (touchCount == 3)
                        {
                            test.SetFloat("Vector1_1DD39ACC", 1f);
                        }
                        else if (touchCount == 4)
                        {
                            test.SetFloat("Vector1_1DD39ACC", 0.8f);
                        }
                        else if (touchCount == 5)
                        {
                            test.SetFloat("Vector1_1DD39ACC", 0.6f);
                        }
                        else if (touchCount == 6)
                        {
                            gameObject.GetComponent<Animator>().SetBool("Cut", true);
                            cuttingGame = false;
                        }

                        if (Camera.main.GetComponent<AudioController>().tempo60)
                        {
                            if (time > 0.80f || time < 0.10f)
                            {
                                Debug.Log("En rythme !");
                            }
                            else
                            {
                                Debug.Log("T'es nul");
                            }
                        }
                        else if (Camera.main.GetComponent<AudioController>().tempo90)
                        {
                            if (time > 0.50f || time < 0.15f)
                            {
                                Debug.Log("En rythme !");
                            }
                            else
                            {
                                Debug.Log("T'es nul");
                            }
                        }
                        else if (Camera.main.GetComponent<AudioController>().tempo120)
                        {
                            if (time > 0.40f || time < 0.10f)
                            {
                                Debug.Log("En rythme !");
                            }
                            else
                            {
                                Debug.Log("T'es nul");
                            }
                        }

                        Debug.Log(time);
                    }
                }
            }
        }
        else if(cookingGame)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                touchPos = cam.ScreenToWorldPoint(touch.position);
                touchPos.z = 0;

                if (touch.phase == TouchPhase.Began)
                {
                    /*if (touchPos.x > 3 && touchPos.x < 11f && touchPos.y < 5 && touchPos.y > -2)
                    {*/
                        startPos = touchPos.x;
                    //}
                }

                if(touch.phase == TouchPhase.Ended)
                {
                    /*if (touchPos.x > 3 && touchPos.x < 11f && touchPos.y < 5 && touchPos.y > -2)
                    {*/
                        endPos = touchPos.x;
                        Swipe();
                    //}
                }
            }

            if(upBar.transform.GetChild(1).GetComponent<Image>().fillAmount == 1 && downBar.transform.GetChild(1).GetComponent<Image>().fillAmount == 1)
            {
                //MiniGameFini
                upBar.transform.parent.gameObject.SetActive(false);
                cookingGame = false;
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
            //Debug.Log(hit.collider);
        }
    }

    void Swipe()
    {
        swipeDifference = Mathf.Abs(startPos - endPos);
        Vector3 saveValue;
        if (startPos > endPos && swipeDifference > 3f)  //Droite
        {
            OnRythm();
        }
        else if(startPos < endPos && swipeDifference > 3f)  //Gauche
        {
            OnRythm();
        }
        else
        {
            return;
        }

        saveValue = upBar.transform.position;
        upBar.transform.position = downBar.transform.position;
        downBar.transform.position = saveValue;
    }

    void OnRythm()
    {
        float time = Camera.main.GetComponent<AudioController>().time;

        if (Camera.main.GetComponent<AudioController>().tempo60)
        {
            if (time > 0.80f || time < 0.10f)
            {
                Debug.Log("En rythme !");
            }
            else
            {
                Debug.Log("T'es nul");
            }
        }
        else if (Camera.main.GetComponent<AudioController>().tempo90)
        {
            if (time > 0.50f || time < 0.15f)
            {
                Debug.Log("En rythme !");
            }
            else
            {
                Debug.Log("T'es nul");
            }
        }
        else if (Camera.main.GetComponent<AudioController>().tempo120)
        {
            if (time > 0.40f || time < 0.10f)
            {
                Debug.Log("En rythme !");
            }
            else
            {
                Debug.Log("T'es nul");
            }
        }

        Debug.Log(time);
    }
}
