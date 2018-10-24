using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singleton<InputManager>
{
    public float INPUT_MinDragDistance;
    public float VerticalAxis;
    public float HorizontalAxis;
    public bool IsUIRaycast; //NGUI에서 ui 가르키는지 여부
    public bool IsPointOverUIObject; //UGUI에서 ui 가르키는지 여부
    public bool IsMouseClicked;

    const float CheckDragRate = 10f;
    Touch touch;
    Vector3 touchPos;
    Vector2 touchBeganPos;
    Vector2 touchMovedPos;
    Vector2 touchVector;

    public void Init()
    {
        INPUT_MinDragDistance = Screen.height / CheckDragRate;
    }

    private void Update()
    {
#if UNITY_STANDALONE_WIN

        VerticalAxis = Input.GetAxis("Vertical");
        HorizontalAxis = Input.GetAxis("Horizontal");
        IsUIRaycast = UICamera.Raycast(Input.mousePosition);
        IsPointOverUIObject = EventSystem.current.IsPointerOverGameObject();
        IsMouseClicked = Input.GetMouseButtonDown(0);

#elif UNITY_ANDROID || UNITY_IPHONE

        if (Input.touchCount > 0)
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
                VerticalAxis = GetInputRange(touchVector.y);
                HorizontalAxis = GetInputRange(touchVector.x);

                Debug.Log(VerticalAxis + ", " + HorizontalAxis);
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
        float result = value / INPUT_MinDragDistance;

        if (result < -1)
        {
            return -1;
        }
        else if(result > 1)
        {
            return 1;
        }
        else
        {
            return result;
        }
    }
}