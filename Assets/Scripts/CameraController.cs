using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float crouchDifference = 0.5f;

    public void RotateCamera(float rotation)
    {
        // Rotates the camera on the X axis to look up and down following the mouse
        rotation = Mathf.Clamp(rotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
    }

    public void LowerCamera()
    {
        transform.Translate(Vector3.down * crouchDifference);
    }

    public void RaiseCamera()
    {
        transform.Translate(Vector3.up * crouchDifference);
    }
}
