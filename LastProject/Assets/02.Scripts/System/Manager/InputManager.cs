using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singleton<InputManager>
{
    public static float VerticalAxis;
    public static float HorizontalAxis;
    public static bool IsUIRaycast; //NGUI에서 ui 가르키는지 여부
    public static bool IsPointOverUIObject; //UGUI에서 ui 가르키는지 여부
    public static bool IsMouseClicked;

    Touch touch;
    Vector3 touchPos;
    Vector2 touchBeganPos;
    Vector2 touchMovedPos;
    Vector2 touchVector;

    public void Init()
    {

    }

    private void Update()
    {
#if UNITY_STANDALONE

        //VerticalAxis = Input.GetAxis("Vertical");
        //HorizontalAxis = Input.GetAxis("Horizontal");
        IsUIRaycast = UICamera.Raycast(Input.mousePosition);
        IsPointOverUIObject = EventSystem.current.IsPointerOverGameObject();
        IsMouseClicked = Input.GetMouseButtonDown(0);
        //if (!EventSystem.current.IsPointerOverGameObject())
        //if (UICamera.Raycast(Input.mousePosition))

//#elif UNITY_ANDROID

        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                touchBeganPos = touch.position;
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                touchMovedPos = touch.position;
                touchVector = touchMovedPos - touchBeganPos;
                VerticalAxis = GetInputRange(touchVector.x);
                HorizontalAxis = GetInputRange(touchVector.y);

                Debug.Log(touchVector);
            }
            else
            {
                VerticalAxis = 0;
                HorizontalAxis = 0;
            }
            touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            IsUIRaycast = UICamera.Raycast(touchPos);
            IsPointOverUIObject = EventSystem.current.IsPointerOverGameObject();
        }

#endif
    }

    float GetInputRange(float value)
    {
        if(value < -1)
        {
            return -1;
        }
        else if(value > 1)
        {
            return 1;
        }
        else
        {
            return value;
        }
    }


}