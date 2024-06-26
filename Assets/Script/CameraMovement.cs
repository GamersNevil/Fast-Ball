using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class CameraMovement : Singleton<CameraMovement>
{
    Vector3 LastPos;
    Transform target;
    PlayerMovement player;

    [Space][SerializeField] float distance = 5.0f;
    [SerializeField] float height = 2.0f;
    [SerializeField] Vector3 _lookAtOffset;

    [Space][SerializeField] float minval;
    [SerializeField] float divideval;

    void Start()
    {
        target = PlayerMovement.Instance.transform;
        player = target.GetComponent<PlayerMovement>();
        LastPos = target.position;
    }

    void Update()
    {
        if (true)
        {
            Vector3 dis = player.Ball_Rb.velocity.normalized;

            float z = Mathf.Abs(dis.z);
            float x = Mathf.Abs(dis.x);

            if (z - x >= minval) dis.x *= divideval;
            else if (x - z >= minval) dis.z *= divideval;

            float LearpSpeed = Vector3.Distance(LastPos, target.position);

            Vector3 desiredPosition = target.position - (dis * distance) + (Vector3.up * height);

            Vector3 lerpedPosition = Vector3.Lerp(transform.position, desiredPosition, LearpSpeed);
            Vector3 direction = lerpedPosition - target.position;

            direction.y = 0;
            transform.position = target.position + direction.normalized * distance + Vector3.up * height;

            Quaternion lookAt = Quaternion.LookRotation(target.position - transform.position);
            Quaternion correction = Quaternion.Euler(_lookAtOffset);
            transform.rotation = lookAt * correction;

            LastPos = target.position;
        }
    }

    #region rr

    //[SerializeField] Transform Player, Camera;
    //[SerializeField] float distance, height;
    //[SerializeField] float Angle;

    //[SerializeField]
    //float
    //    adjacent,
    //    oppositeSide;

    //Vector3 movingDirection => Player.GetComponent<PlayerMovement>().rb.velocity;
    //Vector3 CameraLookingDirection => Player.position - Camera.position;
    //Vector3 perpendiculer => new Vector3(-movingDirection.z, movingDirection.y, movingDirection.x);
    //float angle => Vector3.SignedAngle(movingDirection, CameraLookingDirection, Vector3.up);

    //int multiplier
    //{
    //    get
    //    {
    //        int n = 0;
    //        if (angle > 0) n = 1;
    //        else if (angle < 0) n = -1;
    //        return n;
    //    }
    //}

    //public Vector3 meetingPoint, dir, pos;

    //void Update()
    //{
    //    Angle = angle;
    //    if (movingDirection.magnitude > 0)
    //    {
    //        if (Mathf.Abs(angle) > 0)
    //        {
    //            meetingPoint = CalculateMeetingPoint(Player.position, movingDirection.normalized, Camera.position, perpendiculer.normalized);
    //            adjacent = Vector3.Distance(meetingPoint, Player.position);
    //            adjacent = adjacent > distance ? distance : adjacent;
    //            oppositeSide = Mathf.Sqrt(distance * distance - adjacent * adjacent);

    //            dir = meetingPoint - Player.position;
    //            pos = Player.position + dir.normalized * adjacent + perpendiculer.normalized * oppositeSide * multiplier;
    //            Camera.position = pos + Vector3.up * height;

    //            Debug.DrawLine(Player.position, meetingPoint, Color.red);
    //            Debug.DrawLine(Camera.position, meetingPoint, Color.blue);
    //            Debug.DrawRay(meetingPoint, Vector3.up, Color.green);
    //        }
    //        else
    //        {
    //            Camera.position = Player.position - movingDirection.normalized * distance + Vector3.up * height;
    //        }
    //    }

    //    Quaternion lookAt = Quaternion.LookRotation(Player.position - transform.position);
    //    Quaternion correction = Quaternion.Euler(_lookAtOffset);
    //    transform.rotation = lookAt * correction;
    //}

    //Vector3 CalculateMeetingPoint(Vector3 start1, Vector3 dir1, Vector3 start2, Vector3 dir2)
    //{
    //    Vector3 cross1 = Vector3.Cross(dir1, dir2);
    //    Vector3 cross2 = Vector3.Cross(start2 - start1, dir2);
    //    float t = Vector3.Dot(cross2, cross1) / cross1.sqrMagnitude;
    //    return start1 + dir1 * t;
    //}

    #endregion
}