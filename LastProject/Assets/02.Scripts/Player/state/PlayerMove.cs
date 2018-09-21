using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : State
{
    const float moveSpeedWeight = 10f;

    public PlayerMove(Transform transformComponent, float speed, Rigidbody rigidbodyComponent, Animator animatorComponent, bool isRunning, bool isInHome, float VerticalAxis, float HorizontalAxis)
    {
        this.transformComponent = transformComponent;
        this.rigidbodyComponent = rigidbodyComponent;
        this.animatorComponent = animatorComponent;
        this.isRunning = isRunning;
        this.isInHome = isInHome;
        this.status.MovingSpeed = speed;
        this.VerticalAxis = VerticalAxis;
        this.HorizontalAxis = HorizontalAxis;
    }
    
    public override void SetAnimation()
    {
        float traslation = VerticalAxis * status.MovingSpeed;
        float rotation = HorizontalAxis * status.MovingSpeed;
        Vector3 v = new Vector3(rotation, 0, traslation);
        Debug.Log(v);
        Quaternion q = Quaternion.LookRotation(v);
        rigidbodyComponent.velocity = v;
        //while (transformComponent.rotation != q)
        //{
        //    transformComponent.rotation = Quaternion.Slerp(transformComponent.rotation, q, TimerManager.Instance.DeltaTime * moveSpeedWeight);
        //}
        transformComponent.rotation = q;
        animatorComponent.SetBool("IsRunnig", isRunning);
        animatorComponent.SetBool("InHome", isInHome);
    }
}
