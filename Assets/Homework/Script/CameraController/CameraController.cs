using Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera Camera;
    public Transform CameraTarget;

    public Vector3 offset = new Vector3(0f, 2f, -5f);  // Camera offset from the player
    public float rotationSpeed = 5f;        // Speed of camera rotation

    private void LateUpdate()
    {
        if (CameraTarget == null)
        {
            Debug.LogWarning("Camera target transform not assigned to the camera controller!");
            return;
        }

        //// Calculate desired camera position based on player's position and offset
        Vector3 desiredPosition = CameraTarget.position + offset;

        //// Smoothly move the camera towards the desired position
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, desiredPosition, Time.deltaTime * rotationSpeed);

        //// Rotate the camera to match the player's forward direction
        //Quaternion targetRotation = Quaternion.LookRotation(CameraTarget.forward);
        //Camera.transform.rotation = Quaternion.Slerp(Camera.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        Camera.transform.LookAt(CameraTarget);
    }
}
