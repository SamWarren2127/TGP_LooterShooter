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
    [SerializeField]
    GameObject abilityMenu;
    [SerializeField]
    GameObject deathMenu;

    void Update()
    {
        if(!pauseMenu.activeSelf && !abilityMenu.activeSelf && !deathMenu.activeSelf)
        {
            var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);

            mouseLook += smoothV;
            mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        }
    }

    public void LowerCamera(float _crouchDiff)
    {
        transform.Translate(Vector3.down * _crouchDiff);
    }

    public void RaiseCamera(float _crouchDiff)
    {
        transform.Translate(Vector3.up * _crouchDiff);
    }
}
