using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

    const float RayCastMaxDistance = 100.0f;
    TempInput tempInput;
       
	void Start () {
        tempInput = FindObjectOfType<TempInput>();
	}
	
	void FixedUpdate () {
        Walking();
    }

    private void Walking()
    {
        if (tempInput.Clicked())
        {
            Vector2 clickPos = tempInput.GetCursorPosition();
            Ray ray = Camera.main.ScreenPointToRay(clickPos);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, RayCastMaxDistance, 1 << LayerMask.NameToLayer("Ground")))
            {
                SendMessage("SetDestination", hitInfo.point);
            }
        }
    }
}
