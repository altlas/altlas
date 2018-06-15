using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCameraFollow : MonoBehaviour
{
    private Transform _mainCamera;
    private Transform _lens;

    private void Start()
    {
        Debug.Log(CameraManager.GetActiveCamera());
        _mainCamera = CameraManager.GetActiveCamera().transform;
        _lens = transform.parent;
    }

    private void Update()
    {
        // get vector from camera to lens and base lens direction
        var mainCamToLens = _lens.position - _mainCamera.position;
        var lensBaseDirection = _lens.forward;
        // fix x rotation of cylinder
        lensBaseDirection = Quaternion.AngleAxis(90, _lens.right) * lensBaseDirection;
        // mix base dir and view dir
        var mixedDir = lensBaseDirection * 0.4f + mainCamToLens * 0.6f;
        transform.rotation = Quaternion.LookRotation(mixedDir);
    }
}
