using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어를 바라보는 카메라

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    [SerializeField, Range(-10, 10)]
    float horizontalDistance;
    [SerializeField, Range(-10, 10)]
    float verticalDistance;
    [SerializeField, Range(-20, 20)]
    float heightOffset;

    private void LateUpdate()
    {
        Vector3 offsetPosition = new Vector3(Player.transform.position.x, Player.transform.position.y + heightOffset, Player.transform.position.z);
        transform.position = FollowCamera(offsetPosition, Vector3.up, Vector3.forward, horizontalDistance, verticalDistance);
        transform.LookAt(Player.transform);
    }

    Vector3 FollowCamera(Vector3 targetPos, Vector3 targetUp, Vector3 targetForward, float horizontalDistance, float verticalDistance)
    {
        return targetPos - (targetForward * horizontalDistance) + (targetUp * verticalDistance);
    }
}
