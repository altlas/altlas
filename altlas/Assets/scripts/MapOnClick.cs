using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapOnClick : MonoBehaviour, ClickableInterface
{
    public float speed = 1f;
    public Vector3 targetLocation = new Vector3(-0.2f, 0.858f, 0.65f);

    private float sideOfDeskMapDepth = .2f;
    private float middleOfDeskMapDepth = .6f;

    private Vector3 startPosition;
    public bool isNormlScale = true;
    public bool isLargeScale = false;
    public bool isGettingLarger = false;

    public enum MapState {
        InDrawer,
        OnSideOfDesk,
        MovingSideToMiddle,
        ScalingPreviewToFitDesk,
        InMiddleOfDesk,
        ScalingToFitDeskPreview,
        MovingMiddleToSide };

    public MapState state = MapState.InDrawer;


    // Use this for initialization
    void Start()
    {

    }

    void Update() {
        switch (state)
        {
            case MapState.MovingSideToMiddle:
                {
                    //var target = MoveStack.MAP_ON_MIDDLE_OF_DESK!=null ? targetLocation : startPosition;
                    if (transform.position == targetLocation)
                    {
                        state = MapState.ScalingPreviewToFitDesk;
                        break;
                    }
                    var step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, targetLocation, step);
                    break;
                }
            case MapState.ScalingPreviewToFitDesk:
                {
                    if (transform.localScale.z >= middleOfDeskMapDepth)
                    {
                        state = MapState.InMiddleOfDesk;

                        var data = GetComponent<MapScript>().data;
                        data.loadTexture(); // data.disposeTexture()
                        GetComponent<Renderer>().material.mainTexture = data.texture;
                    }
                    var step = speed * Time.deltaTime;

                    var mapData = GetComponent<MapScript>().data;
                    var imageSize = mapData.m_imageSize;

                    var y = transform.localScale.y;
                    var z = middleOfDeskMapDepth;
                    var x = z * imageSize[1] / imageSize[0];
                    transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(x, y, z), step);
                    break;
                }
        }
    }

    void ClickableInterface.onClick()
    {
        switch (state) {
            case MapState.InDrawer:
                GameObject stack = transform.parent.gameObject;
                //a map in a stack has been clicked even though there are already maps on the desk, so first reset the old stack
                if (MoveStack.removedStack != null)
                {
                    MoveStack.resetStack();
                }
                MoveStack.moveStackToDesk(stack);
                break;
            case MapState.OnSideOfDesk:
                MoveStack.moveMapToMiddleOfDesk(gameObject);
                state = MapState.MovingSideToMiddle;
                break;
        }
    }
}
