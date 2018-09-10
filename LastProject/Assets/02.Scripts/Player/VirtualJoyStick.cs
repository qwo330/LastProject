using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgImg;
    private Image joyStickImg;
    public Vector3 InputDirection { get; set; }

    private void Start()
    {
        bgImg = GetComponent<Image>();
        joyStickImg = transform.GetChild(0).GetComponent<Image>();
        InputDirection = Vector3.zero;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            InputDirection = new Vector3(pos.x*2 , 0, pos.y*2 );
            InputDirection = (InputDirection.magnitude > 1.0f) ? InputDirection.normalized : InputDirection;

            //조이스틱을 움직인다.
            joyStickImg.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (bgImg.rectTransform.sizeDelta.x / 3)
                , InputDirection.z * (bgImg.rectTransform.sizeDelta.y / 3));

            Debug.Log(InputDirection);
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputDirection = Vector3.zero;
        joyStickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    //public float Horizontal()
    //{
    //    if (inputVector.x != 0)
    //    {
    //        return inputVector.x;
    //    }
    //    else
    //        return Input.GetAxis("Horizontal");
    //}
    //public float Vertical()
    //{
    //    if (inputVector.z != 0)
    //    {
    //        return inputVector.z;
    //    }
    //    else
    //        return Input.GetAxis("Vertical");
    //}
}
