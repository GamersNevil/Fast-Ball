using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPostion : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 MyPos;
    public Vector3 MyRot;
    public Transform MyTransform;
    void Start()
    {
        MyPos = transform.position;
       
        MyTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
