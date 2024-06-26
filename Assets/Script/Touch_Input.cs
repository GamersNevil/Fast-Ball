using UnityEngine;
using UnityEngine.EventSystems;

public class Touch_Input : MonoBehaviour
{
    [SerializeField] Local_Data Local_Data;

    Touch touch;

    Vector2 touch_Start_Position = new Vector2(0, 0);
    Vector2 touch_End_Position = new Vector2(0, 0);
    Vector2 touch_Normal_Position = new Vector2(0, 0);

    Vector2 Touch_Start_To_Normal_Direction = new Vector2(0, 0);
    Vector2 Touch_Start_To_End_Direction = new Vector2(0, 0);

    bool bool_isTouched = false;
    bool Touchphase_hasBegan = false;
    bool Touchphase_hasEnded = false;

    float Angle_Between_Normal_And_Touch_Direction = 0;

    void Update()
    {
        F_Detect_Touch();
        F_Detect_Touch_Phase();
        F_Save_Touch_Begin_Position();
        F_Save_Touch_End_Position();
        F_Find_Angle();
    }

    void F_Detect_Touch()
    {
        if (Input.touchCount > 0)
        {
            bool_isTouched = true;
            touch = Input.GetTouch(0);
        }
        else bool_isTouched = false;
    }

    void F_Detect_Touch_Phase()
    {
        if (bool_isTouched)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Touchphase_hasBegan = true;
                Touchphase_hasEnded = false;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                Touchphase_hasBegan = false;
                Touchphase_hasEnded = true;
            }
            else
            {
                Touchphase_hasBegan = false;
                Touchphase_hasEnded = false;
            }
        }
        else
        {
            Touchphase_hasBegan = false;
            Touchphase_hasEnded = false;
        }
    }

    void F_Save_Touch_Begin_Position()
    {
        if (Touchphase_hasBegan) touch_Start_Position = touch.position;
    }

    void F_Save_Touch_End_Position()
    {
        if (Touchphase_hasEnded) touch_End_Position = touch.position;
    }

    bool Isdown;

    void F_Find_Angle()
    {
        if ( EventSystem.current.currentSelectedGameObject != null) return;

        if (Input.GetMouseButtonDown(0))
        {
            Isdown = true;
            touch_Start_Position = Input.mousePosition;
        }
        if (Input.GetMouseButton(0) && Isdown)
        {
            touch_End_Position = Input.mousePosition;
            touch_Normal_Position = new Vector2(touch_Start_Position.x, 0);
            Touch_Start_To_Normal_Direction = touch_Normal_Position - touch_Start_Position;
            Touch_Start_To_End_Direction = touch_End_Position - touch_Start_Position;
            Angle_Between_Normal_And_Touch_Direction = Vector2.SignedAngle(Touch_Start_To_Normal_Direction, Touch_Start_To_End_Direction);
            Local_Data.Relative_Angle = Angle_Between_Normal_And_Touch_Direction;
            touch_Start_Position = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            PlayerMovement.Instance.Ball_Rb.velocity *= 0.75f;
            Isdown = false;
        }

        if (Touchphase_hasEnded)
        {
            touch_Normal_Position = new Vector2(touch_Start_Position.x, 0);
            Touch_Start_To_Normal_Direction = touch_Normal_Position - touch_Start_Position;
            Touch_Start_To_End_Direction = touch_End_Position - touch_Start_Position;
            Angle_Between_Normal_And_Touch_Direction = Vector2.SignedAngle(Touch_Start_To_Normal_Direction, Touch_Start_To_End_Direction);
            Local_Data.Relative_Angle = Angle_Between_Normal_And_Touch_Direction;
        }
    }
}