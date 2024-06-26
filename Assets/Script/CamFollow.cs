using UnityEngine;

public class CamFollow : Singleton<CamFollow>
{
    [SerializeField] Transform Ball;

    public Transform Cam;
    [SerializeField] float FollowCamOffset_Y = 2, FollowCamOffset_Z = 10;

    [SerializeField] float LookAtOffset_Y = 0;
    public Vector3 Target_Pos, CamFollow_Pos;
    [SerializeField] float FollowSpeed_Inverse = 0.5f, LookAtSpeed = 200f;

    Rigidbody Rb;
    Vector3 velo = Vector3.up;

    void Start()
    {
        Rb = Ball.GetComponent<Rigidbody>();
        Cam = Camera.main.GetComponent<Transform>();
        CamFollow_Pos = new Vector3(Ball.position.x, Ball.position.y + FollowCamOffset_Y, Ball.position.z - FollowCamOffset_Z);
        Cam.position = CamFollow_Pos;
    }

    void FixedUpdate()
    {
        FindDisplacementDir();
    }

    void FindDisplacementDir()
    {
        Vector3 velocity = -(Rb.velocity).normalized;
        CamFollow_Pos = velocity * FollowCamOffset_Z;
        CamFollow_Pos = Ball.position + CamFollow_Pos;
        CamFollow_Pos = new Vector3(CamFollow_Pos.x, CamFollow_Pos.y + FollowCamOffset_Y, CamFollow_Pos.z);

        if (Rb.velocity.z != 0) Cam.position = Vector3.SmoothDamp(Cam.position, CamFollow_Pos, ref velo, FollowSpeed_Inverse, Mathf.Infinity);
        else
        {
            CamFollow_Pos = new Vector3(Ball.position.x, Ball.position.y + FollowCamOffset_Y, Ball.position.z - FollowCamOffset_Z);
            Cam.position = CamFollow_Pos;
        }

        Target_Pos = new Vector3(Ball.position.x, Ball.position.y + LookAtOffset_Y, Ball.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(Target_Pos - Cam.position);
        Cam.rotation = Quaternion.Slerp(Cam.rotation, targetRotation, LookAtSpeed * Time.deltaTime);
    }
}