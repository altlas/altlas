using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerOnClick : MonoBehaviour, ClickableInterface
{
    public float speed = 1f;
    public Vector3 openOffset = new Vector3(0f, 0f, .5f);
    private bool isOpen = false;
    private bool isMoving = false;
    private Vector3 startPosition;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (isMoving)
        {
            var target = isOpen ? startPosition + openOffset : startPosition;
            if(transform.position == target)
            {
                isMoving = false;
                return;
            }
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
	}

    void ClickableInterface.onClick()
    {
        Debug.Log("drawer clicked");
        isMoving = true;
        isOpen = !isOpen;
    }
}
