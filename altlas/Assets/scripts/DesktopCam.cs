using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopCam : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    private Camera cam;
    private GameObject gameObject;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.GetComponent<HighlightScript>() != null)
        {
            var tempGameObject = hit.transform.gameObject;
            if (gameObject == null)
            {
                tempGameObject.GetComponent<HighlightScript>().OnRayEnter();
            }
            else if (tempGameObject != gameObject)
            {
                gameObject.GetComponent<HighlightScript>().OnRayExit();
                tempGameObject.GetComponent<HighlightScript>().OnRayEnter();
            }
            gameObject = tempGameObject;
        }
        else if (gameObject != null)
        {
            gameObject.GetComponent<HighlightScript>().OnRayExit();
            gameObject = null;
        }
        if (gameObject != null && Input.GetMouseButtonDown(0))
        {
            var clickable = gameObject.GetComponent<ClickableInterface>();
            if (clickable != null)
            {
                clickable.onClick();
            }
        }
        if (Input.GetMouseButton(1))
        {
            cam.transform.GetChild(0).gameObject.SetActive(true);
            cam.transform.GetChild(0).localPosition = new Vector3(0, 0, 0.3f);
        } else
        {
            cam.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
