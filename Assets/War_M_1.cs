using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class War_M_1 : MonoBehaviour
{
    public Transform turretTransform; // Assign this in the Inspector to the turret part of your tank
   
    public float rotationSpeed;
    void Update()
    {
        // Ensure turretTransform and playerTransform are assigned properly in the Inspector
        if (turretTransform != null || LevelManager.instance.LiveBall != null)
        {
            // Calculate direction from turret to player
            Vector3 targetDirection = LevelManager.instance.LiveBall.transform.GetChild(0).transform.position - turretTransform.position;
            targetDirection.y = 0f; // Ensure turret only rotates around the Y-axis

            // Rotate turret towards the player
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            turretTransform.rotation = Quaternion.RotateTowards(turretTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

      
    }
}
