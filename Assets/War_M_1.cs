using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War_M_1 : MonoBehaviour
{
    public Transform turretTransform; 
    public Transform Barreltransform; 
   
    public float rotationSpeed;
    void Update()
    {
        // Ensure turretTransform and playerTransform are assigned properly in the Inspector
        if (turretTransform != null || LevelManager.instance.LiveBall != null)
        {
            // Calculate direction from turret to player
            Vector3 targetDirection = LevelManager.instance.LiveBall.transform.GetChild(0).transform.position - turretTransform.position;
            Vector3 targetDirection2 = LevelManager.instance.LiveBall.transform.GetChild(0).transform.position - turretTransform.position;
            targetDirection.y = 0f; // Ensure turret only rotates around the Y-axis

            // Rotate turret towards the player
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
           
            Quaternion targetRotation2 = Quaternion.LookRotation(targetDirection2);
            targetRotation2.z = 0f;
            turretTransform.rotation = Quaternion.RotateTowards(turretTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            Barreltransform.rotation = Quaternion.RotateTowards(Barreltransform.rotation, targetRotation2, Time.deltaTime * rotationSpeed);
        }

      
    }
}
