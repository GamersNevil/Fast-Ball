using Cinemachine;
using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : Singleton<PlayerMovement>
{
    [Space] public GameObject CurrentBall;
    public Transform CameraCinemachine;
    public Transform _cam;

    [Space] public Rigidbody Ball_Rb;
    [SerializeField] public Local_Data Local_Data;
    [SerializeField] public Transform Camera_Transform, Ball_Clone_Transform;

    [SerializeField] float Push_Force;

    [Space] public float Applied_Push_Force = 0;
    public Vector3 Camera_No_Y_Pos = new Vector3(0, 0, 0);
    public Vector3 Ball_No_Y_Pos = new Vector3(0, 0, 0);
    public Vector3 Normal_Direction = new Vector3(0, 0, 0);
    Vector3 Force_Direction = new Vector3(0, 0, 0);

    [Space][SerializeField] bool IsActivate;
    [SerializeField] Vector3 offset;

    [Space] public bool Isgrounded;

    [Space][SerializeField] Transform _camParent;
    Camera camera;

    void Start()
    {
        camera = Camera.main;
        Camera_Transform= camera.transform;
        _cam = camera.transform;
        CameraCinemachine = GameObject.Find("[--Virtual Camera--]").transform;
        Application.targetFrameRate = 300;
        Ball_Rb = GetComponent<Rigidbody>();
        Ball_Rb.maxAngularVelocity = Mathf.Infinity;
    }

    void FixedUpdate()
    {
        Ball_Clone_Transform.position = transform.position;

        if (Local_Data.Relative_Angle != 0)
        {
            if (Local_Data.Relative_Angle > -30 && Local_Data.Relative_Angle < 30) Applied_Push_Force = Push_Force * 2;
            else Applied_Push_Force = Push_Force;
            Camera_No_Y_Pos = new Vector3(Camera_Transform.position.x, 0, Camera_Transform.position.z);
            Ball_No_Y_Pos = new Vector3(Ball_Clone_Transform.position.x, 0, Ball_Clone_Transform.position.z);
            Normal_Direction = Camera_No_Y_Pos - Ball_No_Y_Pos;
            Normal_Direction = Normal_Direction.normalized;
            Force_Direction = Quaternion.AngleAxis(Local_Data.Relative_Angle, -Ball_Clone_Transform.up) * Normal_Direction * 2;
            Local_Data.Relative_Angle = 0;
            Ball_Rb.AddForce(Force_Direction * Applied_Push_Force, ForceMode.Force);
        }

        if (IsActivate)
        {
            Quaternion rotation = Quaternion.Euler(0, Time.time * 20, 0);
            Vector3 targetPosition = transform.position + rotation * offset;
            _cam.position = Vector3.Lerp(_cam.position, targetPosition, Time.deltaTime);
            _cam.LookAt(transform.position);
        }

        if (IsBirdMode) Ball_Clone_Transform.eulerAngles += new Vector3(0, Time.deltaTime * 80f, 0);
    }

    bool IsBirdMode;


    void StopballOnWater() => Ball_Rb.isKinematic = true;


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="over")
        {
            UIManager.instance.GameOverScreen();
        }
        else if (collision.gameObject.tag == "flore")
        {

         // LevelManager.instance.cinemachineVirtual.Follow = transform;
         //   LevelManager.instance.cinemachineVirtual.LookAt =transform;
        }
    }
    
    void OnCollisionStay(Collision collision)
    {
        Isgrounded = true;
        if (collision.transform.tag == "Mover")
            if (transform.parent != collision.transform) transform.SetParent(collision.transform);
    }

    void OnCollisionExit(Collision collision)
    {
        Invoke(nameof(checkgrounded), 0.5f);
        if (collision.transform.tag == "Mover") transform.SetParent(null);
    }

    void checkgrounded() => Isgrounded = false;

    public void BirdModeOn()
    {
        CameraCinemachine.GetComponent<CinemachineVirtualCamera>().enabled = false;
        CameraCinemachine.SetParent(Ball_Clone_Transform);
        _cam.SetParent(Ball_Clone_Transform);
        IsBirdMode = true;
    }

    public void BirdModeOff()
    {
        IsBirdMode = false;
        _cam.SetParent(_camParent);
        CameraCinemachine.SetParent(_camParent);
        CameraCinemachine.position = _cam.position;
        CameraCinemachine.GetComponent<CinemachineVirtualCamera>().enabled = true;
        Ball_Clone_Transform.eulerAngles = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "win")
        {
            
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            Invoke("WINS", 2);
        }
    }
    public void WINS()
    {
        UIManager.instance.LevelClear();
    }
     
}