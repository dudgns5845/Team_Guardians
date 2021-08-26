using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //public bool controllerPauseState = false;
    //public bool enableCameraMovement = true;
    //public enum InvertMouseInput { None, X, Y, Both }
    //public InvertMouseInput mouseInputInversion = InvertMouseInput.None;
    //public enum CameraInputMethod { Traditional, TraditionalWithConstraints, Retro }
    //public CameraInputMethod cameraInputMethod = CameraInputMethod.Traditional;

    //public float verticalRotationRange = 170;
    //public float mouseSensitivity = 10;
    //public float fOVToMouseSensitivity = 1;
    //public float cameraSmoothing = 5f;
    //public bool lockAndHideCursor = false;
    //public Camera playerCamera;
    //public bool enableCameraShake = false;
    //internal Vector3 cameraStartingPosition;
    //float baseCamFOV;


    //public bool autoCrosshair = false;
    //public bool drawStaminaMeter = true;
    //float smoothRef;
    ////Image StaminaMeter;
    ////Image StaminaMeterBG;
    //public Sprite Crosshair;
    //public Vector3 targetAngles;
    //private Vector3 followAngles;
    //private Vector3 followVelocity;
    //private Vector3 originalRotation;
    public Camera mainCamera;
    Vector3 cameraPos;

    //[SerializeField] [Range(1f, 1f)] float shakeRange = 1f;
    //[SerializeField] [Range(1f, 1f)] float duration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //cameraPos = mainCamera.transform.position;
        //InvokeRepeating("StarShake", 2f, 1f);
        //Invoke("StarShake", duration);
    }

    private void Update()
    {
        //if (enableCameraMovement && !controllerPauseState)
        //{
        //    float mouseYInput = 0;
        //    float mouseXInput = 0;
        //    float camFOV = playerCamera.fieldOfView;
        //    if (cameraInputMethod == CameraInputMethod.Traditional || cameraInputMethod == CameraInputMethod.TraditionalWithConstraints)
        //    {
        //        mouseYInput = mouseInputInversion == InvertMouseInput.None || mouseInputInversion == InvertMouseInput.X ? Input.GetAxis("Mouse Y") : -Input.GetAxis("Mouse Y");
        //        mouseXInput = mouseInputInversion == InvertMouseInput.None || mouseInputInversion == InvertMouseInput.Y ? Input.GetAxis("Mouse X") : -Input.GetAxis("Mouse X");
        //    }
        //    else
        //    {
        //        mouseXInput = Input.GetAxis("Horizontal") * (mouseInputInversion == InvertMouseInput.None || mouseInputInversion == InvertMouseInput.Y ? 1 : -1);
        //    }
        //    if (targetAngles.y > 180) { targetAngles.y -= 360; followAngles.y -= 360; } else if (targetAngles.y < -180) { targetAngles.y += 360; followAngles.y += 360; }
        //    if (targetAngles.x > 180) { targetAngles.x -= 360; followAngles.x -= 360; } else if (targetAngles.x < -180) { targetAngles.x += 360; followAngles.x += 360; }
        //    targetAngles.y += mouseXInput * (mouseSensitivity - ((baseCamFOV - camFOV) * fOVToMouseSensitivity) / 6f);
        //    if (cameraInputMethod == CameraInputMethod.Traditional) { targetAngles.x += mouseYInput * (mouseSensitivity - ((baseCamFOV - camFOV) * fOVToMouseSensitivity) / 6f); }
        //    else { targetAngles.x = 0f; }
        //    targetAngles.x = Mathf.Clamp(targetAngles.x, -0.5f * verticalRotationRange, 0.5f * verticalRotationRange);
        //    followAngles = Vector3.SmoothDamp(followAngles, targetAngles, ref followVelocity, (cameraSmoothing) / 100);

        //    playerCamera.transform.localRotation = Quaternion.Euler(-followAngles.x + originalRotation.x, 0, 0);
        //    transform.localRotation = Quaternion.Euler(0, followAngles.y + originalRotation.y, 0);
        //    }
        //    void starshake()
        //    {
        //    float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        //    float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        //    Vector3 cameraPos = mainCamera.transform.position;
        //    cameraPos.x += cameraPosX;
        //    cameraPos.y += cameraPosY;
        //    mainCamera.transform.position = cameraPos;

        //}
        //    void Stopshake()
        //    {
        //    CancelInvoke("StarShake");
        //    mainCamera.transform.position = cameraPos;
        //}
    }
}


