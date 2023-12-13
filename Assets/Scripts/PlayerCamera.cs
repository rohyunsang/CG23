using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target; // Link to the character's Transform.
    public float distance = 5.0f; // Distance between the character and camera.
    public float height = 2.0f; // Adjust the height of the camera.
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    public float xSpeed = 120.0f; // Mouse rotation speed.
    public float ySpeed = 120.0f;

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if(GameManager.Instance.isPaused)  // Dont Move Camera
            return;

        if (!target)
            return;

        x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
        y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

        // Clamp the vertical angle to prevent the camera from flipping over.
        y = ClampAngle(y, -50, 80);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, height, -distance) + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}