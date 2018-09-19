using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    Rigidbody rigidbodyComponent;
    [SerializeField, Range(0,100)]
    float speed;



    private void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            float traslation = Input.GetAxis("Vertical") * speed;
            float rotation = Input.GetAxis("Horizontal") * speed;
            Vector3 v = new Vector3(rotation, 0, traslation);
            Quaternion q = Quaternion.LookRotation(v);
            rigidbodyComponent.velocity = v;
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10);
        }
    }
}
