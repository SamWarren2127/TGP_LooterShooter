using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    float sensitivity;
    [SerializeField]
    float smoothing;
    [SerializeField]
    GameObject character;
    Vector2 mouseLook;
    Vector2 smoothV;
    [SerializeField]
    GameObject pauseMenu;

    void Update()
    {
        if(!pauseMenu.activeSelf)
        {
            var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);

            mouseLook += smoothV;
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        }
    }

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
