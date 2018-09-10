using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCtrl : MonoBehaviour {

    public VirtualJoyStick joystick;
    public float speed = 150.0f;
    private Rigidbody rigid;
    
    void Start() {
        rigid = GetComponent<Rigidbody>();
    }

    void Update() {
        rigid.AddForce(joystick.InputDirection * speed * Time.deltaTime);

    }

     
}
