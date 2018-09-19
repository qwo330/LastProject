using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    GameObject Player;
    [SerializeField, Range(-10, 10)]
    float horizontalDistance;
    [SerializeField, Range(-10, 10)]
    float verticalDistance;

    void Update()
    {
        Vector3 up = new Vector3(0, Player.transform.position.y, 0).normalized;
        Vector3 forward = new Vector3(Player.transform.position.x, 0, 0).normalized;
        transform.position = FollowCamera(Player.transform.position, up, forward, horizontalDistance, verticalDistance);
        transform.rotation = Quaternion.LookRotation(Player.transform.position);
    }

    Vector3 FollowCamera(Vector3 targetPos, Vector3 targetUp, Vector3 targetForward, float horizontalDistance, float verticalDistance)
    {
        Vector3 eye = targetPos - (targetForward * horizontalDistance) + (targetUp * verticalDistance);

        return eye;
    }
}
