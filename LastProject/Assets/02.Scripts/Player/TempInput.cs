using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempInput : MonoBehaviour {

    Vector2 slideStartPosition;
    Vector2 prevPosition;
    Vector2 delta = Vector2.zero;
    bool moved = false;

    
	void Update () {
        if (Input.GetButton("Fire1"))
        {
            slideStartPosition = GetCursorPosition();
        }
        if (Input.GetButton("Fire1"))
        {
            if (Vector2.Distance(slideStartPosition, GetCursorPosition()) >= (Screen.width * 0.1f)) moved = true;
        }
        if (Input.GetButton("Fire1") == false && Input.GetButtonUp("Fire1") == false) moved = false;

        //이동량을 구한다
        if (moved) delta = GetCursorPosition() - prevPosition;
        else delta = Vector2.zero;

        prevPosition = GetCursorPosition();
	}

    public bool Clicked()
    {
        if (moved == false && Input.GetButton("Fire1") == true) return true;
        else return false;
    }
    //슬라이드 할때 커서 이동량.
    public Vector2 GetDeltaPosition()
    {
        return delta;
    }

    public bool Moved()
    {
        return moved;
    }

    public Vector2 GetCursorPosition()
    {
        return Input.mousePosition;
    }
}
