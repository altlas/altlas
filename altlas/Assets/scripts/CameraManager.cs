using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraManager : MonoBehaviour
{
    public Camera DesktopCamera;
    public Camera VrCamera;
    public bool VrEnabled { get; private set; }
    public Camera ActiveCamera { get; private set; }

    private static CameraManager _instance = null;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
        _instance = this;

        VrEnabled = XRSettings.isDeviceActive;
        
        VrCamera.enabled = VrEnabled;
        DesktopCamera.enabled = !VrEnabled;

        ActiveCamera = VrEnabled ? VrCamera : DesktopCamera;
    }

    public static Camera GetActiveCamera()
    {
        return _instance.ActiveCamera;
    }
}
