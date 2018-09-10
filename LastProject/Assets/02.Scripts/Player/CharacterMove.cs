using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{

    const float GravityPower = 9.8f;
    const float StoppingDistance = 0.6f; // 목적지에 도착했다고 보는 정지 거리?????
    private Vector3 velocity = Vector3.zero;
    private CharacterController characterController; // 캐릭터 컨트롤러의 캐시
    public bool Arrived = false; // 도착했는가 안했는가
    private bool forceRotate = false; //방향을 강제로 지시하는가
    private Vector3 forceRotateDirection; //강제로 향하고 싶은 방향
    public Vector3 Destination; // 목적지(외부에서 인풋을 받아와야 하는게 아닐까?)
    public float WalkSpeed = 6.0f;
    public float RotationSpeed = 360.0f;

    //private Quaternion characterTargetRotation;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Destination = transform.position;
    }

    void FixedUpdate()
    {
        if (characterController.isGrounded)
        {
            Vector3 destinationXZ = Destination;
            destinationXZ.y = transform.position.y;

            Vector3 direction = (destinationXZ - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, destinationXZ);

            Vector3 currentVelocity = velocity; //현재 속도를 보관한다.

            if (Arrived || distance < StoppingDistance)
            {
                Arrived = true;
            }
            if (Arrived)
            {
                velocity = Vector3.zero;
            }
            else
            {
                velocity = direction * WalkSpeed;
            }

            //보간 처리
            velocity = Vector3.Lerp(currentVelocity, velocity, Mathf.Min(Time.deltaTime * 5.0f, 1.0f));
            velocity.y = 0;

            if (!forceRotate)
            {
                if (velocity.magnitude > 0.1f && !Arrived) //velocity.magnitde : 속력의 길이를 리턴해준다.
                {
                    Quaternion characterTargetRotation = Quaternion.LookRotation(direction); //LookRotation : 정확한 위와 앞방향을 이용해 로테이션을 만들어준다.
                    //RotateTowards : 앞방향으로부터 회전을 해준다.
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, characterTargetRotation, RotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                Quaternion characterTargetRotation = Quaternion.LookRotation(forceRotateDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, characterTargetRotation, RotationSpeed * Time.deltaTime);
            }

        }
        velocity += Vector3.down * GravityPower * Time.deltaTime;

        //땅에 닿아 있다면 지면을 꽉 누른다.
        Vector3 snapGround = Vector3.zero;
        if (characterController.isGrounded)
        {
            snapGround = Vector3.down;
        }

        //Character Controller를 사용해서 움직인다.
        characterController.Move(velocity * Time.deltaTime + snapGround);
        if (characterController.velocity.magnitude < 0.1f)
        {
            Arrived = true;
        }

        if (forceRotate && Vector3.Dot(transform.forward, forceRotateDirection) > 0.99f)
        {
            forceRotate = false;
        }
    }

    //목적지를 설정한다.
    public void SetDestination(Vector3 destination)
    {
        Arrived = false;
        this.Destination = destination;
    }

    //지정한 방향으로 향한다.
    public void SetDirection(Vector3 direction)
    {
        forceRotateDirection = direction;
        forceRotateDirection.y = 0;
        forceRotateDirection.Normalize();
        forceRotate = true;
    }

    //이동을 그만둔다.
    public void StopMove()
    {
        Destination = transform.position;
    }

    //목적지에 도착했는지 조사한다.
    public bool PlayerArrived()
    {
        return Arrived;
    }

}

