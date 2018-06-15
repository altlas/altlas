using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapOnClick : MonoBehaviour, ClickableInterface
{
    public float speed = 1f;
    public Vector3 targetLocation = new Vector3(-0.2f, 0.858f, 0.65f);
    
    public static Vector3 DESK_FREE_AREA_LEFT_CORNER = new Vector3(-0.7105434f, 0.85f, 0.4124395f);
    public static float DESK_FREE_AREA_LENGTH = 0.4f;

    private float sideOfDeskMapDepth = .2f;
    private float middleOfDeskMapDepth = .65f;

    private Vector3 startPosition;
    public bool isNormlScale = true;
    public bool isLargeScale = false;
    public bool isGettingLarger = false;
    public LeverScript leverScript;

    private Vector3 moveTarget;

    public enum MapState {
        InDrawer,
        OnSideOfDesk,
        MovingSideToMiddle,
        ScalingPreviewToFitDesk,
        InMiddleOfDesk,
        ScalingFitDeskToPreview,
        MovingMiddleToSide };

    public MapState state = MapState.InDrawer;


    // Use this for initialization
    void Start()
    {
        leverScript = (LeverScript)Object.FindObjectOfType(typeof(LeverScript));
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
                        leverScript.setActiveGeoCords(gameObject.GetComponent<MapScript>().data.m_coordinate);

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
            case MapState.ScalingFitDeskToPreview:
                {
                    if (transform.localScale.z <= sideOfDeskMapDepth)
                    {
                        float newX = DESK_FREE_AREA_LEFT_CORNER.x - Random.Range(0, DESK_FREE_AREA_LENGTH);
                        float newZ = DESK_FREE_AREA_LEFT_CORNER.z + Random.Range(0, DESK_FREE_AREA_LENGTH);
                        float newY = DESK_FREE_AREA_LEFT_CORNER.y + Random.Range(0, 0.01f);

                        moveTarget = new Vector3(newX, newY, newZ);
                        state = MapState.MovingMiddleToSide;
                    }
                    var step = speed * Time.deltaTime;

                    var mapData = GetComponent<MapScript>().data;
                    var imageSize = mapData.m_imageSize;

                    var y = transform.localScale.y;
                    var z = sideOfDeskMapDepth;
                    var x = z * imageSize[1] / imageSize[0];
                    transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(x, y, z), step);
                    break;
                }
            case MapState.MovingMiddleToSide:
                {
                    if (transform.position == moveTarget)
                    {
                        state = MapState.OnSideOfDesk;

                        var data = GetComponent<MapScript>().data;
                        GetComponent<Renderer>().material.mainTexture = data.thumbnail;
                        data.disposeTexture();
                        break;
                    }
                    var step = speed * Time.deltaTime;

                    transform.position = Vector3.MoveTowards(transform.position, moveTarget, step);
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
